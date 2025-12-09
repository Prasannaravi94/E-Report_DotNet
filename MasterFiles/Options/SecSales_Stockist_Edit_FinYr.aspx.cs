using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Options_SecSales_Stockist_Edit_FinYr : System.Web.UI.Page
{
    #region "Variable Declarations"
    DataSet dsYear = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsSecSale = new DataSet();
    DataSet dsSalesforce = new DataSet();
    int iErrReturn = -1;
    DataTable dtrowClr = new DataTable();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack) // Only on first time page load
        {
            FillYear();
            //Populate MR dropdown as per sf_code
            FillHQ();
            FillStockiest();

            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
        }
        hHeading.InnerText = Page.Title;
    }


    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            DataSet dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division

            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    int Year = k + 1;
                    string F_Year = k + "-" + Year;
                    ddlFinancial.Items.Add(F_Year);
                }
            }

            string CurYear = DateTime.Now.Year.ToString() + "-" + (Convert.ToInt32(DateTime.Now.Year.ToString()) + 1);
            ddlFinancial.Items.FindByText(CurYear).Selected = true;

            //ddlFinancial.Items.Contains();
            //ddlFinancial.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            //ErrorLog err = new ErrorLog();
            // iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillYear()");
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        BindStockiest();
    }

    private void FillHQ()
    {
        Stockist sk = new Stockist();
        string Sf_code = "admin";
        dsSalesforce = sk.getPool_Name(div_code, Sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "Pool_Id";
            ddlFieldForce.DataTextField = "Pool_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();
        }
    }

    private void BindStockiest()
    {
        DB_EReporting db = new DB_EReporting();
        string strQry = "";
        string strQry1 = "";

        string word = ddlFinancial.SelectedItem.ToString();
        string[] token = word.Split('-');

        string Year = (token[0].Trim().Replace(" ", "")).Replace("-", "");
        string Year2 = (token[1].Trim().Replace(" ", "")).Replace("-", "");


        strQry = " select  distinct ROW_NUMBER() OVER ( ORDER BY Year,Month ) Sl_No,SS_Head_Sl_No, Month,(SUBSTRING((DATENAME(month, (CAST(( Month)as varchar(20))+'-01'+'-2019'))),1,3)) as Mnth,Year, " +
            " a.SF_Code,(select sf_name from Mas_Salesforce where SF_Code=a.SF_Code)sf_name " +
            " ,(select  MAX(cast(SS_Head_Sl_No as int)) from Trans_SS_Entry_Head where Stockiest_Code=a.Stockiest_Code)Rowcnt  from Trans_SS_Entry_Head a  " +
            " where Stockiest_Code='" + ddlStk.SelectedValue.ToString() + "' " +
            "  and (Year >  '" + Year + "'  OR Year = '" + Year + "' AND Month >= 04) AND  ((Year <  '" + Year2 + "' ) OR Year =  '" + Year2 + "'  AND Month <= 03 ) order by  Year,Month ";
        dsSecSale = db.Exec_DataSet(strQry);
        if (dsSecSale != null)
        {
            if (dsSecSale.Tables[0].Rows.Count > 0)
            {
                grdSecSales.DataSource = dsSecSale;
                grdSecSales.DataBind();
                btnSubmit.Visible = true;

                dtrowClr = dsSecSale.Tables[0].Copy();
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


    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            CheckBox chkSaleEntry = (CheckBox)e.Row.FindControl("chkSaleEntry");
            Image imgCross = (Image)e.Row.FindControl("imgCross");
            Label lblYear = (Label)e.Row.FindControl("lblYear");
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            Label lblSf_Code = (Label)e.Row.FindControl("lblSf_Code");
            Label lblRowcnt = (Label)e.Row.FindControl("lblRowcnt");
            Label lblhead = (Label)e.Row.FindControl("lblhead");
            //lblYear.Text = indx.ToString();
            //lblMonth.Text = (dtrowClr.Rows.Count).ToString();
            //if (indx == grdSecSales.Rows.Count)
            if (lblhead.Text.Trim() == lblRowcnt.Text.Trim())
            {
                chkSaleEntry.Visible = true;
                imgCross.Visible = false;
            }
            else
            {
                chkSaleEntry.Visible = false;
                imgCross.Visible = true;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(time);
        try
        {
            ListedDR lst = new ListedDR();

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
                    DataSet dsleave = new DataSet();
                    SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                    string Bus_value = string.Empty;
                    string Spent_value = string.Empty;

                    foreach (GridViewRow gridrow in grdSecSales.Rows)
                    {
                        #region Variables
                        Label lblSf_Code = (Label)gridrow.FindControl("lblSf_Code");
                        CheckBox chkSaleEntry = (CheckBox)gridrow.FindControl("chkSaleEntry");

                        Label lblMonth_num = (Label)gridrow.FindControl("lblMonth_num");
                        Label lblYear = (Label)gridrow.FindControl("lblYear");

                        //Label lblSf_Code = (Label)gridrow.Cells[1].FindControl("lblSf_Code");
                        //CheckBox chkSaleEntry = (CheckBox)gridrow.Cells[5].FindControl("chkSaleEntry");
                        if (chkSaleEntry.Checked)
                        {
                            SecSale ss = new SecSale();

                            if (lblMonth_num.Text != "" && lblYear.Text != "")
                            {
                                string Ex_SNo_Dtl = string.Empty;
                                DataSet ds_ExTrns_No_Dtl = new DataSet();
                                command.CommandText = "  select SS_Head_Sl_No from Trans_SS_Entry_Head where Stockiest_Code='" + ddlStk.SelectedValue.ToString() + "' and Month='" + lblMonth_num.Text + "' and Year='" + lblYear.Text + "' ";
                                SqlDataAdapter daExTrans_Dtl = new SqlDataAdapter(command);
                                daExTrans_Dtl.Fill(ds_ExTrns_No_Dtl);
                                if (ds_ExTrns_No_Dtl.Tables[0].Rows.Count > 0)
                                {
                                    Ex_SNo_Dtl = ds_ExTrns_No_Dtl.Tables[0].Rows[0]["SS_Head_Sl_No"].ToString();
                                }
                                if (Ex_SNo_Dtl == "")
                                {
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
                                }
                                else
                                {
                                    command.CommandText =
                                " Delete from Trans_SS_Entry_Detail_Value where SS_Det_Sl_No in (select SS_Det_Sl_No from Trans_SS_Entry_Detail where SS_Head_Sl_No in (select SS_Head_Sl_No from Trans_SS_Entry_Head where Stockiest_Code='" + ddlStk.SelectedValue.ToString() + "' and Month='" + lblMonth_num.Text + "' and Year='" + lblYear.Text + "')) ";
                                    command.ExecuteNonQuery();

                                    command.CommandText =
                               " Delete from Trans_SS_Entry_Detail where SS_Head_Sl_No  in (select SS_Head_Sl_No from Trans_SS_Entry_Head where Stockiest_Code='" + ddlStk.SelectedValue.ToString() + "' and Month='" + lblMonth_num.Text + "' and Year='" + lblYear.Text + "') ";
                                    command.ExecuteNonQuery();

                                    command.CommandText =
                                " Delete from Trans_SS_Entry_Head where Stockiest_Code='" + ddlStk.SelectedValue.ToString() + "' and Month='" + lblMonth_num.Text + "' and Year='" + lblYear.Text + "' ";
                                    command.ExecuteNonQuery();

                                    command.CommandText =
                               " Delete from Trans_Secondary_Entry_BillDetails  where Stockist_Code='" + ddlStk.SelectedValue.ToString() + "' and Trans_Month='" + lblMonth_num.Text + "' and Trans_Year='" + lblYear.Text + "' ";
                                    command.ExecuteNonQuery();

                                }
                            }

                        }
                        #endregion
                    }
                    transaction.Commit();
                    connection.Close();

                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Business Valuewise Entry Updated Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sales Delete request(s) have been Deleted successfully');</script>");

                    BindStockiest();
                    //ddlFinancial.Enabled = true;
                    //lblStk.Enabled = true;
                    //ddlFieldForce.Enabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
            }
        }
        catch (Exception ex2)
        {

        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStockiest();
    }


    private void FillStockiest()
    {

        DB_EReporting db = new DB_EReporting();
        string strQry = "";
        string strQry1 = "";

        strQry = " select distinct Stockist_Code,Stockist_Name from Mas_Stockist  where Division_Code='" + div_code + "' and Stockist_Active_Flag=0 and  Territory like '" + ddlFieldForce.SelectedItem.ToString() + "%'   order by Stockist_Name ";
        dsSecSale = db.Exec_DataSet(strQry);

        //        select ROW_NUMBER() OVER(ORDER BY Year, Month) Sl_No, (SUBSTRING((DATENAME(month, (CAST((Month) as varchar(20)) + '-01' + '-2019'))), 1, 3)) as Mnth, Year,
        //(select sf_name from Mas_Salesforce where SF_Code = a.SF_Code)sf_name from Trans_SS_Entry_Head a  where Stockiest_Code = '1992'--sf_code = 'MR0245' and Year = 2020 and Month = 10
        //order by  Year,Month

        if (dsSecSale.Tables[0].Rows.Count > 0)
        {
            ddlStk.DataValueField = "Stockist_Code";
            ddlStk.DataTextField = "Stockist_Name";
            ddlStk.DataSource = dsSecSale;
            ddlStk.DataBind();
        }

    }
}
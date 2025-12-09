using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
public partial class MasterFiles_MR_SSale_SS_Entry_Primary : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsHead = new DataSet();
    DataSet dsHead2 = new DataSet();
    DataSet dsSale = new DataSet();
    DataSet dsRet = new DataSet();
    DataSet dsYear = null;
    DataSet dsStok = new DataSet();
    DataSet dsTranP = new DataSet();
    DataSet dsReason = new DataSet();
    DataSet dsget = new DataSet();
    DataSet dsMax = new DataSet();
    int iErrReturn = -1;
    string SBillNo = string.Empty;
    string TBillNo = string.Empty;
    string RBillNo = string.Empty;
    string TBilldt = string.Empty;
    string TBillval = string.Empty;
    string st_ERP_Code = string.Empty;
    string sub_code = string.Empty;
    int Bill_Slno = -1;
    string sub_code_P = string.Empty;
    int iyear;
    int imonth;
    string imon = string.Empty;
    string iyr = string.Empty;
    DataSet dsPrev = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        hdnSfcode.Value = sf_code;
        if (!Page.IsPostBack) // Only on first time page load
        {
            // menu1.Title = this.Page.Title;
            //  menu1.FindControl("btnBack").Visible = false;

            FillYear();
            FillHQ();
            // FillStockiest();
        }
    }
    private void FillHQ()
    {
        Stockist ss = new Stockist();
        DataSet ds = ss.GetHQ_Stockist(div_code);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlHQ.Items.Clear();
            ddlHQ.DataTextField = "Pool_Name";
            ddlHQ.DataValueField = "Pool_Id";
            ddlHQ.DataSource = ds;
            ddlHQ.DataBind();
            ddlHQ.Items.Insert(0, "--Select--");
        }

    }
    private void FillStockiest()
    {

        Stockist st = new Stockist();
        DataTable ds = st.Search_HQ_Stockist(div_code, ddlHQ.SelectedItem.Text.Trim());
        if (ds.Rows.Count > 0)
        {
            ddlStockiest.DataValueField = "Stockist_Code";
            ddlStockiest.DataTextField = "Stockist_Name";
            ddlStockiest.DataSource = ds;
            ddlStockiest.DataBind();
            ddlStockiest.Items.Insert(0, "--Select--");
        }
        // ddlStockiest.SelectedIndex = 0;

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
                }
            }
            //  ddlYear.Text = DateTime.Now.Year.ToString();
            //  ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //ddlMonth.Enabled = false;
        //ddlStockiest.Enabled = false;
        //ddlYear.Enabled = false;
        //SubDivision sb = new SubDivision();
        //DataSet dsSub = sb.getSub_sf(sf_code);
        //if (dsSub.Tables[0].Rows.Count > 0)
        //{
        //    sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    sub_code_P = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    sub_code_P = sub_code_P.Remove(sub_code_P.Length - 1);
        //}
        if (ddlMonth.SelectedValue == "1")
        {
            imon = "12";
            iyear = Convert.ToInt32(ddlYear.SelectedValue) - 1;
            iyr = iyear.ToString();
        }
        else
        {
            imonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            imon = imonth.ToString();
            iyr = ddlYear.SelectedValue;

        }

        //SecSale sa = new SecSale();

        //dsReason = sa.Get_Reject_Reason(div_code, ddlStockiest.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue, sub_code);
        //if (dsReason.Tables[0].Rows.Count > 0)
        //{
        //    pnlReject.Visible = true;
        //    lblrej.Text = "Your SS Entry has been Rejected  " + getMonth(Convert.ToInt16(dsReason.Tables[0].Rows[0]["Trans_Month"].ToString())) + " " + Convert.ToInt16(dsReason.Tables[0].Rows[0]["Trans_Year"].ToString()) + "<br> Rejected Reason: "
        //                                            + dsReason.Tables[0].Rows[0]["Reject_Reason"].ToString();
        //}
        //dsPrev = sa.GetSLNO_SS_prev(div_code, ddlStockiest.SelectedValue, imon, iyr, sub_code);
        //dsHead = sa.GetSLNO_SS(div_code, ddlStockiest.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue, sub_code);
        //if (dsPrev.Tables[0].Rows.Count > 0)
        //{
        //    if (dsPrev.Tables[0].Rows[0]["Approve_Flag"].ToString() == "2")
        //    {
        //        ddlMonth.Enabled = false;
        //        ddlStockiest.Enabled = false;
        //        ddlYear.Enabled = false;
        //        pnlApp.Visible = true;
        //        pnlprimary.Visible = false;
        //        lblApp.Text = "Prev Month Sale Approval Pending";

        //    }
        //}

        ////  SecSale sa = new SecSale();

        //else if (dsHead.Tables[0].Rows.Count > 0)
        //{
        //    if (dsHead.Tables[0].Rows[0]["Approve_Flag"].ToString() == "2")
        //    {
        //        ddlMonth.Enabled = false;
        //        ddlStockiest.Enabled = false;
        //        ddlYear.Enabled = false;
        //        pnlApp.Visible = true;
        //        pnlprimary.Visible = false;
        //        lblApp.Text = "Approval Pending";

        //    }
        //    else if (dsHead.Tables[0].Rows[0]["Approve_Flag"].ToString() == "0")
        //    {
        //        ddlMonth.Enabled = false;
        //        ddlStockiest.Enabled = false;
        //        ddlYear.Enabled = false;
        //        pnlApp.Visible = true;
        //        pnlprimary.Visible = false;
        //        lblApp.Text = "Already Entered";
        //    }
        //}
        //else
        //{
        SecSale sa = new SecSale();
        dsHead = sa.Get_SS_Primary_Bill(div_code, ddlStockiest.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsHead.Tables[0].Rows.Count > 0)
        {

            if (dsHead.Tables[0].Rows[0]["Status"].ToString() == "2")
            {
                SqlDataAdapter da;

                SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                con.Open();
                SqlCommand cmd = new SqlCommand("select  SUBSTRING(SF_Code, 0, CHARINDEX(',', SF_Code)) AS SF_Code from mas_stockist where stockist_code=" + ddlStockiest.SelectedValue.Trim() + " ", con);
                DataSet dsbill = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsbill);
                con.Close();
                if (dsbill.Tables[0].Rows.Count > 0)
                {
                    if (dsbill.Tables[0].Rows[0]["SF_Code"].ToString() != "")
                    {
                        Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + dsbill.Tables[0].Rows[0]["SF_Code"].ToString() + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 2);
                    }
                }


            }
            ddlHQ.Enabled = false;
            ddlMonth.Enabled = false;
            ddlStockiest.Enabled = false;
            ddlYear.Enabled = false;
            pnlApp.Visible = true;
            pnlprimary.Visible = false;
            lblApp.Text = "Already Entered";
        }
        else
        {
            ddlHQ.Enabled = false;
            ddlMonth.Enabled = false;
            ddlStockiest.Enabled = false;
            ddlYear.Enabled = false;
            pnlApp.Visible = false;
            pnlTrans.Visible = true;
            pnlprimary.Visible = true;
            Stockist st = new Stockist();
            dsSale = st.Get_Primary_bill_Sale_Tab(ddlStockiest.SelectedValue.Trim(), ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                grdSale.Visible = true;
                grdSale.DataSource = dsSale;
                grdSale.DataBind();
            }
            else
            {
                grdSale.DataSource = dsSale;
                grdSale.DataBind();
            }

            dsRet = st.Get_Primary_bill_Ret_P(ddlStockiest.SelectedValue.Trim(), ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
            if (dsRet.Tables[0].Rows.Count > 0)
            {
                grdRet.Visible = true;
                grdRet.DataSource = dsRet;
                grdRet.DataBind();
            }
            else
            {
                grdRet.DataSource = dsRet;
                grdRet.DataBind();
            }
            dsTranP = st.Get_TransitDt_Prev_P(ddlStockiest.SelectedValue.Trim(), imon, iyr, div_code);
            if (dsTranP.Tables[0].Rows.Count > 0)
            {
                grdPrev.Visible = true;
                grdPrev.DataSource = dsTranP;
                grdPrev.DataBind();
            }
            else
            {
                grdPrev.DataSource = dsTranP;
                grdPrev.DataBind();
            }
            if (dsSale.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                pblNObills.Visible = true;
                pnlprimary.Visible = false;
                pnlTrans.Visible = false;
                pnlApp.Visible = true;
                //  lblApp.Text = "No Data Found";
            }

        }
    }
    private string getMonth(int iMonth)
    {
        string sMonth = string.Empty;

        if (iMonth == 1)
        {
            sMonth = "January";
        }
        else if (iMonth == 2)
        {
            sMonth = "Febraury";
        }
        else if (iMonth == 3)
        {
            sMonth = "March";
        }
        else if (iMonth == 4)
        {
            sMonth = "April";
        }
        else if (iMonth == 5)
        {
            sMonth = "May";
        }
        else if (iMonth == 6)
        {
            sMonth = "June";
        }
        else if (iMonth == 7)
        {
            sMonth = "July";
        }
        else if (iMonth == 8)
        {
            sMonth = "August";
        }
        else if (iMonth == 9)
        {
            sMonth = "September";
        }
        else if (iMonth == 10)
        {
            sMonth = "October";
        }
        else if (iMonth == 11)
        {
            sMonth = "November";
        }
        else if (iMonth == 12)
        {
            sMonth = "December";
        }
        return sMonth;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        foreach (GridViewRow gridRow in grdSale.Rows)
        {
            CheckBox chkR = (CheckBox)gridRow.Cells[0].FindControl("chkReceived");
            bool bCheck = chkR.Checked;
            Label lblbill = (Label)gridRow.Cells[2].FindControl("lblinvoice");
            string Bill = lblbill.Text.ToString().Trim();

            CheckBox chkT = (CheckBox)gridRow.Cells[0].FindControl("chkTransit");
            bool TCheck = chkT.Checked;
            Label lblbill2 = (Label)gridRow.Cells[2].FindControl("lblinvoice");
            string TBill = lblbill2.Text.ToString().Trim();

            Label lblbillDt = (Label)gridRow.Cells[2].FindControl("lbldate");
            string TBidt = lblbillDt.Text.ToString().Trim();

            Label lblvalue = (Label)gridRow.Cells[2].FindControl("lblvalue");
            string val = lblvalue.Text.ToString().Trim();
            if ((bCheck == true))
            {
                SBillNo += Bill + '~';
            }
            if ((TCheck == true))
            {
                TBillNo += TBill + '~';
                TBilldt += TBidt + '~';
                TBillval += val + '~';
            }
        }
        foreach (GridViewRow gridRow in grdRet.Rows)
        {
            CheckBox chkRet = (CheckBox)gridRow.Cells[0].FindControl("chkRReceived");
            bool RCheck = chkRet.Checked;
            Label lblRbill = (Label)gridRow.Cells[2].FindControl("lblRdate");
            string RBill = lblRbill.Text.ToString().Trim();

            if ((RCheck == true))
            {
                RBillNo += RBill + '~';
            }

        }
        //if (SBillNo != "")
        //{
        //    SBillNo = SBillNo.Remove(SBillNo.Length - 1);
        //}
        //if (TBillNo != "")
        //{
        //    TBillNo = TBillNo.Remove(TBillNo.Length - 1);
        //}

        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();

            // Start a local transaction.

            command.Connection = connection;


            command.CommandText =

              " delete from Trans_Secondary_Entry_BillDetails  where Stockist_Code='" + ddlStockiest.SelectedValue.Trim() + "' and Trans_Month='" + ddlMonth.SelectedValue + "' and Trans_Year='" + ddlYear.SelectedValue + "' " +
              " AND Sub_div='" + sub_code + "'";

            command.ExecuteNonQuery();

            SecSale sale = new SecSale();
            Bill_Slno = sale.GetBillSlNO();

            command.CommandText =

            " INSERT INTO Trans_Secondary_Entry_BillDetails(Sl_No,Bill_No_Date,Transit_Bill_No_Date,SaleRet_BillNo_Date ,Updated_Date,Stockist_Code,Trans_Month,Trans_Year,Sub_div,Transit_bill_Dt,Transit_bill_val) " +
            " VALUES (" + Bill_Slno + ", '" + SBillNo + "','" + TBillNo + "','" + RBillNo + "',getdate(),'" + ddlStockiest.SelectedValue.Trim() + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + sub_code + "','" + TBilldt + "','" + TBillval + "')";

            command.ExecuteNonQuery();

            connection.Close();



        }
        Stockist st = new Stockist();
        dsStok = st.getStockistCreate_StockistName(div_code, ddlStockiest.SelectedValue);
        if (dsStok.Tables[0].Rows.Count > 0)
        {
            st_ERP_Code = dsStok.Tables[0].Rows[0]["Stockist_Designation"].ToString();
        }

        //   Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + sf_code + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 1);
        SqlDataAdapter da;

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmd = new SqlCommand("select  SUBSTRING(SF_Code, 0, CHARINDEX(',', SF_Code)) AS SF_Code from mas_stockist where stockist_code=" + ddlStockiest.SelectedValue.Trim() + " ", con);
        DataSet dsbill = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(dsbill);
        con.Close();
        if (dsbill.Tables[0].Rows.Count > 0)
        {
            if (dsbill.Tables[0].Rows[0]["SF_Code"].ToString() != "")
            {
                Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + dsbill.Tables[0].Rows[0]["SF_Code"].ToString() + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 1);
            }
            else
            {
                Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + sf_code + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 1);
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlStockiest.Enabled = true;
        ddlStockiest.SelectedIndex = -1;
        ddlHQ.Enabled = true;
        ddlHQ.SelectedIndex = -1;
        ddlMonth.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        lblApp.Text = "";
        pnlprimary.Visible = false;
        pnlTrans.Visible = false;
        pnlReject.Visible = false;
        lblrej.Text = "";
        pblNObills.Visible = false;
    }

    protected void ddlHQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStockiest();
    }
    //protected void ddlStockiest_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //}
    protected void btnGo2_Click(object sender, EventArgs e)
    {

        //SubDivision sb = new SubDivision();
        //DataSet dsSub = sb.getSub_sf(sf_code);
        //if (dsSub.Tables[0].Rows.Count > 0)
        //{
        //    sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        //}
        SecSale sa = new SecSale();
        dsget = sa.Get_Prev_St_Prim(div_code, ddlStockiest.SelectedValue);

        if (dsget.Tables[0].Rows.Count > 0)
        {
            // dsMax = sa.Get_Month_Max(div_code, ddlStockiest.SelectedValue, sub_code);
            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand(" SELECT  TOP 1  Month,Year,status   " +
                      " FROM Trans_SS_Entry_Head  WHERE  Division_Code='" + div_code + "' AND  Stockiest_Code  = '" + ddlStockiest.SelectedValue + "'   " +
                      " ORDER BY Year DESC,Month DESC ", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // DataTable dt = new DataTable();
                    da.Fill(dsMax);
                    con.Close();
                }
            }
            if (dsMax.Tables[0].Rows.Count > 0)
            {
                if (dsMax.Tables[0].Rows[0]["Month"].ToString().Trim() != "12")
                {
                    int mn = Convert.ToInt32(dsMax.Tables[0].Rows[0]["Month"].ToString().Trim()) + 1;
                    ddlMonth.SelectedValue = mn.ToString();
                    ddlYear.SelectedValue = dsMax.Tables[0].Rows[0]["Year"].ToString().Trim();
                }
                else
                {
                    ddlMonth.SelectedValue = "1";
                    //  int yr = Convert.ToInt32(dsMax.Tables[0].Rows[0]["Trans_Year"].ToString().Trim()) + 1;
                    ddlYear.SelectedValue = DateTime.Today.Year.ToString();
                }
                if (ddlMonth.SelectedValue == "1")
                {
                    imon = "12";
                    iyear = Convert.ToInt32(ddlYear.SelectedValue) - 1;
                    iyr = iyear.ToString();
                }
                else
                {
                    imonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
                    imon = imonth.ToString();
                    iyr = ddlYear.SelectedValue;

                }
                //dsHead2 = sa.Get_SS_Primary_Bill(div_code, ddlStockiest.SelectedValue, imon, iyr);
                //if (dsHead2.Tables[0].Rows.Count > 0)
                //{
                //    if (dsHead2.Tables[0].Rows[0]["Status"].ToString() == "2")
                //    {
                //        ddlMonth.SelectedValue = imon;
                //        ddlYear.SelectedValue = iyr;

                //    }
                //}
            }
            else
            {
                //  ddlMonth.SelectedValue = "1";
                //  int yr = 2018;
                //   ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            }


        }
        else
        {
            ddlMonth.Enabled = true;
            ddlYear.Enabled = true;
        }

        //ddlMonth.SelectedIndex = 0;
        //ddlYear.SelectedIndex = 0;
        //ddlMonth.Enabled = true;
        //ddlYear.Enabled = true;
    }
    protected void chkbill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbill.Checked == true)
        {

            //  Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + sf_code + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 1);
            SqlDataAdapter da;

            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            con.Open();
            SqlCommand cmd = new SqlCommand("select  SUBSTRING(SF_Code, 0, CHARINDEX(',', SF_Code)) AS SF_Code from mas_stockist where stockist_code=" + ddlStockiest.SelectedValue.Trim() + " ", con);
            DataSet dsbill = new DataSet();
            da = new SqlDataAdapter(cmd);
            da.Fill(dsbill);
            con.Close();
            if (dsbill.Tables[0].Rows.Count > 0)
            {
                if (dsbill.Tables[0].Rows[0]["SF_Code"].ToString() != "")
                {
                    Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + dsbill.Tables[0].Rows[0]["SF_Code"].ToString() + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 1);
                }
                else
                {
                    Response.Redirect("Sec_Sale_Entry.aspx?sfcode=" + sf_code + "&stk_code=" + ddlStockiest.SelectedValue.Trim() + "&stk_name=" + ddlStockiest.SelectedItem.Text.Trim() + "&FMonth=" + ddlMonth.SelectedValue.Trim() + "&Fyear=" + ddlYear.SelectedValue.Trim() + "&Billno=" + SBillNo + "&TBillno=" + TBillNo + "&RBillno=" + RBillNo + "&st_ERP=" + st_ERP_Code + "&Status=" + 1);
                }
            }
        }
    }
}
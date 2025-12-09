using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_ProductRate : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string prod_code = string.Empty;
    string prod_name = string.Empty;
    decimal mrp_amt;
    decimal ret_amt;
    decimal dist_amt;
    decimal nsr_amt;
    decimal target_amt;
    decimal sample_amt;
    string effective_from = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            FillState(div_code);
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtEffFrom.Text = DateTime.Now.ToShortDateString();
            btnGo.Focus();
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateProd(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }
    // Sorting
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Product dv = new Product();
        dtGrid = dv.getProductRatelist_DataTable(div_code);
        return dtGrid;
    }

    protected void grdProdRate_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdProdRate.DataSource = sortedView;
        grdProdRate.DataBind();

    }



    private void FillProd()
    {
        Product dv = new Product();
        DataSet dsst = new DataSet();
        string State_Code = string.Empty;

        //dsProd = dv.getProdRate(ddlState.SelectedValue.ToString(), div_code);
        if (ddlState.SelectedItem.Text == "ALL")
        {
            //string[] strState;
            //dsst = dv.getProduct_State_Code(div_code);
            //if (dsst.Tables[0].Rows.Count > 0)
            //{
            //    State_Code = dsst.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            //}
            //State_Code = State_Code.Remove(State_Code.Length - 1);
            //strState = State_Code.Split(',');
            //dsProd = dv.getProductRate_all(div_code, strState[0]);
            dsProd = dv.getProdRate_all_Latest(div_code);
        }
        else
        {
            dsProd = dv.getProductRate(ddlState.SelectedValue.ToString(), div_code);
        }

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdProdRate.Visible = true;
            grdProdRate.DataSource = dsProd;
            grdProdRate.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdProdRate.DataSource = dsProd;
            grdProdRate.DataBind();
        }
    }

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    string State_Code = string.Empty;
    //    Product dv = new Product();
    //    int iReturn = -1;
    //    int iMaxState = 0;

    //    iMaxState = dv.getMaxStateSlNo(ddlState.SelectedValue, div_code);

    //    if (ddlState.SelectedItem.Text == "ALL")
    //    {
    //        iReturn = dv.DeleteProductRate(div_code);
    //    }
    //    else
    //    {
    //        iReturn = dv.DeleteProductRate(ddlState.SelectedValue, div_code);
    //    }
    //    Division div = new Division();
    //    DataSet dsstate = new DataSet();
    //    string[] strState;
    //    dsstate = div.getStatePerDivision(div_code);
    //    if (dsstate.Tables[0].Rows.Count > 0)
    //    {
    //        State_Code = dsstate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //    }
    //    State_Code = State_Code.Remove(State_Code.Length - 1);
    //    strState = State_Code.Split(',');
    //    foreach (GridViewRow gridRow in grdProdRate.Rows)
    //    {

    //        Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
    //        prod_code = lblProdCode.Text;

    //        TextBox txtMRP = (TextBox)gridRow.Cells[1].FindControl("txtMRP");
    //        mrp_amt = Convert.ToDecimal(txtMRP.Text);

    //        TextBox txtRP = (TextBox)gridRow.Cells[1].FindControl("txtRP");
    //        ret_amt = Convert.ToDecimal(txtRP.Text);

    //        TextBox txtDP = (TextBox)gridRow.Cells[1].FindControl("txtDP");
    //        dist_amt = Convert.ToDecimal(txtDP.Text);

    //        TextBox txtNSR = (TextBox)gridRow.Cells[1].FindControl("txtNSR");
    //        nsr_amt = Convert.ToDecimal(txtNSR.Text);

    //        TextBox txtTarg = (TextBox)gridRow.Cells[1].FindControl("txtTarg");
    //        target_amt = Convert.ToDecimal(txtTarg.Text);

    //        TextBox txtSamp = (TextBox)gridRow.Cells[1].FindControl("txtSamp");
    //        sample_amt = Convert.ToDecimal(txtSamp.Text);

    //        // Update Division
    //        if (ddlState.SelectedItem.Text == "ALL")
    //        {
    //            //DataSet dsstate = new DataSet();
    //            Product st = new Product();
    //            //string[] strState;
    //            //dsstate = st.getProduct_State(div_code, prod_code);
    //            //if (dsstate.Tables[0].Rows.Count > 0)
    //            //{
    //            //    State_Code = dsstate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //}
    //            //State_Code = State_Code.Remove(State_Code.Length - 1);
    //            //strState = State_Code.Split(',');               


    //            foreach (string state in strState)
    //            {
    //                iReturn = dv.UpdateProductRate(prod_code, state, txtEffFrom.Text, mrp_amt, ret_amt, dist_amt, nsr_amt, target_amt, div_code, iMaxState, sample_amt);
    //            }
    //        }
    //        else
    //        {
    //            iReturn = dv.UpdateProductRate(prod_code, ddlState.SelectedValue.ToString(), txtEffFrom.Text, mrp_amt, ret_amt, dist_amt, nsr_amt, target_amt, div_code, iMaxState, sample_amt);
    //        }
    //        if (iReturn > 0)
    //        {
    //            // menu1.Status = "Produce Rate Updated Successfully ";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
    //        }
    //    }

    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string State_Code = string.Empty;
        Product dv = new Product();
        int iReturn = -1;
        int iMaxState = 0;

        try
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
                    DataSet ds_SlNo = new DataSet();
                    string leave = "SELECT ISNULL(MAX(Max_State_Sl_No),0)+1 FROM mas_Product_State_Rates WHERE state_code = '" + ddlState.SelectedValue + "' AND Division_Code='" + div_code + "' ";
                    SqlCommand cmd1;
                    cmd1 = new SqlCommand(leave, con1);
                    con1.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(ds_SlNo);
                    con1.Close();
                    iMaxState = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());


                    SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                    DataSet ds_SlNo_New = new DataSet();
                    string leave_New = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                    SqlCommand cmd2;
                    cmd2 = new SqlCommand(leave_New, con2);
                    con2.Open();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(ds_SlNo_New);
                    con2.Close();

                    int iSlNo = Convert.ToInt32(ds_SlNo_New.Tables[0].Rows[0]["Column1"].ToString());


                    //iMaxState = dv.getMaxStateSlNo(ddlState.SelectedValue, div_code);

                    if (ddlState.SelectedItem.Text == "ALL")
                    {
                        command.CommandText = "Delete from Mas_Product_State_Rates where Division_Code='" + div_code + "'";
                        command.ExecuteNonQuery();
                        //iReturn = dv.DeleteProductRate(div_code);
                    }
                    else
                    {
                        command.CommandText = "Delete from Mas_Product_State_Rates where State_Code='" + ddlState.SelectedValue + "' and Division_Code='" + div_code + "'"; ;
                        command.ExecuteNonQuery();
                        //iReturn = dv.DeleteProductRate(ddlState.SelectedValue, div_code);
                    }
                    foreach (GridViewRow gridRow in grdProdRate.Rows)
                    {

                        Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
                        prod_code = lblProdCode.Text;

                        TextBox txtMRP = (TextBox)gridRow.Cells[1].FindControl("txtMRP");
                        mrp_amt = Convert.ToDecimal(txtMRP.Text);

                        TextBox txtRP = (TextBox)gridRow.Cells[1].FindControl("txtRP");
                        ret_amt = Convert.ToDecimal(txtRP.Text);

                        TextBox txtDP = (TextBox)gridRow.Cells[1].FindControl("txtDP");
                        dist_amt = Convert.ToDecimal(txtDP.Text);

                        TextBox txtNSR = (TextBox)gridRow.Cells[1].FindControl("txtNSR");
                        nsr_amt = Convert.ToDecimal(txtNSR.Text);

                        TextBox txtTarg = (TextBox)gridRow.Cells[1].FindControl("txtTarg");
                        target_amt = Convert.ToDecimal(txtTarg.Text);

                        TextBox txtSamp = (TextBox)gridRow.Cells[1].FindControl("txtSamp");
                        sample_amt = Convert.ToDecimal(txtSamp.Text);
                        //SqlCommand command_2 = connection.CreateCommand();
                        // Update Division
                        if (ddlState.SelectedItem.Text == "ALL")
                        {
                            DataSet dsstate = new DataSet();
                            Product st = new Product();
                            string[] strState;
                            dsstate = st.getProduct_State(div_code, prod_code);
                            if (dsstate.Tables[0].Rows.Count > 0)
                            {
                                State_Code = dsstate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            }
                            State_Code = State_Code.Remove(State_Code.Length - 1);
                            strState = State_Code.Split(',');


                            foreach (string state in strState)
                            {
                                //int isl

                                //command.CommandText = string.Empty;
                                command.CommandText = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price," +
                                    "Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date,Sample_Price) VALUES('" + iSlNo + "', '" + iMaxState + "','" + state + "', '" + prod_code + "', '" + mrp_amt + "', '" + ret_amt + "', '" + dist_amt + "','" + target_amt + "', '" + nsr_amt + "', '" + txtEffFrom.Text.Substring(6, 4) + "-" + txtEffFrom.Text.Substring(3, 2) + "-" + txtEffFrom.Text.Substring(0, 2) + "','" + div_code + "', getdate(),getdate(),'" + sample_amt + "')";

                                command.ExecuteNonQuery();
                                iSlNo = iSlNo + 1;
                                //iReturn = dv.UpdateProductRate(prod_code, state, txtEffFrom.Text, mrp_amt, ret_amt, dist_amt, nsr_amt, target_amt, div_code, iMaxState, sample_amt);
                            }
                        }
                        else
                        {
                            //SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            //DataSet ds_SlNo_New = new DataSet();
                            //string leave_New = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                            //SqlCommand cmd2;
                            //cmd2 = new SqlCommand(leave_New, con2);
                            //con2.Open();
                            //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            //da2.Fill(ds_SlNo_New);
                            //con2.Close();
                            //int iSlNo = Convert.ToInt32(ds_SlNo_New.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                         " Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date,Sample_Price) VALUES " +
                         " ( '" + iSlNo + "', '" + iMaxState + "', '" + ddlState.SelectedValue + "', '" + prod_code + "', '" + mrp_amt + "', '" + ret_amt + "', '" + dist_amt + "', " +
                         " '" + target_amt + "', '" + nsr_amt + "', '" + txtEffFrom.Text.Substring(6, 4) + "-" + txtEffFrom.Text.Substring(3, 2) + "-" + txtEffFrom.Text.Substring(0, 2) + "', '" + div_code + "', getdate(),getdate(),'" + sample_amt + "' ) ";

                            command.ExecuteNonQuery();

                            iSlNo = iSlNo + 1;

                            //iReturn = dv.UpdateProductRate(prod_code, ddlState.SelectedValue.ToString(), txtEffFrom.Text, mrp_amt, ret_amt, dist_amt, nsr_amt, target_amt, div_code, iMaxState, sample_amt);
                        }

                        //if (iReturn > 0)
                        //{
                        //    // menu1.Status = "Produce Rate Updated Successfully ";
                        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                        //}
                    }
                    transaction.Commit();
                    connection.Close();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully!');</script>");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                    Console.WriteLine("Message: {0}", ex.Message);


                    // Attempt to roll back the transaction.
                    try
                    {

                        transaction.Rollback();

                    }

                    catch (Exception ex2)
                    {

                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                        Console.WriteLine("  Message: {0}", ex2.Message);

                    }
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        tblRate.Visible = true;
        FillProd();
    }
}
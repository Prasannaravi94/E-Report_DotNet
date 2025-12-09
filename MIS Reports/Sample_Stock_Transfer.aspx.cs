using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;

public partial class MIS_Reports_Sample_Stock_Transfer : System.Web.UI.Page
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
    int Trans_Sl_No_From;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    int Detail_Trans_Sl_No;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;
    //DateTime ServerStartTime;
    //DateTime ServerEndTime;
    int time;
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
            //ServerStartTime = DateTime.Now;
            //base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //FillMR();
            //FillYear();

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                // FillMRManagers();
                FillMRfor_mr();
                // FillMRfor_mr_To();
                FillYear();

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                //  txtNew.Visible = false;
                //FillMRManagers();
                FillYear();
                FillMRfor_mr();

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
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                // FillMRManagers();
                FillMRfor_mr();
                FillMR_ActiveOnly();
                //FillMRfor_mr_To();
                FillYear();

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

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
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
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }


        }
        FillColor();
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
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

        }
    }

    private void FillMRManagers()
    {
        //SalesForce sf = new SalesForce();
        ////dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        //dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
        //if (sf_type == "3")
        //{
        //    dsSalesForce.Tables[0].Rows[1].Delete();
        //}
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlFieldForce.DataTextField = "sf_name";
        //    ddlFieldForce.DataValueField = "sf_code";
        //    ddlFieldForce.DataSource = dsSalesForce;
        //    ddlFieldForce.DataBind();

        //    ddlSF.DataTextField = "des_color";
        //    ddlSF.DataValueField = "sf_code";
        //    ddlSF.DataSource = dsSalesForce;
        //    ddlSF.DataBind();
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        dsSalesForce = sf.UserListTP_Hierarchy(div_code, sf_code);

        if (sf_type == "3")
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
            dsSalesForce.Tables[0].Rows[1].Delete();
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }


        FillColor();
    }


    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    private void FillColor2()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF2.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            toddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    [ScriptMethod]

    private void FillMRfor_mr()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceListMgrGet_Active_Only(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesforce;
            ddlSF.DataBind();

        }
        FillColor();
    }

    private void FillMR_ActiveOnly()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceListMgrGet_Active_Only(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            toddlFieldForce.DataValueField = "SF_Code";
            toddlFieldForce.DataTextField = "Sf_Name";
            toddlFieldForce.DataSource = dsSalesforce;
            toddlFieldForce.DataBind();

            ddlSF2.DataTextField = "Desig_Color";
            ddlSF2.DataValueField = "sf_code";
            ddlSF2.DataSource = dsSalesforce;
            ddlSF2.DataBind();

        }
        FillColor();
    }
    [WebMethod(EnableSession = true)]
    private void FillMRfor_mr_To()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceListMgrGet_Vacant_Only(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesforce;
            ddlSF.DataBind();

        }
        FillColor2();
    }
    private void FillMRfor_mr_Active_And_Vacant()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesforce;
            ddlSF.DataBind();

        }
        FillColor2();
    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {

        DataSet da = new DataSet();
        Product Sample_Product = new Product();

        if (ddlmode.SelectedValue=="3")
        {
            //ddlFieldForce.Visible = false;
            lblffto.Visible = false;
            toddlFieldForce.Visible = false;
            lblMonth.Visible = false;
            ddlMonth.Visible = false;
            lblYear.Visible = false;
            ddlYear.Visible = false;
            da = Sample_Product.GetAdjusmentBal(div_code,ddlFieldForce.SelectedValue);
            if (da.Tables[0].Rows.Count > 0)
            {
                btntransfer.Visible = true;
                grdadjusment.DataSource = da;
                grdadjusment.DataBind();
            }
            else
            {
                grdadjusment.DataSource = da;
                grdadjusment.DataBind();
            }
        }
        else if(ddlmode.SelectedValue=="1" || ddlmode.SelectedValue == "2")
        {
            ddlFieldForce.Enabled = false;
            toddlFieldForce.Enabled = false;
            ddlMonth.Enabled = false;
            ddlYear.Enabled = false;

            //da = Sample_Product.GetSampled_Product_Temp(div_code, ddlFieldForce.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            da = Sample_Product.GetAdjusmentBal(div_code, ddlFieldForce.SelectedValue);
            //SalesForce Sample_Product = new SalesForce();
            //da = Sample_Product.Sample_Status_New_Product(div_code,ddlFieldForce.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            if (da.Tables[0].Rows.Count > 0)
            {
                btntransfer.Visible = true;
                grdsample.DataSource = da;
                grdsample.DataBind();
            }
            else
            {
                grdsample.DataSource = da;
                grdsample.DataBind();
            }
        }
        
    }
    protected void ddlmodeselectedindexchange(object sender, EventArgs e)
    {
        if(ddlmode.SelectedValue=="1")
        {
            FillMRfor_mr();
            FillMR_ActiveOnly();
            lblMonth.Visible = true;
            ddlMonth.Visible = true;
            lblffto.Visible = true;
            toddlFieldForce.Visible = true;
            lblYear.Visible = true;
            ddlYear.Visible = true;
        }
        else if(ddlmode.SelectedValue=="2")
        {
            FillMRfor_mr_To();
            FillMR_ActiveOnly();
            lblMonth.Visible = true;
            ddlMonth.Visible = true;
            lblffto.Visible = true;
            toddlFieldForce.Visible = true;
            lblYear.Visible = true;
            ddlYear.Visible = true;
        }
        else if(ddlmode.SelectedValue=="3")
        {
            FillMRfor_mr();
            lblMonth.Visible = false;
            ddlMonth.Visible = false;
            lblffto.Visible = false;
            toddlFieldForce.Visible = false;
            lblYear.Visible = false;
            ddlYear.Visible = false;
        }
        else if (ddlmode.SelectedValue == "4")
        {
            FillMRfor_mr_Active_And_Vacant();
            lblMonth.Visible = false;
            ddlMonth.Visible = false;
            lblffto.Visible = false;
           // txtNew1.Visible = false;
            toddlFieldForce.Visible = false;
            lblYear.Visible = false;
            ddlYear.Visible = false;
            btntransfer.Visible = true;
            btnGo.Visible = false;

        }
    }

    //protected void grdDr_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {


    //        Label lblsamqty = (Label)e.Row.FindControl("lbldes");
    //        Label lblissued = (Label)e.Row.FindControl("lbliss");
    //        Label lblclosing = (Label)e.Row.FindControl("lblClosingBal");

    //        if(lblsamqty.Text!="" && lblissued.Text!="")
    //        {
    //            int open1 = Convert.ToInt32(lblsamqty.Text);
    //            int issued1 = Convert.ToInt32(lblissued.Text);

    //            if ((Convert.ToInt32(lblissued.Text)) > (open1))
    //            {

    //                int issue1 = open1;

    //                lblissued.Text = issue1.ToString();
    //                lblclosing.Text = "";
    //            }

    //            //int open1 = Convert.ToInt32(lblsamqty.Text);
    //            //int issued1 = Convert.ToInt32(lblissued.Text);
    //            else
    //            {
    //                int clos = open1 - issued1;
    //                lblclosing.Text = clos.ToString();
    //            }


    //        }
    //        else
    //        {
    //            lblclosing.Text = lblsamqty.Text;
    //        }


    //    }
    //}




    protected void BtnTransfer_Click(object sender, EventArgs e)
     {


        System.Threading.Thread.Sleep(time);
        try
        {
            DataSet db = new DataSet();
            int Trans_Sl_No;
            int iReturn = -1;
            int iReturn2 = -1;
            Product sample_Given_Product = new Product();
            using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // transaction = connection.BeginTransaction();

                // command.Connection = connection;

                //command.Transaction = transaction;
                if (ddlmode.SelectedValue == "4")
                {
                    try
                    {
                        SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                        command.CommandText = "delete from Trans_Sample_Stock_FFWise_AsonDate where Sf_Code='" + ddlFieldForce.SelectedValue + "' and  division_Code='" + div_code + "'";

                        command.ExecuteNonQuery();

                        //command.CommandText = "exec sample_At_A_Glance_Process_Single_Fieldforce '" + div_code + "','" + ddlFieldForce.SelectedValue + "','" + DateTime.Now.Month + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Year + "'";

                        //command.ExecuteNonQuery();
                        command.CommandText = "exec Sample_Split_MR_Wise_LastTwomonths '" + div_code + "','" + ddlFieldForce.SelectedValue + "'";
                        command.ExecuteNonQuery();

                        //command.CommandText = "exec sample_At_A_Glance_Process_Single_Fieldforce '" + div_code + "','" + ddlFieldForce.SelectedValue + "','" + DateTime.Now.Month + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Year + "'";
                        command.CommandText = "Exec Sample_Process_Fieldforcewise '" + ddlFieldForce.SelectedValue + "','" + div_code + "'";

                        command.ExecuteNonQuery();

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processd Successfully!'); window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                        Console.WriteLine("Message: {0}", ex.Message);


                        // Attempt to roll back the transaction.
                        try
                        {

                            //transaction.Rollback();

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

                if (ddlmode.SelectedValue=="3")
                {
                    try
                    {
                        SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                        DataSet ds_SlNo = new DataSet();
                        string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Transfer_Head";
                        SqlCommand cmd1;
                        cmd1 = new SqlCommand(leave, con1);
                        con1.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        da1.Fill(ds_SlNo);
                        con1.Close();
                        Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                        command.CommandText = "insert into Trans_Sample_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce.SelectedValue + "','" + ddlFieldForce.SelectedValue + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + div_code + "',getdate(),'" + ddlFieldForce.SelectedItem + "','" + ddlFieldForce.SelectedItem + "')";

                        command.ExecuteNonQuery();

                        foreach (GridViewRow row in grdadjusment.Rows)
                        {
                            Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                            Label lblProduct = (Label)row.Cells[3].FindControl("lblprdtName");
                            Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                            Label Prod_Erp_Code = (Label)row.Cells[4].FindControl("lblsaleerpcode");
                            TextBox transfrerQty = (TextBox)row.Cells[5].FindControl("txtTransferQty");
                            //Label lbltransferqty_New = (Label)row.Cells[6].FindControl("txtTransferQty");
                            HiddenField HiddenField1 = (HiddenField)row.Cells[6].FindControl("HiddenField1");

                            int Old_Desptach_Qty = 0;
                            //string trans_Sl_No = db.Tables[0].Rows[0]["Trans_sl_No"].ToString();

                            if (HiddenField1.Value != "" && HiddenField1.Value != "0")
                            {

                                SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                DataSet ds_SlNo_new = new DataSet();

                                string leave_New = "select MAX(b.Trans_sl_No) from Trans_Sample_Despatch_Head a,Trans_Sample_Despatch_Details b where a.Sf_Code='" + ddlFieldForce.SelectedValue + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR.Text + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select SI_EM_Month + '-' + '1' + '-' + SI_EM_Year from setup_others where division_code = '" + div_code + "')";
                                SqlCommand cmd2;
                                cmd2 = new SqlCommand(leave_New, con2);
                                con2.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                da2.Fill(ds_SlNo_new);
                                con2.Close();
                                Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new.Tables[0].Rows[0]["Column1"].ToString());

                                if(Convert.ToInt32(lbldespatch_qty.Text) > Convert.ToInt32(HiddenField1.Value))
                                {
                                    Old_Desptach_Qty = -Convert.ToInt32(transfrerQty.Text);
                                }
                                else
                                {
                                    Old_Desptach_Qty = Convert.ToInt32(transfrerQty.Text);
                                 
                                }
                                if(ds_SlNo_new.Tables[0].Rows.Count>0)
                                {
                                    command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,Despatch_Actual_qty,Despatch_Qty_Bk,productc,Received_Flag) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct.Text + "','" + Old_Desptach_Qty + "','" + Old_Desptach_Qty + "','" + Old_Desptach_Qty + "','" + lblDR.Text + "',1)";


                                    command.ExecuteNonQuery();
                                }
                                else
                                {
                                    if(Old_Desptach_Qty < 0)
                                    {
                                        command.CommandText = "update Sample_OB_Updation set OB_Qty=Ob_Qty-" + transfrerQty.Text + " where sf_Code='" + ddlFieldForce.SelectedValue + "' and Product_Code='" + lblDR.Text + "'";

                                        command.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        command.CommandText = "update Sample_OB_Updation set OB_Qty=Ob_Qty+" + transfrerQty.Text + " where sf_Code='" + ddlFieldForce.SelectedValue + "' and Product_Code='" + lblDR.Text + "'";

                                        command.ExecuteNonQuery();
                                    }
                                }




                                command.CommandText = "UPDATE Trans_Sample_Stock_FFWise_AsonDate SET  Sample_AsonDate=" + HiddenField1.Value + " where cast(Division_Code as varchar) = '" + div_code + "' and SF_Code = '" + ddlFieldForce.SelectedValue + "' and Prod_Detail_Sl_No = '" + lblDR.Text + "' ";
                                command.ExecuteNonQuery();

                                command.CommandText = "Insert into Trans_Sample_Transfer_Detail        (Trans_Sl_No,Product_Code,Product_Erp_Code,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR.Text + "','" + Prod_Erp_Code.Text + "','" + lbldespatch_qty.Text + "','" + HiddenField1.Value + "','" + div_code + "')";

                                command.ExecuteNonQuery();
                            }

                        }
                        //}
                        //transaction.Commit();
                        connection.Close();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');window.location ='" + Request.Url.AbsoluteUri + "';</script>");

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                        Console.WriteLine("Message: {0}", ex.Message);


                        // Attempt to roll back the transaction.
                        try
                        {

                            //transaction.Rollback();

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
                else if(ddlmode.SelectedValue=="1" || ddlmode.SelectedValue == "2")
                {
                    DataSet ds_SlNo_new = new DataSet();
                    try
                    {
                        db = sample_Given_Product.GetSampled_Product(toddlFieldForce.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                        if (db.Tables[0].Rows.Count > 0)
                        {


                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            DataSet ds_SlNo = new DataSet();
                            string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Transfer_Head";
                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds_SlNo);
                            con1.Close();
                            Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "insert into Trans_Sample_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce.SelectedValue + "','" + toddlFieldForce.SelectedValue + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + div_code + "',getdate(),'" + ddlFieldForce.SelectedItem + "','" + toddlFieldForce.SelectedItem + "')";

                            command.ExecuteNonQuery();




                            //for (int i = 0; i <= db.Tables[0].Rows.Count - 1; i++)
                            //{
                            foreach (GridViewRow row in grdsample.Rows)
                            {
                                // string accessType = row.Cells[3].Text;
                                //Label lblTrans_Sl_No = (Label)row.Cells[1].FindControl("lbl_Trans_SlNo");
                                //Label lblUnique_Sl_No = (Label)row.Cells[1].FindControl("Unique_Trans_SlNo");
                                Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                                Label lblProduct = (Label)row.Cells[3].FindControl("lblprdtName");
                                Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                                Label Prod_Erp_Code = (Label)row.Cells[4].FindControl("lblsaleerpcode");
                                //TextBox transfrerQty = (TextBox)row.Cells[6].FindControl("txtTransferQty");
                                TextBox transfrerQty = (TextBox)row.Cells[5].FindControl("txtTransferQty");



                                // string actual_despatch_Qty = db.Tables[0].Rows[i]["Despatch_Qty"].ToString();
                                string trans_Sl_No = db.Tables[0].Rows[0]["Trans_sl_No"].ToString();


                                //if (db.Tables[0].Rows[i]["Product_Code_SlNo"].ToString() == lblDR.Text)
                                //{
                                //if (transfrerQty.Text == "" || transfrerQty.Text == "0")
                                //{

                                //}
                                //else
                                //{
                                // int New_Despatch_Qty = Convert.ToInt32(db.Tables[0].Rows[i]["Despatch_Qty"].ToString()) + Convert.ToInt32(transfrerQty.Text);
                                if (transfrerQty.Text != "" && transfrerQty.Text != "0")
                                {

                                 if(ddlFieldForce.SelectedValue!="admin")
                                    {
                                        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                      
                                        // string leave_New = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_despatch_Head where Sf_Code='" + ddlFieldForce.SelectedValue + "'";
                                        string leave_New = "select MAX(b.Trans_sl_No) from Trans_Sample_Despatch_Head a,Trans_Sample_Despatch_Details b where a.Sf_Code='" + ddlFieldForce.SelectedValue + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR.Text + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select SI_EM_Month + '-' + '1' + '-' + SI_EM_Year from setup_others where division_code = '" + div_code + "')";
                                        SqlCommand cmd2;
                                        cmd2 = new SqlCommand(leave_New, con2);
                                        con2.Open();
                                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                        da2.Fill(ds_SlNo_new);
                                        con2.Close();
                                        Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new.Tables[0].Rows[0]["Column1"].ToString());
                                    }


                                    int Old_Desptach_Qty = -Convert.ToInt32(transfrerQty.Text);
                                    //string New_Despatch_Qty = db.Tables[0].Rows[i]["Despatch_Qty"].ToString() + transfrerQty.Text;
                                    //string actual_despatch_Qty = db.Tables[0].Rows[i]["Despatch_Qty"].ToString();
                                    //string trans_Sl_No = db.Tables[0].Rows[i]["Trans_sl_No"].ToString();
                                    // Product admin = new Product();


                                    AdminSetup adm = new AdminSetup();
                                    DataSet dssample1 = adm.getsample_AsonDate(ddlFieldForce.SelectedValue, div_code, lblDR.Text);

                                    DataSet dssample2 = adm.getsample_AsonDate(toddlFieldForce.SelectedValue, div_code, lblDR.Text);

                                    if (dssample1.Tables[0].Rows.Count > 0 && dssample2.Tables[0].Rows.Count > 0)
                                    {


                                        if (dssample1.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString() == dssample2.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString())
                                        {
                                            int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty.Text);
                                            int tot_sample2 = Convert.ToInt32(dssample2.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) + Convert.ToInt32(transfrerQty.Text);
                                            //If Already the Product There just Increment the qty as(inhand+Uploaded)
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce.SelectedValue, div_code, tot_Sample, lblDR.Text);
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(toddlFieldForce.SelectedValue, div_code, tot_sample2, lblDR.Text);
                                        }
                                    }
                                    else
                                    {
                                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty.Text);
                                        iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce.SelectedValue, div_code, tot_Sample, lblDR.Text);
                                        iReturn2 = adm.InsertSample_AS_ON_Date(toddlFieldForce.SelectedValue, div_code, transfrerQty.Text, lblDR.Text);
                                    }

                                    //command.CommandText = "update Trans_Sample_Despatch_Details set Despatch_Qty='" + Old_Desptach_Qty + "' where Trans_sl_No='" + lblTrans_Sl_No.Text + "' and         productc='" + lblDR.Text + "' and sl_No='" + lblUnique_Sl_No.Text + "' ";
                                    if(ds_SlNo_new.Tables[0].Rows.Count>0)
                                    {
                                        command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,Despatch_Actual_qty,Despatch_Qty_Bk,productc,Received_Flag) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct.Text + "','" + Old_Desptach_Qty + "','" + Old_Desptach_Qty + "','" + Old_Desptach_Qty + "','" + lblDR.Text + "',1)";

                                        //command.CommandText = "update Trans_Sample_Despatch_Details set Despatch_Qty='" + Old_Desptach_Qty + "' where Trans_sl_No='" + trans_Sl_No + "' and         productc='" + lblDR.Text + "' ";

                                        command.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        command.CommandText = "update Sample_OB_Updation set OB_Qty=Ob_Qty-" + transfrerQty.Text + " where sf_Code='" + ddlFieldForce.SelectedValue + "' and Product_Code='" + lblDR.Text + "'";

                                        command.ExecuteNonQuery();
                                    }
                                   

                                    if(toddlFieldForce.SelectedValue!="admin")
                                    {
                                        command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,productc,Despatch_Qty_Bk,Despatch_Actual_qty,Received_Flag) values('" + trans_Sl_No + "','" + div_code + "','" + lblProduct.Text + "','" + transfrerQty.Text + "','" + lblDR.Text + "','" + transfrerQty.Text + "','" + transfrerQty.Text + "',1)";

                                        command.ExecuteNonQuery();
                                    }



                                    command.CommandText = "Insert into Trans_Sample_Transfer_Detail        (Trans_Sl_No,Product_Code,Product_Erp_Code,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR.Text + "','" + Prod_Erp_Code.Text + "','" + lbldespatch_qty.Text + "','" + transfrerQty.Text + "','" + div_code + "')";

                                    command.ExecuteNonQuery();
                                }

                            }
                            //}
                            //transaction.Commit();
                            connection.Close();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                        }
                        else
                        {
                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            DataSet ds_SlNo = new DataSet();
                            string leave = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Transfer_Head";

                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds_SlNo);
                            con1.Close();
                            Trans_Sl_No = Convert.ToInt32(ds_SlNo.Tables[0].Rows[0]["Column1"].ToString());

                            command.CommandText = "insert into Trans_Sample_Transfer_Head(Trans_Sl_No,From_Sf_Code,To_Sf_Code,Trans_Month,Trans_Year,Division_Code,Transfer_Date,From_Sf_Name,To_Sf_Name) values('" + Trans_Sl_No + "','" + ddlFieldForce.SelectedValue + "','" + toddlFieldForce.SelectedValue + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + div_code + "',getdate(),'" + ddlFieldForce.SelectedItem + "','" + toddlFieldForce.SelectedItem + "')";

                            command.ExecuteNonQuery();


                            // ireturn=

                            //DataSet Trans_Slno = new DataSet();
                            //string New_sl_No = "SELECT ISNULL(MAX(Trans_Sl_No),0)+1 FROM Trans_Sample_Despatch_Head where sf";
                            //SqlCommand cmd2;
                            //cmd2 = new SqlCommand(New_sl_No, con1);
                            //con1.Open();
                            //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            //da2.Fill(Trans_Slno);
                            //con1.Close();
                            //Detail_Trans_Sl_No = Convert.ToInt32(Trans_Slno.Tables[0].Rows[0]["Column1"].ToString());

                            //command.CommandText = "insert into Trans_Sample_Despatch_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Created_Date,Updated_Date,Trans_month_year) values('" + toddlFieldForce.SelectedValue + "','" + div_code + "','" + ddlMonth.SelectedValue + "','"+ddlYear.SelectedValue+"',getdate(),getdate(),getdate())";

                            //command.ExecuteNonQuery();
                            Product Insert_Head = new Product();
                            int ireturn = -1;
                            if(toddlFieldForce.SelectedValue!="admin")
                            {
                                ireturn = Insert_Head.Insert_Existing_Product_New(toddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);

                                DataSet Trans_Slno = new DataSet();
                                string New_sl_No = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_Despatch_Head where sf_code='" + toddlFieldForce.SelectedValue + "' and Trans_Month='" + ddlMonth.SelectedValue + "' and Trans_Year='" + ddlYear.SelectedValue + "'";
                                SqlCommand cmd2;
                                cmd2 = new SqlCommand(New_sl_No, con1);
                                con1.Open();
                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                da2.Fill(Trans_Slno);
                                con1.Close();
                                Detail_Trans_Sl_No = Convert.ToInt32(Trans_Slno.Tables[0].Rows[0]["Column1"].ToString());
                            }




                            //This Part For The Receiving User Who Not Have Any Samples In The despatch detail
                            foreach (GridViewRow row in grdsample.Rows)
                            {
                                // string accessType = row.Cells[3].Text;
                                //Label lblTrans_Sl_No = (Label)row.Cells[1].FindControl("lbl_Trans_SlNo");
                                //Label lblUnique_Sl_No = (Label)row.Cells[1].FindControl("Unique_Trans_SlNo");
                                Label lblDR = (Label)row.Cells[2].FindControl("lblprdtcode");
                                Label lblProduct = (Label)row.Cells[1].FindControl("lblprdtName");
                                Label lbldespatch_qty = (Label)row.Cells[5].FindControl("lblClosingBal");
                                Label Prod_Erp_Code = (Label)row.Cells[3].FindControl("lblsaleerpcode");
                                TextBox transfrerQty = (TextBox)row.Cells[5].FindControl("txtTransferQty");


                                if (transfrerQty.Text != "" && transfrerQty.Text != "0")
                                {

                                    SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                    DataSet ds_SlNo_new1 = new DataSet();
                                    //string leave_New1 = "SELECT MAX(Trans_Sl_No) FROM Trans_Sample_despatch_Head where Sf_Code='" + ddlFieldForce.SelectedValue + "'";
                                    string leave_New1 = "select MAX(b.Trans_sl_No) from Trans_Sample_Despatch_Head a,Trans_Sample_Despatch_Details b where a.Sf_Code='" + ddlFieldForce.SelectedValue + "'  and a.Trans_sl_No=b.Trans_sl_No and productc='" + lblDR.Text + "' and cast((cast(Trans_Year as varchar)+'-'+cast(Trans_Month as varchar)+'-'+'15')as datetime) >= (select SI_EM_Month + '-' + '1' + '-' + SI_EM_Year from setup_others where division_code = '" + div_code + "')";
                                    SqlCommand cmd3;
                                    cmd3 = new SqlCommand(leave_New1, con3);
                                    con3.Open();
                                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                    da3.Fill(ds_SlNo_new1);
                                    con3.Close();
                                    Trans_Sl_No_From = Convert.ToInt32(ds_SlNo_new1.Tables[0].Rows[0]["Column1"].ToString());

                                    int Old_Desptach_Qty = -Convert.ToInt32(transfrerQty.Text);

                                    AdminSetup adm = new AdminSetup();
                                    DataSet dssample1 = adm.getsample_AsonDate(ddlFieldForce.SelectedValue, div_code, lblDR.Text);

                                    DataSet dssample2 = adm.getsample_AsonDate(toddlFieldForce.SelectedValue, div_code, lblDR.Text);

                                    if (dssample1.Tables[0].Rows.Count > 0 && dssample2.Tables[0].Rows.Count > 0)
                                    {


                                        if (dssample1.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString() == dssample2.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString())
                                        {
                                            int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty.Text);
                                            int tot_sample2 = Convert.ToInt32(dssample2.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) + Convert.ToInt32(transfrerQty.Text);
                                            //If Already the Product There just Increment the qty as(inhand+Uploaded)
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce.SelectedValue, div_code, tot_Sample, lblDR.Text);
                                            iReturn2 = adm.UpdateSample_AS_ON_Date(toddlFieldForce.SelectedValue, div_code, tot_sample2, lblDR.Text);
                                        }
                                    }
                                    else
                                    {
                                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) - Convert.ToInt32(transfrerQty.Text);
                                        iReturn2 = adm.UpdateSample_AS_ON_Date(ddlFieldForce.SelectedValue, div_code, tot_Sample, lblDR.Text);
                                        iReturn2 = adm.InsertSample_AS_ON_Date(toddlFieldForce.SelectedValue, div_code, transfrerQty.Text, lblDR.Text);
                                    }

                                    //command.CommandText = "update Trans_Sample_Despatch_Details set Despatch_Qty='" + Old_Desptach_Qty + "' where Trans_sl_No='" + lblTrans_Sl_No.Text + "' and         productc='" + lblDR.Text + "' and sl_No='" + lblUnique_Sl_No.Text + "' ";
                                    command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,Despatch_Actual_qty,Despatch_Qty_Bk,productc,Received_Flag) values('" + Trans_Sl_No_From + "','" + div_code + "','" + lblProduct.Text + "','" + Old_Desptach_Qty + "','" + Old_Desptach_Qty + "','" + Old_Desptach_Qty + "','" + lblDR.Text + "',1)";


                                    command.ExecuteNonQuery();
                                    if(toddlFieldForce.SelectedValue!="admin")
                                    {
                                        command.CommandText = "insert into Trans_Sample_Despatch_Details(Trans_sl_No,Division_Code,Product_Code,Despatch_Qty,Despatch_Actual_qty,productc,Despatch_Qty_Bk,Received_Flag) values('" + Detail_Trans_Sl_No + "','" + div_code + "','" + lblProduct.Text + "','" + transfrerQty.Text + "','" + transfrerQty.Text + "','" + lblDR.Text + "','" + transfrerQty.Text + "',1)";

                                        command.ExecuteNonQuery();
                                    }



                                    command.CommandText = "Insert into Trans_Sample_Transfer_Detail(Trans_Sl_No,Product_Code,Product_Erp_Code,Actual_Balance_Qty,Actual_Transfer_Qty,Division_Code) values('" + Trans_Sl_No + "','" + lblDR.Text + "','" + Prod_Erp_Code.Text + "','" + lbldespatch_qty.Text + "','" + transfrerQty.Text + "','" + div_code + "')";

                                    command.ExecuteNonQuery();
                                }
                            }
                            //transaction.Commit();
                            connection.Close();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transfered Successfully!');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                        Console.WriteLine("Message: {0}", ex.Message);


                        // Attempt to roll back the transaction.
                        try
                        {

                            //transaction.Rollback();

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

         }
        catch(Exception ex)
        {

        }
    }
}

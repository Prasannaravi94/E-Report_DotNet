using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Collections;
using DBase_EReport;
public partial class MasterFiles_MR_LeaveForm : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    DataSet dsAdminSetup = null;

    DataSet dsPolicy = new DataSet();
    DataSet dsHoliday = new DataSet();
    DataSet dsLev = new DataSet();
    string sf_code = string.Empty;
    string sfcode = string.Empty;
    string div_code = string.Empty;
    int request_type = -1;
    string Leave_code = string.Empty;
    string Leave_Id = string.Empty;
    string div_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsLeave = new DataSet();
    DataSet dsSal = new DataSet();
    int time;
    string sf_emp_id = string.Empty;
    string mgr_code = string.Empty;
    string mgr_emp_id = string.Empty;
    string Sf_Name = string.Empty;
    string state_code = string.Empty;
    string EntryMode = string.Empty;
    DataSet dsMaxCl = new DataSet();
    DataSet dsMaxCl2 = new DataSet();
    DataSet dsMaxCl3 = new DataSet();
    string mx_date = string.Empty;
    string mx_date2 = string.Empty;
    string mx_date3 = string.Empty;
    DataSet dsDCR = new DataSet();
    DataSet dsMgr = new DataSet();
    DataSet dsMr = new DataSet();
    DataSet dsWork = new DataSet();
    DataSet dsWorkname = new DataSet();
    DataSet dsremarks = new DataSet();
    DataSet dsmaxSlno = new DataSet();
    int leave_Idd;
    protected void Page_Load(object sender, EventArgs e)
    {
        //  sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = Page.Title;
    
        if (!Page.IsPostBack)
        {
            // menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillLeaveType();

            if (Session["sf_type"].ToString() == "1")
            {
                menu1.Visible = true;
                menu1.Title = this.Page.Title;
                menu1.FindControl("btnBack").Visible = false;
                GetHQ();
                pnlmr.Visible = true;
                btnBack.Visible = false;

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                menu1.Visible = false;

                GetHQ();
                pnlmgr.Visible = true;
                btnBack.Visible = true;
                pnlHead.Visible = true;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null)
            {
                menu1.Visible = false;

                GetHQ();
                pnlmgr.Visible = true;
                btnBack.Visible = true;
                pnlHead.Visible = true;
            }
            else if (Session["sf_type"].ToString() == "3")
            {
                menu1.Visible = false;

                GetHQ();
                pnlmgr.Visible = true;
                btnBack.Visible = true;
                pnlHead.Visible = true;

            }
            if (Request.QueryString["LeaveFrom"] != null)
            {
                menu1.Visible = false;
                pnlleaveback.Visible = true;
                pnltit.Visible = true;
                string Leavedate = Request.QueryString["LeaveFrom"];
                txtLeave.Text = Leavedate.Substring(3, 2) + "/" + Leavedate.Substring(0, 2) + "/" + Leavedate.Substring(6, 4);
                Leavedate = "";
                txtLeave.Enabled = false;
                //imgPopup.Enabled = false;
            }

            if (Request.QueryString["sfcode"] != null)
            {
                sfcode = Request.QueryString["sfcode"].ToString();
            }
            if (Request.QueryString["Leave_Id"] != null)
            {
                Leave_Id = Request.QueryString["Leave_Id"].ToString();
            }
            if (Request.QueryString["status"] == "")
            {
                btnApprove.Visible = false;
                btnApproved.Visible = false;
                btnReject.Visible = false;
            }
            dsMr = adm.getSetup_Leave_Ent_MR(div_code);
            if (dsMr.Tables[0].Rows.Count > 0)
            {
                if (dsMr.Tables[0].Rows[0]["LE_MR"].ToString() == "Y")
                {
                    Pnlent.Visible = true;

                    LeaveBalance_Det();
                }
                else
                {
                    Pnlent.Visible = false;
                }
            }
            if (sfcode != null)
            {
                AdminSetup adm1 = new AdminSetup();
                dsAdminSetup = adm1.getLeave(sfcode, Leave_Id);
                if (dsAdminSetup.Tables[0].Rows.Count > 0)
                {
                    GetHQ();
                    Leave_Id = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ddltype.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtLeave.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtLeaveto.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtreason.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtAddr.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    lblDaysCount.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    chkmanager.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                  //  lblValidreason.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    chkho.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    ddltype.Enabled = false;
                    txtLeave.Enabled = false;
                    txtLeaveto.Enabled = false;
                    txtreason.Enabled = false;
                    txtAddr.Enabled = false;
                    lblDaysCount.Enabled = false;
                    //imgPop.Enabled = false;
                    //imgPopup.Enabled = false;
                    chkmanager.Enabled = false;
                    chkho.Enabled = false;
                   // lblValidreason.Enabled = false;
                }

            }


        }
    }
    private void FillLeaveType()
    {
        AdminSetup adm = new AdminSetup();
        DataSet dsstup = new DataSet();
        dsstup = adm.Leave_type_Setup(div_code);
        if (dsstup.Tables[0].Rows.Count > 0)
        {
            if (dsstup.Tables[0].Rows[0]["Leave_Type_Reg"].ToString() == "D")
            {
                Filltype();
            }
            else
            {
                if (Session["sf_type"].ToString() == "1")
                {
                    sfcode = Session["sf_code"].ToString();
                }
                else
                {
                    sfcode = Request.QueryString["sfcode"];
                }
                DataSet dstype = new DataSet();
                dstype = adm.get_Mr_type(div_code, sfcode);
                if ((dstype.Tables[0].Rows[0]["Fieldforce_Type"].ToString() != "") && (dstype.Tables[0].Rows[0]["Fieldforce_Type"].ToString() != "0") && (dstype.Tables[0].Rows[0]["Fieldforce_Type"].ToString() != "NULL"))
                {
                    dsAdminSetup = adm.FillLeave_Type_stup(div_code, dstype.Tables[0].Rows[0]["Fieldforce_Type"].ToString());
                    if (dsAdminSetup.Tables[0].Rows.Count > 0)
                    {
                        ddltype.DataTextField = "Leave_SName";
                        ddltype.DataValueField = "Leave_code";
                        ddltype.DataSource = dsAdminSetup;
                        ddltype.DataBind();
                    }
                }
                else
                {
                    Filltype();
                }
                
            }
        }
        else
        {
            Filltype();
        }
    }

    private void Filltype()
    {
        dsAdminSetup = adm.FillLeave_Type(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            ddltype.DataTextField = "Leave_SName";
            ddltype.DataValueField = "Leave_code";
            ddltype.DataSource = dsAdminSetup;
            ddltype.DataBind();
        }
    }

    private void LeaveBalance_Det()
    {
        int c_cnt = 0;
        int p_cnt = 0;
        int s_cnt = 0;
        int lo_cnt = 0;
        int t_cnt = 0;
        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.Get_Leave_Bal(div_code, sfcode);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsAdminSetup.Tables[0].Rows.Count; i++)
            {
                if (dsAdminSetup.Tables[0].Rows[i]["Leave_Type_Code"].ToString() == "CL")
                {
                    lblCLL.Text = dsAdminSetup.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblBCL.Text = dsAdminSetup.Tables[0].Rows[i]["Leave_Balance_Days"].ToString();
                }
                if (dsAdminSetup.Tables[0].Rows[i]["Leave_Type_Code"].ToString() == "PL")
                {
                    lblPLL.Text = dsAdminSetup.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblBPL.Text = dsAdminSetup.Tables[0].Rows[i]["Leave_Balance_Days"].ToString();
                }
                if (dsAdminSetup.Tables[0].Rows[i]["Leave_Type_Code"].ToString() == "SL")
                {
                    lblSLL.Text = dsAdminSetup.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblBSL.Text = dsAdminSetup.Tables[0].Rows[i]["Leave_Balance_Days"].ToString();
                }
                if (dsAdminSetup.Tables[0].Rows[i]["Leave_Type_Code"].ToString() == "LOP")
                {
                    lblLLL.Text = dsAdminSetup.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblBLL.Text = dsAdminSetup.Tables[0].Rows[i]["Leave_Balance_Days"].ToString();
                }
                
            }

        }
        if (lblBCL.Text == "" || lblBCL.Text == "NULL")
        {
            lblBCL.Text = "0";
        }
        if (lblBPL.Text == "" || lblBPL.Text == "NULL")
        {
            lblBPL.Text = "0";
        }
        if (lblBSL.Text == "" || lblBSL.Text == "NULL")
        {
            lblBSL.Text = "0";
        }
        if (lblBLL.Text == "" || lblBLL.Text == "NULL")
        {
            lblBLL.Text = "0";
        }
       
        AdminSetup ad = new AdminSetup();
        dsLeave = ad.Leave_App_Pending_Days(div_code, sfcode);
        if (dsLeave.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsLeave.Tables[0].Rows.Count; i++)
            {
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "CL")
                {
                    //   lblACL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    c_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());

                    lblACL.Text = Convert.ToString(c_cnt);
                }
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "PL")
                {
                    //lblAPL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    p_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());

                    lblAPL.Text = Convert.ToString(p_cnt);
                }
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "SL")
                {
                    //lblASL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    s_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());

                    lblASL.Text = Convert.ToString(s_cnt);
                }
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "LOP")
                {
                    lo_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());
                    // lblALL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblALL.Text = Convert.ToString(lo_cnt);
                }
               
            }


        }
        if (lblACL.Text == "" || lblACL.Text == "NULL")
        {
            lblACL.Text = "0";
        }
        if (lblAPL.Text == "" || lblAPL.Text == "NULL")
        {
            lblAPL.Text = "0";
        }
        if (lblASL.Text == "" || lblASL.Text == "NULL")
        {
            lblASL.Text = "0";
        }
        if (lblALL.Text == "" || lblALL.Text == "NULL")
        {
            lblALL.Text = "0";
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
    private void GetHQ()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }
        Territory terr = new Territory();
        dsTerritory = terr.getSfname_Desig(sfcode, div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblemp.Text =
                 "<span style='font-weight: bold;color:#0077FF; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString() + "</span>";
            //Session["Sf_Name"] = dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString();
            lbldesig.Text =
               "<span style='font-weight: bold;color:#0077FF; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Designation_Name"].ToString() + "</span>";
            lblSfhq.Text =
                "<span style='font-weight: bold;color:#0077FF; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";
            lblempcode.Text =
                   "<span style='font-weight: bold;color:#0077FF; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["sf_emp_id"].ToString() + "</span>";

            lbldivi.Text =
                   "<span style='font-weight: bold;color:#0077FF; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Division_Name"].ToString() + "</span>";



        }
    }
    DataTable lv = new DataTable();
    AdminSetup adm = new AdminSetup();
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        btnApprove.Visible = false;
        DateTime dold = Convert.ToDateTime(txtLeave.Text);
        DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
        int iReturn = -1;
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }
        SalesForce sf = new SalesForce();
        dsSal = sf.Leave_Sf_Emp_id(sfcode);
        if (dsSal.Tables[0].Rows.Count > 0)
        {
            sf_emp_id = dsSal.Tables[0].Rows[0]["sf_emp_id"].ToString();
            mgr_code = dsSal.Tables[0].Rows[0]["mgr_code"].ToString();
            mgr_emp_id = dsSal.Tables[0].Rows[0]["mgr_emp_id"].ToString();
            Sf_Name = dsSal.Tables[0].Rows[0]["Sf_Name"].ToString();
        }
        if (Request.Browser.IsMobileDevice)
        {
            EntryMode = "Mobile";
        }
        else
        {
            EntryMode = "Desktop";
        }

        if (txtLeave.Text != "" && txtLeaveto.Text != "")
        {
            DataSet ds = new DataSet();
            DCR dcr = new DCR();
            List<DateTime> dates = GetDatesBetween(dold, dnew);

            ArrayList arrlst = new ArrayList();
            for (var i = 0; i < dates.Count; i++)
            {
                ds = dcr.getLeave_Mr(sfcode, Convert.ToDateTime(dates[i]));


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    arrlst.Add(row);
                }


            }

            if (arrlst.Count > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
                txtAddr.Text = "";
                txtLeave.Text = "";
                txtLeaveto.Text = "";
                txtreason.Text = "";
                ddltype.SelectedIndex = -1;
                lblDaysCount.Text = "";
                btnApprove.Visible = true;
            }
            else
            {
                DCR dc = new DCR();

                dsDCR = dc.get_All_dcr_Date_Leave(sfcode, div_code, dold.ToString("MM/dd/yyyy"), dnew.ToString("MM/dd/yyyy"));
                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Already Posted');</script>");
                    txtAddr.Text = "";
                    txtLeave.Text = "";
                    txtLeaveto.Text = "";
                    txtreason.Text = "";
                    ddltype.SelectedIndex = -1;
                    lblDaysCount.Text = "";
                    btnApprove.Visible = true;
                }
                else
                {
                    //iReturn = adm.Insert_Leave(ddltype.SelectedValue, Convert.ToDateTime(txtLeave.Text), Convert.ToDateTime(txtLeaveto.Text), txtreason.Text, txtAddr.Text, lblDaysCount.Text, chkmanager.SelectedValue, lblValidreason.Text, sfcode, div_code, chkho.SelectedValue);
                    //if (iReturn > 0)
                    //{
                    //    if (Request.QueryString["LeaveFrom"] != null)
                    //    {
                    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='../../DCR/DCR_Entry.aspx';</script>");
                    //    }
                    //    else
                    //    {
                    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='../../MasterFiles/MR/LeaveForm.aspx';</script>");
                    //        LeaveBalance_Det();
                    //    }

                    //}
                    using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = connection.CreateCommand();
                        SqlTransaction transaction;

                        // Start a local transaction.
                        transaction = connection.BeginTransaction("SampleTransaction");

                        // Must assign both transaction object and connection
                        // to Command object for a pending local transaction
                        command.Connection = connection;
                        command.Transaction = transaction;

                        try
                        {
                            DataSet dsleave = new DataSet();
                            SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                            string leave = "SELECT isnull(max(Leave_Id)+1,'1') Leave_Id from mas_Leave_Form ";
                            SqlCommand cmd1;
                            cmd1 = new SqlCommand(leave, con1);
                            con1.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                            da1.Fill(dsleave);
                            con1.Close();
                            if (dsleave.Tables[0].Rows.Count > 0)
                            {
                                leave_Idd = Convert.ToInt32(dsleave.Tables[0].Rows[0]["Leave_Id"].ToString());
                            }
                            DateTime From_Date = Convert.ToDateTime(txtLeave.Text);
                            DateTime To_Date = Convert.ToDateTime(txtLeaveto.Text);

                            string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                            string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;
                            command.CommandText =

                                " INSERT INTO mas_Leave_Form(Leave_Id,Leave_Type, From_Date,To_Date,Reason,Address,No_of_Days,Inform_by,Valid_Reason,Leave_Active_Flag,Created_Date,sf_code,Division_Code,Informed_Ho,Sf_Emp_Id,Mgr_Code,Mgr_Emp_Id,Entry_Mode,Sf_Name) " +
                         " VALUES ('" + leave_Idd + "', '" + ddltype.SelectedValue + "' , '" + leave_From_Date + "' , '" + leave_To_Date + "', '" + txtreason.Text.Trim() + "' , '" + txtAddr.Text.Trim() + "', " +
                         " '" + lblDaysCount.Text + "','" + chkmanager.SelectedValue + "','',2, getdate(),'" + sfcode + "','" + div_code + "','" + chkho.SelectedValue + "','" + sf_emp_id + "','" + mgr_code + "','" + mgr_emp_id + "','" + EntryMode + "','" + Sf_Name + "' ) ";


                            command.ExecuteNonQuery();
                         

                            transaction.Commit();
                            if (Request.QueryString["LeaveFrom"] != null)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='../../DCR/DCR_Entry.aspx';</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='../../MasterFiles/MR/LeaveForm.aspx';</script>");
                                LeaveBalance_Det();
                            }


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
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
                    }


                }
            }
        }

        txtAddr.Text = "";
        txtLeave.Text = "";
        txtLeaveto.Text = "";
        txtreason.Text = "";
        ddltype.SelectedIndex = -1;
        lblDaysCount.Text = "";
    }

    protected void txtLeaveto_TextChanged(object sender, EventArgs e)
    {
        DateTime dold = Convert.ToDateTime(txtLeave.Text);
        DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
        TimeSpan daydif = (dnew - dold);
        double dayd = (daydif.TotalDays) + 1;
        if (txtLeave.Text != "" && txtLeaveto.Text != "")
        {


            if (dold > dnew)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Date must be greater than From date');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
                lblDaysCount.Text = "";
            }
            else
            {
                lblDaysCount.Text = dayd.ToString();
                lblDaysCount.Style.Add("color", "#0077FF");
                lblDaysCount.Style.Add("Font-Size", "16px");
            }

        }
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        if (txtLeaveto.Text != "")
        {
            ds = dcr.getLeave_Mr(sfcode, Convert.ToDateTime(txtLeaveto.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
                txtLeaveto.Text = "";
                lblDaysCount.Text = "";
                txtLeaveto.Focus();
            }
        }
        double lvcnt = 0;
        lv = adm.getOtherSetup(div_code);

        if (lv.Rows.Count > 0)
        {
            lvcnt = Convert.ToDouble(lv.Rows[0]["leave_allowed"].ToString() == "" ? "0" : lv.Rows[0]["leave_allowed"].ToString());

            if (dayd > lvcnt && lvcnt > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + lvcnt + " days');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
                lblDaysCount.Text = "";
            }
        }
        double typecnt = 0;
        dsMr = adm.getSetup_Leave_Ent_MR(div_code);
        if (dsMr.Tables[0].Rows.Count > 0)
        {
            if (dsMr.Tables[0].Rows[0]["LE_MR"].ToString() == "Y")
            {
                if (ddltype.SelectedItem.Text.Trim() == "CL")
                {
                    typecnt = Convert.ToInt32(lblBCL.Text) - Convert.ToInt32(lblACL.Text);
                    if (dayd > typecnt || typecnt <= 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                        txtLeaveto.Text = "";
                        txtLeaveto.Focus();
                        lblDaysCount.Text = "";
                    }
                }
                else if (ddltype.SelectedItem.Text.Trim() == "PL")
                {
                    int mincnt = 0;
                    typecnt = Convert.ToInt32(lblBPL.Text) - Convert.ToInt32(lblAPL.Text);
                    if (dayd > typecnt || typecnt <= 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                        txtLeaveto.Text = "";
                        txtLeaveto.Focus();
                        lblDaysCount.Text = "";
                    }
                }
                else if (ddltype.SelectedItem.Text.Trim() == "SL")
                {
                    typecnt = Convert.ToInt32(lblBSL.Text) - Convert.ToInt32(lblASL.Text);
                    if (dayd > typecnt || typecnt <= 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                        txtLeaveto.Text = "";
                        txtLeaveto.Focus();
                        lblDaysCount.Text = "";
                    }
                }
                else if (ddltype.SelectedItem.Text.Trim() == "LOP")
                {
                    typecnt = Convert.ToInt32(lblBLL.Text) - Convert.ToInt32(lblALL.Text);
                    if (dayd > typecnt || typecnt <= 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                        txtLeaveto.Text = "";
                        txtLeaveto.Focus();
                        lblDaysCount.Text = "";
                    }
                }
            }
        }
    }

    protected void txtLeave_TextChanged(object sender, EventArgs e)
    {
        if (txtLeave.Text != "" && txtLeaveto.Text != "")
        {
            DateTime dold = Convert.ToDateTime(txtLeave.Text);
            DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
            TimeSpan daydif = (dnew - dold);
            double dayd = (daydif.TotalDays) + 1;
            if (dold > dnew)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('To Date must be greater than From date');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
                lblDaysCount.Text = "";
            }
            else
            {
                lblDaysCount.Text = dayd.ToString();
                lblDaysCount.Style.Add("color", "Red");
                lblDaysCount.Style.Add("Font-Size", "16px");
            }
            AdminSetup adm = new AdminSetup();
            double lvcnt = 0;
            lv = adm.getOtherSetup(div_code);

            if (lv.Rows.Count > 0)
            {
                lvcnt = Convert.ToDouble(lv.Rows[0]["leave_allowed"].ToString() == "" ? "0" : lv.Rows[0]["leave_allowed"].ToString());

                if (dayd > lvcnt && lvcnt > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + lvcnt + " days');</script>");
                    txtLeaveto.Text = "";
                    txtLeaveto.Focus();
                    lblDaysCount.Text = "";
                }
            }
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
        }
        else
        {
            sfcode = Request.QueryString["sfcode"];
        }
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        ds = dcr.getLeave_Mr(sfcode, Convert.ToDateTime(txtLeave.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
            txtLeave.Text = "";
            txtLeave.Focus();
        }
        if (txtLeaveto.Text != "")
        {
            DateTime dold = Convert.ToDateTime(txtLeave.Text);
            DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
            TimeSpan daydif = (dnew - dold);
            double dayd = (daydif.TotalDays) + 1;
            double typecnt = 0;
        dsMr = adm.getSetup_Leave_Ent_MR(div_code);
            if (dsMr.Tables[0].Rows.Count > 0)
            {
                if (dsMr.Tables[0].Rows[0]["LE_MR"].ToString() == "Y")
                {
                    if (ddltype.SelectedItem.Text.Trim() == "CL")
                    {
                        typecnt = Convert.ToInt32(lblBCL.Text) - Convert.ToInt32(lblACL.Text);
                        if (dayd > typecnt || typecnt <= 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                            txtLeaveto.Text = "";
                            txtLeaveto.Focus();
                            lblDaysCount.Text = "";
                        }
                    }
                    else if (ddltype.SelectedItem.Text.Trim() == "PL")
                    {
                        int mincnt = 0;
                        typecnt = Convert.ToInt32(lblBPL.Text) - Convert.ToInt32(lblAPL.Text);
                        if (dayd > typecnt || typecnt <= 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                            txtLeaveto.Text = "";
                            txtLeaveto.Focus();
                            lblDaysCount.Text = "";
                        }
                    }
                    else if (ddltype.SelectedItem.Text.Trim() == "SL")
                    {
                        typecnt = Convert.ToInt32(lblBSL.Text) - Convert.ToInt32(lblASL.Text);
                        if (dayd > typecnt || typecnt <= 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                            txtLeaveto.Text = "";
                            txtLeaveto.Focus();
                            lblDaysCount.Text = "";
                        }
                    }
                    else if (ddltype.SelectedItem.Text.Trim() == "LOP")
                    {
                        typecnt = Convert.ToInt32(lblBLL.Text) - Convert.ToInt32(lblALL.Text);
                        if (dayd > typecnt || typecnt <= 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Possible to take more than " + ddltype.SelectedItem.Text + " " + typecnt + " days');</script>");
                            txtLeaveto.Text = "";
                            txtLeaveto.Focus();
                            lblDaysCount.Text = "";
                        }
                    }
                }
            }
            txtLeaveto.Text = "";
            lblDaysCount.Text = "";
        }
    }

    public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
    {
        List<DateTime> allDates = new List<DateTime>();
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            allDates.Add(date);
        return allDates;

    }


    protected void btnApproved_Click(object sender, EventArgs e)
    {
        string sftype = string.Empty;
        string type_Name = string.Empty;
        int iReturn = -1;
        int S_Max = 0;
        int i = 0;
        sfcode = Request.QueryString["sfcode"];
        Leave_Id = Request.QueryString["Leave_Id"].ToString();
        if (sfcode != null)
        {
            //AdminSetup adm = new AdminSetup();
            //iReturn = adm.Leave_Appprove(sfcode, Leave_Id);
            using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                   
                    DateTime From_Date = Convert.ToDateTime(txtLeave.Text);
                    DateTime To_Date = Convert.ToDateTime(txtLeaveto.Text);
                 
                    DataSet Trans_SlNo_New = new DataSet();
                    string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                    string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;

                  
                    if (sfcode.Contains("MGR"))
                    {
                         sftype = "2";

                         type_Name = "MGR";
                    }
                    else if (sfcode.Contains("MR"))
                    {
                         sftype = "1";

                         type_Name = "MR";
                    }
                    while (From_Date <= To_Date)
                    {
                        DataSet Trans_SlNo = new DataSet();
                      
                        string Transno = string.Empty;
                        Transno = "";
                        DB_EReporting db_ER = new DB_EReporting();
                        //command.CommandText = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sfcode + "' and Activity_Date ='" + From_Date.ToString("MM/dd/yyyy") + "' ";
                        //Trans_SlNo = db_ER.Exec_DataSet(command.CommandText);
                        SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                        string leave = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sfcode + "' and Activity_Date ='" + From_Date.ToString("MM/dd/yyyy") + "'";
                        SqlCommand cmd1;
                        cmd1 = new SqlCommand(leave, con1);
                        con1.Open();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                        da1.Fill(Trans_SlNo);
                        con1.Close();
                        if (Trans_SlNo.Tables[0].Rows.Count > 0)
                        {
                            Transno = Trans_SlNo.Tables[0].Rows[0][0].ToString();
                            command.CommandText = "UPDATE DCRMain_Temp  SET Confirmed = 1 ,ReasonforRejection = '' " +
                                   " WHERE Sf_Code = '" + sfcode + "' and Trans_SlNo ='" + Transno + "'";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo =  '" + Transno + "'  and exists  " +
                                                  " (select * from DCRDetail_Lst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "')    ";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Transno + "' and exists " +
                                                  " (select * from DCRDetail_UnLst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "')  ";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Transno + "' and exists  " +
                                                  " (select * from DCRDetail_CSH_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "') ";   
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRMain_Temp where Sf_Code ='" + sfcode + "'  and Trans_SlNo = '" + Transno + "' and exists " +
                                                  " (select * from DCRMain_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "') ";   
                            command.ExecuteNonQuery();

                            command.CommandText = " Insert into DCRMain_Trans select Trans_SlNo,Sf_Code,Sf_Name,Activity_Date,Submission_Date,Work_Type,Plan_No,Plan_Name,Half_Day_FW,Start_Time,End_Time,Sys_Ip,Sys_Name, " +
                                                  " Division_Code,Remarks,Confirmed,ReasonforRejection,Emp_Id,Employee_Id,App_MGR,WorkType_Name,Entry_Mode,FieldWork_Indicator,getdate() from DCRMain_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Trans_SlNo.Tables[0].Rows[0][0].ToString() + "' ";                                                    
                            command.ExecuteNonQuery();

                            command.CommandText = " Insert into DCRDetail_Lst_Trans select * from DCRDetail_Lst_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Transno + "'";
                            command.ExecuteNonQuery();

                            command.CommandText = " Insert into DCRDetail_UnLst_Trans select * from DCRDetail_UnLst_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Transno + "'";
                            command.ExecuteNonQuery();

                            command.CommandText = " Insert into DCRDetail_CSH_Trans select * from DCRDetail_CSH_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Transno + "' ";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo ='" + Transno + "'  and Trans_SlNo in (select Trans_SlNo from DCRDetail_Lst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "'  )  ";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Transno + "' and Trans_SlNo in (select Trans_SlNo from DCRDetail_UnLst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "')  ";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo ='" + Transno + "' and Trans_SlNo in (select Trans_SlNo from DCRDetail_CSH_Trans where Sf_Code =  '" + sfcode + "'  and Trans_SlNo = '" + Transno + "')   ";
                            command.ExecuteNonQuery();

                            command.CommandText = " DELETE from DCRMain_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Transno + "' and Trans_SlNo in (select Trans_SlNo from DCRMain_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Transno + "')     ";
                            command.ExecuteNonQuery();

                        }
                        else
                        {
                      
                            //SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            //SqlCommand cmd3;
                          
                            //con3.Open();
                            //string sProc_Name2 = "";

                            //sProc_Name2 = "Leave_Approve_Latest";
                       
                            ////string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
                            ////SqlCommand cmd = new SqlCommand(sProc_Name2, con3);
                            //cmd3 = new SqlCommand(sProc_Name2, con3);
                            //cmd3.CommandType = CommandType.StoredProcedure;


                            //cmd3.Parameters.AddWithValue("@sf_code", sfcode);
                            //cmd3.Parameters.AddWithValue("@sf_type", sftype);
                            //cmd3.Parameters.AddWithValue("@sf_tyname", type_Name);
                            //cmd3.Parameters.AddWithValue("@Activity", From_Date);
                            //cmd3.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
                            //cmd3.Parameters.AddWithValue("@leave_id", Leave_Id);
                            //cmd3.CommandTimeout = 600;
                            //SqlDataAdapter da = new SqlDataAdapter(cmd3);
                            //DataSet dsts = new DataSet();
                            //da.Fill(dsts);
                            //con3.Close();
                            SqlConnection con6 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            con6.Open();
                            string work = " ;with cte as (select WorkType_Code_B as  code,Worktype_Name_B as  WName,'MR' sf_type,1 sftyp,FieldWork_Indicator,Division_Code " +
                                                  " from Mas_WorkType_BaseLevel where active_flag=0 and  Division_Code='" + div_code + "'   and FieldWork_Indicator='L' " +
                                                  " union " +
                                                  " select WorkType_Code_M as  code,Worktype_Name_M  as WName,'MGR' sf_type,2 sftyp,FieldWork_Indicator ,Division_Code " +
                                                  " from Mas_WorkType_Mgr  where active_flag=0      and Division_Code='" + div_code + "'   and FieldWork_Indicator='L' ) " +
                                                  " select WName,FieldWork_Indicator, code from cte " +
                                                  " where sf_type='" + type_Name + "' and sftyp='" + sftype + "' and FieldWork_Indicator='L' and Division_Code='" + div_code + "' ";
                            SqlCommand cmdwrk2;
                            cmdwrk2 = new SqlCommand(work, con6);

                            SqlDataAdapter dawork = new SqlDataAdapter(cmdwrk2);

                            dawork.Fill(dsWork);
                            con6.Close();

                            SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            con3.Open();
                            string Reason = "select Reason from mas_Leave_Form where Leave_Id='" + Leave_Id + "' and sf_code='" + sfcode + "' ";
                            SqlCommand cmdReason;
                            cmdReason = new SqlCommand(Reason, con3);

                            SqlDataAdapter dar = new SqlDataAdapter(cmdReason);

                            dar.Fill(dsremarks);
                            con3.Close();

                            SqlConnection con4 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                            con4.Open();
                            string max = "SELECT DivSH+cast(SFSlNo as varchar)+'-' as Sl,Cast(Max_Sl_No_Main as numeric)+1 as slNo FROM DCR_MaxSlNo where SF_Code='" + sfcode + "'";
                            SqlCommand cmdmax;
                            cmdmax = new SqlCommand(max, con4);

                            SqlDataAdapter dam = new SqlDataAdapter(cmdmax);

                            dam.Fill(dsmaxSlno);
                            con4.Close();
                            
                           

                            string maxNO = dsmaxSlno.Tables[0].Rows[0]["slNo"].ToString();
                            S_Max = Convert.ToInt32(maxNO) + i;
                                string Remarks = dsremarks.Tables[0].Rows[0]["Reason"].ToString();
                                string wtype = dsWork.Tables[0].Rows[0]["code"].ToString();
                                string Flag = dsWork.Tables[0].Rows[0]["FieldWork_Indicator"].ToString();
                                string Tran_sNO = dsmaxSlno.Tables[0].Rows[0]["Sl"].ToString() + "" + S_Max;
                                string Wname = dsWork.Tables[0].Rows[0]["Wname"].ToString();
                                string l_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;

                            command.CommandText = " insert into DCRMain_Temp (Trans_SlNo,Sf_Code,Sf_Name,Activity_Date,Submission_Date,Work_Type,Plan_No,Plan_Name,Half_Day_FW, " +
                                                  " Start_Time,End_Time,Sys_Ip,Sys_Name,Division_Code,Remarks,Confirmed,ReasonforRejection,Emp_Id, " +
                                                  " Employee_Id,App_MGR,WorkType_Name,Entry_Mode,FieldWork_Indicator) " +
                                                  " select '" + Tran_sNO + "',SF_Code,SF_Name,'" + l_From_Date + "',GETDATE(),'" + wtype + "','','','',GETDATE(),GETDATE(),'','','" + div_code + "','" + Remarks + "',1,'',sf_emp_id,Employee_Id,'','" + Wname + "','web','" + Flag + "' " +
                                                  " from Mas_Salesforce where sf_Code='" + sfcode + "'   ";
                            command.ExecuteNonQuery();


                            command.CommandText = " DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo =  '" + Tran_sNO + "'  and exists  " +
                                                   " (select * from DCRDetail_Lst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "')    ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Tran_sNO + "' and exists " +
                                                      " (select * from DCRDetail_UnLst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "')  ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Tran_sNO + "' and exists  " +
                                                      " (select * from DCRDetail_CSH_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "') ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRMain_Temp where Sf_Code ='" + sfcode + "'  and Trans_SlNo = '" + Tran_sNO + "' and exists " +
                                                      " (select * from DCRMain_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "') ";
                                command.ExecuteNonQuery();

                                command.CommandText = " Insert into DCRMain_Trans select Trans_SlNo,Sf_Code,Sf_Name,Activity_Date,Submission_Date,Work_Type,Plan_No,Plan_Name,Half_Day_FW,Start_Time,End_Time,Sys_Ip,Sys_Name, " +
                                                      " Division_Code,Remarks,Confirmed,ReasonforRejection,Emp_Id,Employee_Id,App_MGR,WorkType_Name,Entry_Mode,FieldWork_Indicator,getdate() from DCRMain_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "' ";
                                command.ExecuteNonQuery();

                                command.CommandText = " Insert into DCRDetail_Lst_Trans select * from DCRDetail_Lst_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "'";
                                command.ExecuteNonQuery();

                                command.CommandText = " Insert into DCRDetail_UnLst_Trans select * from DCRDetail_UnLst_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "'";
                                command.ExecuteNonQuery();

                                command.CommandText = " Insert into DCRDetail_CSH_Trans select * from DCRDetail_CSH_Temp where Sf_Code ='" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "' ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo ='" + Tran_sNO + "'  and Trans_SlNo in (select Trans_SlNo from DCRDetail_Lst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "'  )  ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Tran_sNO + "' and Trans_SlNo in (select Trans_SlNo from DCRDetail_UnLst_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "')  ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo ='" + Tran_sNO + "' and Trans_SlNo in (select Trans_SlNo from DCRDetail_CSH_Trans where Sf_Code =  '" + sfcode + "'  and Trans_SlNo = '" + Tran_sNO + "')   ";
                                command.ExecuteNonQuery();

                                command.CommandText = " DELETE from DCRMain_Temp where Sf_Code = '" + sfcode + "'  and Trans_SlNo = '" + Tran_sNO + "' and Trans_SlNo in (select Trans_SlNo from DCRMain_Trans where Sf_Code = '" + sfcode + "' and Trans_SlNo = '" + Tran_sNO + "')     ";
                                command.ExecuteNonQuery();



                            i++;
                        }
                        From_Date = From_Date.AddDays(1);

                    }
                    //   S_Max = S_Max + 1;
                    if (S_Max != 0)
                    {
                        command.CommandText = " update DCR_MaxSlNo set Max_Sl_No_Main='" + S_Max + "' where SF_Code='" + sfcode + "' ";
                        command.ExecuteNonQuery();
                    }
                    DateTime F_Date = Convert.ToDateTime(txtLeave.Text);
                    DateTime T_Date = Convert.ToDateTime(txtLeaveto.Text);

                    TimeSpan daydif = (T_Date - F_Date);
                    double dayd = (daydif.TotalDays) + 1;
                    string no_of_days = dayd.ToString();
                    if (sfcode.Contains("MR"))
                    {
                        dsMr = adm.getSetup_Leave_Ent_MR(div_code);
                        if (dsMr.Tables[0].Rows.Count > 0)
                        {
                            if (dsMr.Tables[0].Rows[0]["LE_MR"].ToString() == "Y")
                            {
                                command.CommandText =

                         " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + no_of_days + " " +
                          " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sfcode + "' and " +
                          " h.Sl_no =d.Sl_NO and Trans_Year=YEAR(GETDATE())  and Leave_Type_Code='" + ddltype.SelectedItem.Text.Trim() + "' and h.active_flag=0 ";

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (sfcode.Contains("MGR"))
                    {
                        dsMr = adm.getSetup_Leave_Ent_MGR(div_code);
                        if (dsMr.Tables[0].Rows.Count > 0)
                        {
                            if (dsMr.Tables[0].Rows[0]["LE_MGR"].ToString() == "Y")
                            {
                                command.CommandText =

                         " update Trans_Leave_Entitle_Details set leave_balance_days=leave_balance_days - " + no_of_days + " " +
                          " from Trans_Leave_Entitle_Head h, Trans_Leave_Entitle_Details d  where sf_code='" + sfcode + "' and " +
                          " h.Sl_no =d.Sl_NO and Trans_Year=YEAR(GETDATE())  and Leave_Type_Code='" + ddltype.SelectedItem.Text.Trim() + "' and h.active_flag=0 ";

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    command.CommandText =

                      " update mas_Leave_Form set Leave_Active_Flag = 0 ,LastUpdt_Date= getdate(), Leave_App_Mgr ='" + Session["sf_Name"].ToString() + "', mgr_code='" + Session["sf_code"].ToString() + "' " +
                   "  where sf_code = '" + sfcode + "' and Leave_Active_Flag=2  and Leave_Id = '" + Leave_Id + "'";


                    command.ExecuteNonQuery();
                    command.CommandText =

                         " update DCR_MissedDates set Status = 2, Missed_Release_Date = getdate(),Finished_Date= getdate(),Released_by_Whom='Admin(Saneforce)',Reason_for_Release='MD Released and overwritten as Leave' where division_code='" + div_code + "'  and Sf_Code = '" + sfcode + "' and convert(date,Dcr_Missed_Date) between convert(char(10),'" + F_Date.ToString("MM/dd/yyyy") + "',126) and convert(char(10),'" + T_Date.ToString("MM/dd/yyyy") + "',126) ";

                    command.ExecuteNonQuery();

                    command.CommandText =

                    " update DCR_Delay_Dtls set Delayed_Flag = 2, Delay_Release_Date = getdate(),Finished_Date= getdate(),Released_by_Whom='Admin(Saneforce)' where division_code='" + div_code + "'  and Sf_Code = '" + sfcode + "' and convert(date,Delayed_Date) between convert(char(10),'" + F_Date.ToString("MM/dd/yyyy") + "',126) and convert(char(10),'" + T_Date.ToString("MM/dd/yyyy") + "',126) ";

                    command.ExecuteNonQuery();
                    transaction.Commit();
                    if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
                    {

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../../MasterFiles/Leave_Admin_Approval.aspx';</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../../MasterFiles/MGR/MGR_Index.aspx';</script>");
                    }


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
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");

            }


            if (iReturn > 0)
            {
                //     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved Successfully');window.location='../MGR/MGR_Index.aspx';</script>");
             



            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        if ((Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3") && Request.QueryString["status"] != "")
        {
            Response.Redirect("~/MasterFiles/Leave_Admin_Approval.aspx");
        }
        else if (Request.QueryString["status"] == "")
        {
            Response.Redirect("~/MasterFiles/MR/Leave_Status.aspx");
        }
        else
        {
            Response.Redirect("~/MasterFiles/MGR/MGR_Index.aspx");
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            txtreject.Visible = true;
            btnSubmit.Visible = true;
            btnReject.Visible = false;

            btnApproved.Visible = false;
            lblRejectReason.Visible = true;
            txtreject.Focus();
        }
        catch (Exception ex)
        {

        }

    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    int iReturn = -1;
    //    sfcode = Request.QueryString["sfcode"];
    //    Leave_Id = Request.QueryString["Leave_Id"].ToString();
    //    if (sfcode != null)
    //    {
    //        AdminSetup adm = new AdminSetup();
    //        iReturn = adm.Leave_Reject_Mgr(sfcode, Session["sf_Name"].ToString(), Leave_Id, txtreject.Text);
    //        if (iReturn > 0)
    //        {
    //            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
    //            {
    //                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../MasterFiles/Leave_Admin_Approval.aspx';</script>");
    //            }
    //            else
    //            {
    //                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../MasterFiles/MGR/MGR_Index.aspx';</script>");
    //            }

    //        }
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        sfcode = Request.QueryString["sfcode"];
        Leave_Id = Request.QueryString["Leave_Id"].ToString();
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        DateTime dtFromTo = new DateTime();
        string From_date = "";
        string to_date = "";
        string all_dates = string.Empty;
        if (sfcode != null)
        {
            AdminSetup adm = new AdminSetup();
            dsAdminSetup = adm.GetLeaveDates(div_code, sfcode, Leave_Id);

            if (dsAdminSetup.Tables[0].Rows.Count > 0)
            {

                dtFrom = Convert.ToDateTime(dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                From_date = dtFrom.Month + "-" + dtFrom.Day + "-" + dtFrom.Year;
                dtTo = Convert.ToDateTime(dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());
                to_date = dtTo.Month + "-" + dtTo.Day + "-" + dtTo.Year;
                dsTerritory = adm.getAllDaysBetweenTwoDate(From_date, to_date);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTerritory.Tables[0].Rows)
                    {
                        dtFromTo = Convert.ToDateTime(dr["AllDays"].ToString());
                        string dates = dtFromTo.Month + "-" + dtFromTo.Day + "-" + dtFromTo.Year;

                        all_dates += dates + "','";
                        //   strSf_Code = strSf_Code.Replace(SfCode + ',',''',''');
                        //strSf_Code += replace(SfCode,',',''','''); 
                    }
                    all_dates = "'" + all_dates;
                    all_dates = all_dates.Remove(all_dates.Length - 2);
                    DataSet dslev = new DataSet();
                    AdminSetup ad = new AdminSetup();
                    dslev = ad.Max_activity_date(sfcode, div_code);
                    using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = connection.CreateCommand();
                        SqlTransaction transaction;

                        // Start a local transaction.
                        transaction = connection.BeginTransaction("SampleTransaction");

                        // Must assign both transaction object and connection
                        // to Command object for a pending local transaction
                        command.Connection = connection;
                        command.Transaction = transaction;

                        try
                        {
                            command.CommandText =

                                 " update mas_Leave_Form set Leave_Active_Flag = 1 ,LastUpdt_Date= getdate(), Leave_App_Mgr ='" + Session["sf_Name"].ToString() + "', Rejected_Reason = '" + txtreject.Text + "' " +
                                  " where sf_code = '" + sfcode + "' and Leave_Active_Flag=2 and Leave_Id='" + Leave_Id + "' ";

                            command.ExecuteNonQuery();
                            command.CommandText =

                                 " update DCRMain_Temp set confirmed = 2, ReasonforRejection= '" + txtreject.Text + "' where Activity_Date in ( " + all_dates + " ) and Sf_Code = '" + sfcode + "' ";

                            command.ExecuteNonQuery();

                        

                         
                            transaction.Commit();
                            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../MasterFiles/Leave_Admin_Approval.aspx';</script>");
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');window.location='../../MasterFiles/MGR/MGR_Index.aspx';</script>");
                            }

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
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
                    }



                }
            }

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtLeave.Text = "";
        txtLeaveto.Text = "";
        lblDaysCount.Text = "";
        txtreason.Text = "";

        txtAddr.Text = "";
        ddltype.SelectedValue = "0";
        //lblValidreason.Text = "";
    }
    protected void btnleavedcr_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/DCR/DCR_Entry.aspx");
    }
}
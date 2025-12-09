using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Collections;
public partial class MasterFiles_MGR_Leave_Form_Mgr : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    DataSet dsAdminSetup = null;
    string sf_code = string.Empty;
    string sfcode = string.Empty;
    string div_code = string.Empty;
    int request_type = -1;
    string Leave_code = string.Empty;
    string Leave_Id = string.Empty;
    DataSet dsLeave = new DataSet();
    DataSet dsMgr = new DataSet();
    DataTable lv = new DataTable();
    DataSet dsMr = new DataSet();
    DataSet dsDCR = new DataSet();
    DataSet dsSal = new DataSet();
    string sf_emp_id = string.Empty;
    string mgr_code = string.Empty;
    string mgr_emp_id = string.Empty;
    string state_code = string.Empty;
    string EntryMode = string.Empty;
    string Sf_Name = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfcode = Session["sf_code"].ToString();
        hHeading.InnerText = Page.Title;
        if (!Page.IsPostBack)
        {
            menu1.Visible = true;
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            FillLeaveType();
            GetHQ();
            AdminSetup adm = new AdminSetup();
            //dsAdminSetup = adm.FillLeave_Type(div_code);
            //ddltype.DataTextField = "Leave_SName";
            //ddltype.DataValueField = "Leave_code";
            //ddltype.DataSource = dsAdminSetup;
            //ddltype.DataBind();
            dsMgr = adm.getSetup_Leave_Ent_MGR(div_code);
            if (dsMgr.Tables[0].Rows.Count > 0)
            {
                if (dsMgr.Tables[0].Rows[0]["LE_MGR"].ToString() == "Y")
                {
                    Pnlent.Visible = true;

                    LeaveBalance_Det();
                }
                else
                {
                    Pnlent.Visible = false;
                }
            }
        }
        if (Request.QueryString["LeaveFrom"] != null)
        {
            // menu1.Visible = false;
            //  pnlleaveback.Visible = true;
            //  pnltit.Visible = true;
            string Leavedate = Request.QueryString["LeaveFrom"];
            txtLeave.Text = Leavedate.Substring(3, 2) + "/" + Leavedate.Substring(0, 2) + "/" + Leavedate.Substring(6, 4);
            Leavedate = "";
            txtLeave.Enabled = false;
            // imgPopup.Enabled = false;
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
                sfcode = Session["sf_code"].ToString();
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
        AdminSetup adm = new AdminSetup();
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
    private void GetHQ()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getSfname_Desig(sfcode, div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            lblemp.Text =
                 "<span style='font-weight: bold;color:BlueViolet; font-size:12px;font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString() + "</span>";
            //Session["Sf_Name"] = dsTerritory.Tables[0].Rows[0]["Sf_Name"].ToString();
            lbldesig.Text =
               "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Designation_Name"].ToString() + "</span>";
            lblSfhq.Text =
                "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri '>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";
            lblempcode.Text =
                   "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["sf_emp_id"].ToString() + "</span>";

            lbldivi.Text =
                   "<span style='font-weight: bold;color:BlueViolet; font-size:12px; font-names:Calibri'>  " + dsTerritory.Tables[0].Rows[0]["Division_Name"].ToString() + "</span>";


        }
    }
    protected void txtLeaveto_TextChanged(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
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
                lblDaysCount.Style.Add("color", "Red");
                lblDaysCount.Style.Add("Font-Size", "16px");
            }
        }
        //if (Session["sf_type"].ToString() == "1")
        //{
        //    sfcode = Session["sf_code"].ToString();
        //}
        //else
        //{
        //    sfcode = Request.QueryString["sfcode"];
        //}
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
        dsMr = adm.getSetup_Leave_Ent_MGR(div_code);
        if (dsMr.Tables[0].Rows.Count > 0)
        {
            if (dsMr.Tables[0].Rows[0]["LE_MGR"].ToString() == "Y")
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
            AdminSetup adm = new AdminSetup();
            DataSet dsMgr = new DataSet();
            dsMgr = adm.getSetup_Leave_Ent_MGR(div_code);
            if (dsMgr.Tables[0].Rows.Count > 0)
            {
                if (dsMgr.Tables[0].Rows[0]["LE_MGR"].ToString() == "Y")
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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        SalesForce sf = new SalesForce();
        DateTime dold = Convert.ToDateTime(txtLeave.Text);
        DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
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
                    AdminSetup adm = new AdminSetup();
                    iReturn = adm.Insert_Leave_Mgr(ddltype.SelectedValue, Convert.ToDateTime(txtLeave.Text), Convert.ToDateTime(txtLeaveto.Text), txtreason.Text, txtAddr.Text, lblDaysCount.Text, chkmanager.SelectedValue, "", sfcode, div_code, chkho.SelectedValue, ddltype.SelectedItem.Text.Trim(), sf_emp_id, mgr_code, mgr_emp_id, EntryMode, Sf_Name);
                    if (iReturn > 0)
                    {
                        if (Request.QueryString["LeaveFrom"] != null)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='../../DCR/DCR_Entry.aspx';</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                            LeaveBalance_Det();
                        }
                    }
                }
            }
        }

        txtAddr.Text = "";
        txtLeaveto.Text = "";
        txtLeave.Text = "";
        txtreason.Text = "";
        ddltype.SelectedIndex = -1;
        lblDaysCount.Text = "";
    }
    public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
    {
        List<DateTime> allDates = new List<DateTime>();
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            allDates.Add(date);
        return allDates;

    }


}
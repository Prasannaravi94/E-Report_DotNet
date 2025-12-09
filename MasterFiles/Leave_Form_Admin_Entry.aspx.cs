using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Collections;
public partial class MasterFiles_Leave_Form_Admin_Entry : System.Web.UI.Page
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
    DataSet dsLeave = new DataSet();
    string Reporting_to = string.Empty;
    DataSet dsSal = new DataSet();
    string sf_emp_id = string.Empty;
    string mgr_code = string.Empty;
    string mgr_emp_id = string.Empty;
    string state_code = string.Empty;
    string EntryMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsDCR = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfcode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Visible = true;
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            FillManagers();
         
        }
        //if (Request.QueryString["LeaveFrom"] != null)
        //{
        //    // menu1.Visible = false;
        //    //  pnlleaveback.Visible = true;
        //    //  pnltit.Visible = true;
        //    string Leavedate = Request.QueryString["LeaveFrom"];
        //    txtLeave.Text = Leavedate.Substring(3, 2) + "/" + Leavedate.Substring(0, 2) + "/" + Leavedate.Substring(6, 4);
        //    Leavedate = "";
        //    txtLeave.Enabled = false;
        //    imgPopup.Enabled = false;
        //}

    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.Hierarchy_Team(div_code, "admin");
        
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";        
            ddlFieldForce.DataSource = dsSalesForce;         
            ddlFieldForce.DataBind();
            foreach (ListItem item in ddlFieldForce.Items)
            {
                if (item.Text.ToLower() == "admin")
                {
                    ddlFieldForce.Items.Remove(item);
                    break;
                }
            }
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
        dsAdminSetup = adm.Get_Leave_Bal(div_code, ddlFieldForce.SelectedValue.Trim());
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
                if (dsAdminSetup.Tables[0].Rows[i]["Leave_Type_Code"].ToString() == "TL")
                {
                    lblTLL.Text = dsAdminSetup.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblBTL.Text = dsAdminSetup.Tables[0].Rows[i]["Leave_Balance_Days"].ToString();
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
        if (lblBTL.Text == "" || lblBTL.Text == "NULL")
        {
            lblBTL.Text = "0";
        }
        AdminSetup ad = new AdminSetup();
        dsLeave = ad.Leave_App_Pending_Days(div_code, ddlFieldForce.SelectedValue.Trim());
        if (dsLeave.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsLeave.Tables[0].Rows.Count; i++)
            {
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "CL")
                {
                    //  lblACL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    c_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());

                    lblACL.Text = Convert.ToString(c_cnt);
                }
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "PL")
                {
                    // lblAPL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
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
                    // lblALL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lo_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());

                    lblALL.Text = Convert.ToString(lo_cnt);
                }
                if (dsLeave.Tables[0].Rows[i]["Leave_SName"].ToString() == "TL")
                {
                    t_cnt += Convert.ToInt32(dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString());
                    // lblALL.Text = dsLeave.Tables[0].Rows[i]["No_Of_Days"].ToString();
                    lblATL.Text = Convert.ToString(t_cnt);
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
        if (lblATL.Text == "" || lblATL.Text == "NULL")
        {
            lblATL.Text = "0";
        }

    }
    private void GetHQ()
    {

        Territory terr = new Territory();
        dsTerritory = terr.getSfname_Desig(ddlFieldForce.SelectedValue.Trim(), div_code);
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
            lblDaysCount.Text = dayd.ToString();
            lblDaysCount.Style.Add("color", "Red");
            lblDaysCount.Style.Add("Font-Size", "14px");
            DataTable lv = new DataTable();
            AdminSetup adm1 = new AdminSetup();

            Territory terr = new Territory();
            dsTerritory = terr.getSfname_Desig(ddlFieldForce.SelectedValue.Trim(), div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                state_code = dsTerritory.Tables[0].Rows[0]["state_code"].ToString();
            }

         
        }
           
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        if (txtLeaveto.Text != "")
        {
            ds = dcr.getLeave_Mr(ddlFieldForce.SelectedValue.Trim(), Convert.ToDateTime(txtLeaveto.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
                txtLeaveto.Text = "";
                txtLeaveto.Focus();
            }
        }
    }
    protected void ddltype_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txtLeave.Text = "";
        txtLeaveto.Text = "";
        lblDaysCount.Text = "";
    }
    protected void txtLeave_TextChanged(object sender, EventArgs e)
    {
        txtLeaveto.Text = "";
        lblDaysCount.Text = "";
      
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
            lblDaysCount.Text = dayd.ToString();
            lblDaysCount.Style.Add("color", "Red");
            lblDaysCount.Style.Add("Font-Size", "14px");


        }
      
        DCR dcr = new DCR();
        DataSet ds = new DataSet();
        if (txtLeave.Text != "")
        {
            ds = dcr.getLeave_Mr(ddlFieldForce.SelectedValue.Trim(), Convert.ToDateTime(txtLeave.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave already applied for the selected Date');</script>");
                txtLeave.Text = "";
                txtLeave.Focus();
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
        lblValidreason.Text = "";
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DateTime dold = Convert.ToDateTime(txtLeave.Text);
        DateTime dnew = Convert.ToDateTime(txtLeaveto.Text);
        int iReturn = -1;
        SalesForce sf = new SalesForce();
        dsSal = sf.Leave_Sf_Emp_id(ddlFieldForce.SelectedValue.Trim());
        if (dsSal.Tables[0].Rows.Count > 0)
        {
            sf_emp_id = dsSal.Tables[0].Rows[0]["sf_emp_id"].ToString();
            mgr_code = dsSal.Tables[0].Rows[0]["mgr_code"].ToString();
            mgr_emp_id = dsSal.Tables[0].Rows[0]["mgr_emp_id"].ToString();
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
                ds = dcr.getLeave_Mr(ddlFieldForce.SelectedValue.Trim(), Convert.ToDateTime(dates[i]));


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
              
            }
            else
            {
                DCR dc = new DCR();

                dsDCR = dc.get_All_dcr_Date_Leave(ddlFieldForce.SelectedValue.Trim(), div_code, dold.ToString("MM/dd/yyyy"), dnew.ToString("MM/dd/yyyy"));
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
                    iReturn = adm.Insert_Leave_admin_Entry(ddltype.SelectedValue, Convert.ToDateTime(txtLeave.Text), Convert.ToDateTime(txtLeaveto.Text), txtreason.Text, txtAddr.Text, lblDaysCount.Text, chkmanager.SelectedValue, lblValidreason.Text, ddlFieldForce.SelectedValue.Trim(), div_code, chkho.SelectedValue, ddltype.SelectedItem.Text.Trim(), sf_emp_id, mgr_code, mgr_emp_id, EntryMode);
                    if (iReturn > 0)
                    {

                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                        txtAddr.Text = "";
                        txtLeave.Text = "";
                        txtLeaveto.Text = "";
                        txtreason.Text = "";
                        ddltype.SelectedIndex = -1;
                        lblDaysCount.Text = "";
                    }

                    pnlLeave.Visible = false;
                }
            }
        }
    }
    public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
    {
        List<DateTime> allDates = new List<DateTime>();
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            allDates.Add(date);
        return allDates;

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        pnlLeave.Visible = true;
        GetHQ();
        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.FillLeave_Type(div_code);
        ddltype.DataTextField = "Leave_SName";
        ddltype.DataValueField = "Leave_code";
        ddltype.DataSource = dsAdminSetup;
        ddltype.DataBind();
        LeaveBalance_Det();
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlLeave.Visible = false;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;


public partial class MasterFiles_Stockist_Creation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsReport = null;
    string stockist_code = string.Empty;
    string Sale_Entry = string.Empty;
    int iReturn_FM = -1;
    string divcode = string.Empty;
    string SF_Name = string.Empty;
    string stockist_name = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobilno = string.Empty;
    string scheck = string.Empty;
    string Territory = string.Empty;
    string State = string.Empty;
    string PoolStatus = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sChkSalesforce = string.Empty;
    //int sChkSalesforce = -1;
    string ReportingMGR = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "StockistList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        stockist_code = Request.QueryString["Stockist_Code"];


        if (!Page.IsPostBack)
        {
            FillReporting();
            Get_State();
            // GetPoolName();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            GetHQ();
            if (stockist_code != "" && stockist_code != null)
            {
                Stockist sk = new Stockist();
                dsStockist = sk.getStockist_Create(divcode, stockist_code);

                if (dsStockist.Tables[0].Rows.Count > 0)
                {
                    txtStockist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtStockist_Address.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtStockist_ContactPerson.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtStockist_Desingation.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtStockist_Mobile.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    string State = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();

                    if (State == "" || State == "---Select---")
                    {
                        State = "---Select---";
                        ddlState.Items.FindByText(State).Selected = true;
                    }
                    else
                    {
                        var s = ddlState.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(State, StringComparison.InvariantCultureIgnoreCase));
                        ddlState.Items.FindByText(s.ToString()).Selected = true;
                    }

                    Stockist ss = new Stockist();
                    //  DataSet ds = ss.Get_StateWise_HQ(State, divcode);

                    DataSet ds = ss.Get_Statewise_HQ_Det(State, divcode);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlPoolName.DataTextField = "Pool_Name";
                        ddlPoolName.DataValueField = "Pool_Id";
                        ddlPoolName.DataSource = ds;
                        ddlPoolName.DataBind();
                    }

                    string HQName = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                    if (HQName == "" || HQName == "--Select--")
                    {
                        HQName = "--Select--";
                        ddlPoolName.Items.FindByText(HQName).Selected = true;
                    }
                    else
                    {
                        ListItem item = ddlPoolName.Items.FindByText(HQName);

                        if (item != null)
                        {
                            ddlPoolName.Items.FindByText(HQName).Selected = true;
                        }
                    }

                    sf_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                    HidStockistCode.Value = stockist_code;
                }

            }
            FillCheckBoxList();

            string[] Salesforce;
            if (sf_code != "")
            {
                iIndex = -1;
                Salesforce = sf_code.Split(',');
                foreach (string sf in Salesforce)
                {
                    foreach (DataListItem cb in DataList1.Items)
                    {

                        CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
                        HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

                        if (sf == hf.Value)
                        {
                            //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                            chk.Checked = true;
                            //chk.Attributes.Add("style", "color:#ff0000;font-weight:bold;font-size:14px;");
                            chk.Attributes.Add("style", "color:Red;font-weight:bold;font-size:14px;");
                        }

                    }
                }
            }

            // Resetall();
        }
    }

    private void Get_State()
    {
        Division dv = new Division();
        DataSet dsDivision;
        dsDivision = dv.getStatePerDivision(divcode);
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
            DataSet dsState;
            dsState = st.getStateChkBox(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("---Select---", "0"));

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
    private void FillCheckBoxList()//changes done by resh in query
    {
        string strcomma = string.Empty;
        string strName = string.Empty;
        string[] str;
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistCheck(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
            ViewState["dsStockist"] = dsStockist;
        for (int i = 0; i <= dsStockist.Tables[0].Rows.Count - 1; i++)
        {
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                //string str1 = "";
                //strName = dsStockist.Tables[0].Rows[i]["rep_hq"].ToString() + ",";
                //strcomma = strcomma + strName;
                //str = strcomma.Split(',');
                //string[] b = str.Distinct().ToArray();
                //foreach (string s in b)
                //{
                //    //Label ProductNameLabel = (Label)DataList1.FindControl("ProductNameLabel");
                //    //ProductNameLabel.Text = s.ToString();
                //}
                // DataList1.DataSource = dsStockist;
                // DataList1.DataBind();
            }


        }


    }


    private void FillReporting()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Reporting(divcode);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "Sf_Name";
            ddlFilter.DataValueField = "Sf_Code";
            //  ddlFilter.BackColor = System.Drawing.Color.Crimson; 
            ddlFilter.DataSource = dsStockist;
            ddlFilter.DataBind();

        }

    }
    private void FillStockist_Reporting()
    {

        string sReport = ddlFilter.SelectedValue.ToString();
        Stockist sk = new Stockist();

        dsStockist = sk.getStockist_Filter(divcode, sReport);
        for (int i = 0; i < dsStockist.Tables[0].Rows.Count; i++)
        {
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                GetHQ_Filter();
            }
        }

        // Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Create(divcode, stockist_code);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            txtStockist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txtStockist_Address.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtStockist_ContactPerson.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtStockist_Desingation.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtStockist_Mobile.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            // ddlPoolName.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

            string HQName = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

            if (HQName == "")
            {
                HQName = "--Select--";
                ddlPoolName.Items.FindByText(HQName).Selected = true;
            }
            else
            {
                ddlPoolName.Items.FindByText(HQName).Selected = true;
            }

            sf_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

            // ddlState.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();


            string State = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();

            if (State == "")
            {
                State = "---Select---";
                ddlState.Items.FindByText(State).Selected = true;
            }
            else
            {
                var s = ddlState.Items.Cast<ListItem>().FirstOrDefault(i => i.Text.Equals(State, StringComparison.InvariantCultureIgnoreCase));
                ddlState.Items.FindByText(s.ToString()).Selected = true;
            }

            //ddlPlStatus.SelectedValue = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            HidStockistCode.Value = stockist_code;
        }
        string[] Salesforce;
        if (sf_code != "")
        {
            iIndex = -1;
            Salesforce = sf_code.Split(',');
            foreach (string sf in Salesforce)
            {
                foreach (DataListItem cb in DataList1.Items)
                {
                    CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
                    HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

                    if (sf == hf.Value)
                    {
                        //sChkSalesforce = sChkSalesforce + hf.Value + ",";
                        chk.Checked = true;
                        //chk.Attributes.Add("style", "color:#ff0000;font-weight:bold;font-size:14px;");
                        chk.Attributes.Add("style", "color:Red;font-weight:bold;font-size:14px;");
                    }

                }
            }
        }

    }

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (ddlFilter.SelectedIndex > 0)
        {
            FillStockist_Reporting();
        }
        else
        {
            FillCheckBoxList();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        //for (int i = 0; i < chkboxSalesforce.Items.Count; i++)
        //{
        //    if (chkboxSalesforce.Items[i].Selected)
        //    {
        //        sChkSalesforce = sChkSalesforce + chkboxSalesforce.Items[i].Value + ",";
        //        // chkboxSalesforce.Items[i].Attributes.Add("style", "Color: Red");
        //    }

        //}

        foreach (DataListItem cb in DataList1.Items)
        {
            CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
            HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

            if (chk.Checked)
            {
                sChkSalesforce = sChkSalesforce + hf.Value + ",";

            }
        }

        stockist_code = Request.QueryString["Stockist_Code"];
        stockist_name = txtStockist_Name.Text;
        stockist_Address = txtStockist_Address.Text;
        stockist_ContactPerson = txtStockist_ContactPerson.Text;
        stockist_Designation = txtStockist_Desingation.Text;
        stockist_mobilno = txtStockist_Mobile.Text;
        PoolStatus = "0";

        string hqPool = hdnPoolName.Value;

        Territory = hqPool;

        State = ddlState.SelectedItem.Text;

        Stockist St = new Stockist();
        DataSet dsStockist_Admin = St.getStockist_Allow_Admin(divcode);
        DataSet dsStockist_Count = St.getStockist_Count(divcode);

        if (dsStockist_Count.Tables[0].Rows[0][0].ToString() == dsStockist_Admin.Tables[0].Rows[0][0].ToString() && stockist_code == null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Cannot enter more than " + dsStockist_Admin.Tables[0].Rows[0][0].ToString() + " Stockist');", true);
        }
        else
        {
            if (stockist_code == null)
            {
                // Add new Stockist Details 
                Stockist sk = new Stockist();
                int iReturn = sk.RecordAdd(divcode, sChkSalesforce, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobilno, Territory, PoolStatus, State);
                if (iReturn > 0)
                {
                    foreach (DataListItem cb in DataList1.Items)
                    {
                        //  CheckBox chksales;
                        //CheckBox cb = li.FindControl("chkCategoryNameLabel") as CheckBox;
                        //HiddenField cb1 = li.FindControl("cbTestID") as HiddenField;

                        CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");
                        HiddenField hf = (HiddenField)cb.FindControl("cbTestID");

                        if (chk.Checked)
                        {
                            sChkSalesforce = hf.Value;
                            //   chkboxSalesforce.Items[i].Attributes.Add("style", "Color: Red");

                            stockist_code = Request.QueryString["Stockist_Code"];
                            stockist_name = txtStockist_Name.Text;
                            stockist_Address = txtStockist_Address.Text;
                            stockist_ContactPerson = txtStockist_ContactPerson.Text;
                            stockist_Designation = txtStockist_Desingation.Text;
                            stockist_mobilno = txtStockist_Mobile.Text;
                            Territory = ddlPoolName.SelectedItem.Text;
                            State = ddlState.SelectedItem.Text;
                            // PoolStatus = ddlPlStatus.SelectedValue;
                            iReturn_FM = sk.RecordAdd_FM(divcode, iReturn, stockist_name, sChkSalesforce, Sale_Entry);

                            // menu1.Status = "Stockist Created Successfully";
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                            Resetall();
                        }
                    }


                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "Stockist already Exist!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                }
            }
            else
            {
                // Update Stockist Details
                Stockist sk = new Stockist();
                int iReturn = sk.RecordUpdate(divcode, stockist_code, sChkSalesforce, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobilno, Territory, PoolStatus, State);

                if (iReturn > 0)
                {
                    //menu1.Status = "Stockist updated Successfully";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='StockistList.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "Stockist exist with the same stockist name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist with the Same Name');</script>");
                }
            }
        }
    }

    private void GetPoolName()
    {
        Stockist sk = new Stockist();
        string Sf_Code = "admin";

        string StateName = ddlState.SelectedItem.Text;

        dsStockist = sk.Get_HQ_Detail(divcode, Sf_Code, StateName);

        // dsStockist = sk.getPool_Name(divcode, Sf_Code);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlPoolName.DataTextField = "Pool_Name";
            ddlPoolName.DataValueField = "Pool_Id";
            ddlPoolName.DataSource = dsStockist;
            ddlPoolName.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSubmit_Click(sender, e);
    }

    private void Resetall()
    {
        txtStockist_Name.Text = "";
        txtStockist_Address.Text = "";
        txtStockist_ContactPerson.Text = "";
        txtStockist_Desingation.Text = "";
        txtStockist_Mobile.Text = "";
        //ddlPlStatus.SelectedIndex = -1;
        ddlPoolName.SelectedIndex = -1;
        ddlState.SelectedIndex = -1;

        foreach (DataListItem cb in DataList1.Items)
        {
            CheckBox chk = (CheckBox)cb.FindControl("chkCategoryNameLabel");

            chk.Checked = false;
        }

    }

    //protected void ddlPlStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlPlStatus.SelectedValue == "0")
    //    {
    //        ddlPoolName.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlPoolName.Enabled = false;
    //    }
    //}


    public void GetHQ()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt.Columns.Add("sf_Name");
        dt.Columns.Add("Reporting_To_SF");
        dt.Columns.Add("rep_hq");
        dt.Columns.Add("sf_code");
        DataRow dr;
        string hq = "";
        Stockist sk = new Stockist();
        ds = sk.getStockistCheck(divcode);

        //  DataSet ds = GetDbData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            hq = ds.Tables[0].Rows[0]["rep_hq"].ToString();
            dr = dt.NewRow();
            dr[0] = ds.Tables[0].Rows[0]["sf_Name"].ToString();
            dr[1] = ds.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
            dr[2] = ds.Tables[0].Rows[0]["rep_hq"].ToString();
            dr[3] = ds.Tables[0].Rows[0]["sf_code"].ToString();
            dt.Rows.Add(dr);
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {

                dr = dt.NewRow();

                dr[0] = ds.Tables[0].Rows[i][1];
                dr[3] = ds.Tables[0].Rows[i][0];
                //  dr[1] = ds.Tables[0].Rows[i][1];

                if (hq == ds.Tables[0].Rows[i][5].ToString())
                {

                    dr[2] = "";

                }
                else
                {

                    dr[2] = ds.Tables[0].Rows[i][5];

                    hq = ds.Tables[0].Rows[i][5].ToString();

                }
                //dt.Columns.Add("SF_Name");
                //dt.Columns.Add("sf_code");
                dt.Rows.Add(dr);

            }
            DataList1.DataSource = dt;
            DataList1.DataBind();

        }

    }
    public void GetHQ_Filter()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt.Columns.Add("sf_Name");
        dt.Columns.Add("Reporting_To_SF");
        dt.Columns.Add("rep_hq");
        dt.Columns.Add("sf_code");
        DataRow dr;
        string hq = "";
        string sReport = ddlFilter.SelectedValue.ToString();
        Stockist sk = new Stockist();
        ds = sk.getStockist_Filter(divcode, sReport);

        //  DataSet ds = GetDbData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            hq = ds.Tables[0].Rows[0]["rep_hq"].ToString();
            dr = dt.NewRow();
            dr[0] = ds.Tables[0].Rows[0]["sf_Name"].ToString();
            dr[1] = ds.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
            dr[2] = ds.Tables[0].Rows[0]["rep_hq"].ToString();
            dr[3] = ds.Tables[0].Rows[0]["sf_code"].ToString();
            dt.Rows.Add(dr);
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {

                dr = dt.NewRow();

                dr[0] = ds.Tables[0].Rows[i][1];
                dr[3] = ds.Tables[0].Rows[i][0];
                //  dr[1] = ds.Tables[0].Rows[i][1];

                if (hq == ds.Tables[0].Rows[i][5].ToString())
                {
                    dr[2] = "";
                }
                else
                {
                    dr[2] = ds.Tables[0].Rows[i][5];
                    hq = ds.Tables[0].Rows[i][5].ToString();
                }

                //dt.Columns.Add("SF_Name");
                //dt.Columns.Add("sf_code");
                dt.Rows.Add(dr);

            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }

    }


    protected void btnHq_Click(object sender, EventArgs e)
    {
        Stockist Sk = new Stockist();
        string Sf_Code = "admin";

        string State_Name = ddlState.SelectedItem.Text;

        // int iReturn = Sk.Add_Stockist_HQ(divcode, txtPool_Sname.Text, txtPool_Name.Text, Sf_Code);

        int iReturn = Sk.Create_Stockist_HQ(divcode, txtPool_Sname.Text, txtPool_Name.Text, Sf_Code, State_Name);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            Reset_HQ();
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('HQ Name Already Exist');</script>");
            txtPool_Name.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('HQ Short Name Already Exist');</script>");
            txtPool_Sname.Focus();
        }

        GetPoolName();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StateName = ddlState.SelectedItem.Text;

        Stockist ss = new Stockist();
        DataSet ds = ss.Get_Statewise_HQ_Det(StateName, divcode);

        ddlPoolName.Items.Clear();
        // ddlPoolName.Items.Insert(0, new ListItem("--Select--", "0"));

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlPoolName.Items.Clear();
            ddlPoolName.DataTextField = "Pool_Name";
            ddlPoolName.DataValueField = "Pool_Id";
            ddlPoolName.DataSource = ds;
            ddlPoolName.DataBind();
            //  ddlPoolName.Items.Insert(0, "--Select--");
        }
    }

    private void Reset_HQ()
    {
        txtPool_Sname.Text = "";
        txtPool_Name.Text = "";
    }




    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("StockistList.aspx");
    }
}

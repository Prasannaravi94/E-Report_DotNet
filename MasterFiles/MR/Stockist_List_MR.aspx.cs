using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Text.RegularExpressions;

public partial class MasterFiles_MR_Stockist_List_MR : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsTerritory = null;
    string divcode = string.Empty;
    string stockist_code = string.Empty;
    string stockist_name = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobileno = string.Empty;
    string Territory = string.Empty;
    string State = string.Empty;
    string sf_code = string.Empty;
    string sCmd = string.Empty;
    string sChkSalesforce = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string S_Name = string.Empty;
    string PoolStatus = string.Empty;
    string val = string.Empty;

    string Sale_Entry = string.Empty;
    int iReturn_FM = -1;

    int time;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            Session["Char"] = "All";
            //Session["Char"] = 1;
            FillStockist();
            FillSF_Alpha();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            getWorkName();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            grdStockist.Visible = true;

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

    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            ddlStockist.Items.Add(new ListItem(str, "Territory Name", true));
            //CblDoctorCode.Items.Add(new ListItem(dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString(), "Territory_Code", true));

        }
    }


    protected void grdStockist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow && grdStockist.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlHQName = (e.Row.FindControl("ddlHQ") as DropDownList);

            Stockist sk = new Stockist();
            DataSet dsStockist;


            dsStockist = sk.getPool_Name(divcode, sf_code);

            ddlHQName.DataSource = dsStockist;
            ddlHQName.DataTextField = "Pool_Name";
            ddlHQName.DataValueField = "Pool_Id";
            ddlHQName.DataBind();

            string HQName = (e.Row.FindControl("lblHQName") as Label).Text;

            if (HQName == "")
            {
                HQName = "--Select--";
                ddlHQName.Items.FindByText(HQName).Selected = true;
            }

            else if (HQName != "")
            {
                ListItem item = ddlHQName.Items.FindByText(HQName);

                if (item != null)
                {
                    ddlHQName.Items.FindByText(HQName).Selected = true;
                }
            }

            DropDownList ddlState = (e.Row.FindControl("ddlState") as DropDownList);

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


                string State = (e.Row.FindControl("lblState") as Label).Text;

                //if (State == "")
                //{
                //    State = "---Select---";
                //}

                //ddlState.Items.FindByText(State).Selected = true;

                if (State == "")
                {
                    State = "---Select---";
                    ddlState.Items.FindByText(State).Selected = true;
                }

                else if (State != "")
                {
                    ListItem item = ddlState.Items.FindByText(State);

                    if (item != null)
                    {
                        ddlState.Items.FindByText(State).Selected = true;
                    }
                }
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlHQName = (e.Row.FindControl("ddlHQ_Foot") as DropDownList);

            Stockist sk = new Stockist();
            DataSet dsStockist;

            dsStockist = sk.getPool_Name(divcode, sf_code);

            ddlHQName.DataSource = dsStockist;
            ddlHQName.DataTextField = "Pool_Name";
            ddlHQName.DataValueField = "Pool_Id";
            ddlHQName.DataBind();

            //string HQName = (e.Row.FindControl("lblHQName") as Label).Text;

            //if (HQName == "")
            //{
            //    HQName = "--Select--";
            //    ddlHQName.Items.FindByText(HQName).Selected = true;
            //}

            //else if (HQName != "")
            //{
            //    ListItem item = ddlHQName.Items.FindByText(HQName);

            //    if (item != null)
            //    {
            //        ddlHQName.Items.FindByText(HQName).Selected = true;
            //    }
            //}

            DropDownList ddlState = (e.Row.FindControl("ddlState_Foot") as DropDownList);

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


                //string State = (e.Row.FindControl("lblState") as Label).Text;

                //if (State == "")
                //{
                //    State = "---Select---";
                //}

                //ddlState.Items.FindByText(State).Selected = true;
            }
        }

    }

    private void FillStockist()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.Get_Stockist_MR_Wise(divcode, sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            dsStockist.Tables[0].Rows.Add(dsStockist.Tables[0].NewRow());
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
    }


    private void FillSF_Alpha()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.get_MR_Stockist_Alphabet(divcode, sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }
    }
    private void FillStockist(string sAlpha)
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Alphabat_OrderList_MR(divcode, sAlpha, sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
    }

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["Char"] = sCmd;

        if (sCmd == "All")
        {
            FillStockist();
        }
        else
        {
            grdStockist.PageIndex = 0;
            FillStockist(sCmd);
        }

    }

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
        Stockist sk = new Stockist();
        dtGrid = sk.get_MR_StockistList_DataTable(divcode, sf_code);
        sCmd = Session["Char"].ToString();

        if (sCmd == "All")
        {
            dtGrid = sk.get_MR_StockistList_DataTable(divcode, sf_code);
        }
        else if (sCmd != "")
        {
            dtGrid = sk.get_MR_Stockist_Alphabat_Filter(divcode, sCmd, sf_code);
        }
        else if (TxtSrch.Text != "")
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 2)
            {
                S_Name = "Stockist_Name";

            }
            dtGrid = sk.get_MR_StockistList_Sorting(divcode, TxtSrch.Text, sf_code);
        }
        else if (ddlStockist.SelectedIndex > 0)
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 3)
            {
                dtGrid = sk.Search_MR_Statewise_StockName(divcode, sf_code, ddlStockist.SelectedItem.Text);
            }
            else if (search == 4)
            {
                dtGrid = sk.Search_MR_HQwise_StockistList(divcode, sf_code, ddlStockist.SelectedItem.Text);
            }

        }
        return dtGrid;
    }

    protected void grdStockist_Sorting(object sender, GridViewSortEventArgs e)
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
        grdStockist.DataSource = sortedView;
        grdStockist.DataBind();

    }

    protected void grdStockist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dtGrid;

        Stockist sk = new Stockist();
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdStockist.EditIndex = -1;
        //Fill the Division Grid
        sCmd = Session["Char"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 2)
            {
                S_Name = "Stockist_Name";
            }

            dtGrid = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);

            // dtGrid = sk.FindStockistlist(sFind);

            if (dtGrid.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
        }

        else if (ddlStockist.SelectedIndex != -1)
        {
            Search();
        }

    }
    protected void grdStockist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdStockist.EditIndex = e.NewEditIndex;
        //Fill the Division Grid

        DataTable dtGrid;

        sCmd = Session["Char"].ToString();
        Stockist sk = new Stockist();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 2)
            {
                S_Name = "Stockist_Name";
            }

            dtGrid = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);

            // dtGrid = sk.FindStockistlist(sFind);

            if (dtGrid.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;

                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
        }

        else if (ddlStockist.SelectedIndex != -1)
        {
            Search();
        }

        TextBox ctrl = (TextBox)grdStockist.Rows[e.NewEditIndex].Cells[2].FindControl("txtStockist_Name");
        ctrl.Focus();
    }
    protected void grdStockist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdStockist.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateStockist(iIndex);

        DataTable dtGrid;
        Stockist sk = new Stockist();

        sCmd = Session["Char"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 2)
            {
                S_Name = "Stockist_Name";
                //  FindStockist(S_Name, TxtSrch.Text, Session["div_code"].ToString());
            }

            dtGrid = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);

            if (dtGrid.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;

                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;

                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
        }

        else if (ddlStockist.SelectedIndex != -1)
        {
            Search();
        }

    }
    protected void grdStockist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            stockist_code = Convert.ToString(e.CommandArgument);

            //Deactivate the Stockist Details
            Stockist dv = new Stockist();
            int iReturn = dv.DeActivate(stockist_code);
            if (iReturn > 0)
            {
                //  menu1.Status = "Stockist has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Unable to Deactivate');</script>");
            }
            FillStockist();
        }

        if (e.CommandName == "Insert")
        {
            TextBox txtStockist_Name_Foot = (TextBox)grdStockist.FooterRow.FindControl("txtStockist_Name_Foot");
            DropDownList ddlState_Foot = (DropDownList)grdStockist.FooterRow.FindControl("ddlState_Foot");
            DropDownList ddlHQ_Foot = (DropDownList)grdStockist.FooterRow.FindControl("ddlHQ_Foot");
            TextBox txtStockist_ContactPerson_Foot = (TextBox)grdStockist.FooterRow.FindControl("txtStockist_ContactPerson_Foot");
            TextBox txtStockist_Mobile_Foot = (TextBox)grdStockist.FooterRow.FindControl("txtStockist_Mobile_Foot");

            // Add new Stockist Details 

            // stockist_code = Request.QueryString["Stockist_Code"];
            stockist_name = txtStockist_Name_Foot.Text;
            //stockist_Address = txtStockist_Address.Text;
            stockist_ContactPerson = txtStockist_ContactPerson_Foot.Text;
            // stockist_Designation = txtStockist_Desingation.Text;
            stockist_mobileno = txtStockist_Mobile_Foot.Text;
            Territory = ddlHQ_Foot.SelectedItem.Text;
            State = ddlState_Foot.SelectedItem.Text;
            PoolStatus = "0";
            string SFCode = sf_code + ",";

            Stockist sk = new Stockist();
            int iReturn = sk.RecordAdd(divcode, SFCode, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobileno, Territory, PoolStatus, State);
            if (iReturn > 0)
            {
                // PoolStatus = ddlPlStatus.SelectedValue;
                iReturn_FM = sk.RecordAdd_FM(divcode, iReturn, stockist_name, SFCode, Sale_Entry);
                // menu1.Status = "Stockist Created Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Created Successfully');</script>");
                //Resetall();
            }
            else if (iReturn == -2)
            {
                //menu1.Status = "Stockist already Exist!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Already Exist');</script>");
            }

            FillStockist();

        }


    }

    protected void grdStockist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStockist.PageIndex = e.NewPageIndex;
        DataTable dtGrid;
        Stockist sk = new Stockist();

        sCmd = Session["Char"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 2)
            {
                S_Name = "Stockist_Name";
            }

            dtGrid = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);


            if (dtGrid.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
        }

        else if (ddlStockist.SelectedIndex != -1)
        {
            Search();
        }


    }
    protected void grdStockist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    private void FindStockist(string sSearchBy, string sSearchText, string div_code)
    {
        Stockist sk = new Stockist();
        DataTable dsStockist;
        // dsStockist = sk.FindStockistlist(sFind);


        if (TxtSrch.Text == "")
        {
            dsStockist = sk.getStockistList_MR_sort_Empty(divcode, sf_code);
        }
        else
        {
            dsStockist = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);
        }


        if (dsStockist.Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }

    }
    private void UpdateStockist(int eIndex)
    {
        Label lblStockist_Code = (Label)grdStockist.Rows[eIndex].Cells[1].FindControl("lblStockist_Code");
        stockist_code = lblStockist_Code.Text;
        TextBox txtStockist_Name = (TextBox)grdStockist.Rows[eIndex].Cells[2].FindControl("txtStockist_Name");
        stockist_name = txtStockist_Name.Text;
        TextBox txtStockist_ContactPerson = (TextBox)grdStockist.Rows[eIndex].Cells[3].FindControl("txtStockist_ContactPerson");
        stockist_ContactPerson = txtStockist_ContactPerson.Text;
        TextBox txtStockist_Mobile = (TextBox)grdStockist.Rows[eIndex].Cells[4].FindControl("txtStockist_Mobile");
        stockist_mobileno = txtStockist_Mobile.Text;
        DropDownList ddlHQ = (DropDownList)grdStockist.Rows[eIndex].Cells[5].FindControl("ddlHQ");
        Territory = ddlHQ.SelectedItem.Text;
        DropDownList ddlState = (DropDownList)grdStockist.Rows[eIndex].Cells[5].FindControl("ddlState");
        State = ddlState.SelectedItem.Text;

        // int iReturn=0;

        //if (Territory != "---Select---")
        //{
        Stockist sk = new Stockist();
        int iReturn = sk.RecordUpdate(divcode, stockist_code, stockist_name, stockist_ContactPerson, stockist_mobileno, Territory, State);

        //}
        //else
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Please Select HQ Name');</script>");
        //}

        //Update Stockist

        if (iReturn > 0)
        {
            //menu1.Status = "Stockist Updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Stockist exit with the same name !!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Already Exist with the Same Name');</script>");
        }
    }

    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStockist.Visible = true;
        int search = Convert.ToInt32(ddlSrch.SelectedValue);
        TxtSrch.Text = string.Empty;

        if (grdStockist.Visible == true)
        {
            if (search == 2)
            {
                TxtSrch.Visible = true;
                Btnsrc.Visible = true;
                ddlStockist.Visible = false;
                TxtSrch.Focus();
            }
            else
            {
                TxtSrch.Visible = false;
                ddlStockist.Visible = true;
                Btnsrc.Visible = true;
                ddlStockist.Focus();
            }
            if (search == 1)
            {
                TxtSrch.Visible = false;
                ddlStockist.Visible = false;
                Btnsrc.Visible = false;

            }
            if (search == 3)
            {
                FillState(divcode);

            }
            if (search == 4)
            {
                //TxtSrch.Visible = true;
                //Btnsrc.Visible = true;
                //ddlStockist.Visible = false;

                GetPoolName();

            }

            val = "";
            FillStockist();
        }
        else
        {
            grdStockist.Visible = false;


            if (search == 2)
            {
                TxtSrch.Visible = true;
                Btnsrc.Visible = true;
                ddlStockist.Visible = false;
                TxtSrch.Focus();
            }
            else
            {
                TxtSrch.Visible = false;
                ddlStockist.Visible = true;
                Btnsrc.Visible = true;
                ddlStockist.Focus();
            }
            if (search == 1)
            {
                TxtSrch.Visible = false;
                ddlStockist.Visible = false;
                Btnsrc.Visible = false;
            }
            if (search == 3)
            {
                FillState(divcode);
            }
            if (search == 4)
            {
                //TxtSrch.Visible = true;
                //Btnsrc.Visible = true;
                //ddlStockist.Visible = false;

                GetPoolName();

            }

            val = "";

        }

    }

    private void FillState(string divcode)
    {

        DataSet dsDivision;
        DataSet dsState;

        Division dv = new Division();
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
            dsState = st.getState(state_cd);
            ddlStockist.DataTextField = "statename";
            ddlStockist.DataValueField = "state_code";
            ddlStockist.DataSource = dsState;
            ddlStockist.DataBind();
        }
    }

    private void GetPoolName()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getPool_Name(divcode, sf_code);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlStockist.DataTextField = "Pool_Name";
            ddlStockist.DataValueField = "Pool_Id";
            ddlStockist.DataSource = dsStockist;
            ddlStockist.DataBind();
        }
    }

    protected void Btnsrc_Click(object sender, EventArgs e)
    {

        if (grdStockist.Visible == true)
        {

            System.Threading.Thread.Sleep(time);
            Session["Char"] = string.Empty;
            grdStockist.PageIndex = 0;
            Search();
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            Session["Char"] = string.Empty;

        }

    }

    private void Fill_Statewise_Stock()
    {
        Stockist sk = new Stockist();
        DataTable dsStockist;
        dsStockist = sk.Search_MR_Statewise_StockName(divcode, sf_code, ddlStockist.SelectedItem.Text);
        if (dsStockist.Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
    }

    private void Fill_HQ_Stock()
    {
        Stockist sk = new Stockist();
        DataTable dsStockist;
        dsStockist = sk.Search_MR_HQwise_StockistList(divcode, sf_code, ddlStockist.SelectedItem.Text);
        if (dsStockist.Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
    }

    private void Search()
    {

        int search = Convert.ToInt32(ddlSrch.SelectedValue);

        DataTable dtStock;

        Stockist sk = new Stockist();

        if (search == 1)
        {
            FillStockist();
        }
        if (search == 2)
        {
            S_Name = "Stockist_Name";

            if (TxtSrch.Text == "")
            {
                dtStock = sk.getStockistList_MR_sort_Empty(divcode, sf_code);
            }
            else
            {
                dtStock = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);
            }

            if (dtStock.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;

                grdStockist.DataSource = dtStock;
                grdStockist.DataBind();
            }
            else
            {
                grdStockist.DataSource = dtStock;
                grdStockist.DataBind();
                dlAlpha.Visible = false;

            }

        }

        if (search == 4)
        {
            //S_Name = "Territory";
            //string sFind = string.Empty;
            //sFind = " AND " + S_Name + " like '" + TxtSrch.Text + "%' AND Division_Code = '" + divcode + "' ";
            //dtStock = sk.FindStockistlist(sFind);

            dtStock = sk.Search_MR_HQwise_StockistList(divcode, sf_code, ddlStockist.SelectedItem.Text);

            if (dtStock.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;

                grdStockist.DataSource = dtStock;
                grdStockist.DataBind();
            }
            else
            {
                grdStockist.DataSource = dtStock;
                grdStockist.DataBind();
                dlAlpha.Visible = false;
            }
        }
        if (search == 3)
        {

            dtStock = sk.Search_MR_Statewise_StockName(divcode, sf_code, ddlStockist.SelectedItem.Text);

            if (dtStock.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;

                grdStockist.DataSource = dtStock;
                grdStockist.DataBind();
            }
            else
            {
                grdStockist.DataSource = dtStock;
                grdStockist.DataBind();
                dlAlpha.Visible = false;


            }
        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Stockist_MR_Creation.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        btnBack.Visible = false;
        grdStockist.Visible = true;
        dlAlpha.Visible = true;
    }

    protected void btnHq_Click(object sender, EventArgs e)
    {
        Stockist Sk = new Stockist();
        int iReturn = Sk.Add_Stockist_HQ(divcode, txtPool_Sname.Text, txtPool_Name.Text, sf_code);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Created Successfully');</script>");
            Resetall();
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('HQ Name Already Exist');</script>");
            txtPool_Name.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('HQ Short Name Already Exist');</script>");
            txtPool_Sname.Focus();
        }

        DataTable dtGrid;
        Stockist sk = new Stockist();

        sCmd = Session["Char"].ToString();

        if (sCmd == "All")
        {
            FillStockist();
        }
        else if (sCmd != "")
        {
            FillStockist(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            int search = Convert.ToInt32(ddlSrch.SelectedValue);

            if (search == 2)
            {
                S_Name = "Stockist_Name";
            }

            dtGrid = sk.get_MR_StockistList_Sorting(divcode, sf_code, TxtSrch.Text);


            if (dtGrid.Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdStockist.Visible = true;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdStockist.DataSource = dtGrid;
                grdStockist.DataBind();
            }
        }

        else if (ddlStockist.SelectedIndex != -1)
        {
            Search();
        }

    }

    private void Resetall()
    {
        txtPool_Sname.Text = "";
        txtPool_Name.Text = "";
    }
}
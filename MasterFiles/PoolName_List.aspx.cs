using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_PoolName_List : System.Web.UI.Page
{
    string divcode = string.Empty;
    string Pool_sname = string.Empty;
    string Pool_name = string.Empty;
    DataSet dsStockist = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sCmd = string.Empty;
    string poolname = string.Empty;
    string shortname = string.Empty;
    int poolcode = 0;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            GetPoolArea();
            btnNew.Focus();
            ddlSt.Visible = false;
            TxtSrch.Visible = true;
            Btnsrc.Visible = true;
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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("PoolName_Creation.aspx");
    }

    private void GetPoolArea()
    {
        Stockist sk = new Stockist();
        // dsStockist = sk.getPoolName_List(divcode,"admin");
        dsStockist = sk.GetHQ_List_Det(divcode, "admin");

        if (dsStockist.Tables[0].Rows.Count > 0)
        {

            grdPoolName.DataSource = dsStockist;
            grdPoolName.DataBind();

            foreach (GridViewRow row in grdPoolName.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                if (lblCount.Text != "0")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }

        }
        else
        {
            grdPoolName.DataSource = dsStockist;
            grdPoolName.DataBind();
        }
    }
    protected void grdPoolName_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdPoolName.EditIndex = -1;
        Search();
    }

    protected void grdPoolName_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdPoolName.EditIndex = e.NewEditIndex;
        Search();
        TextBox ctrl = (TextBox)grdPoolName.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }
    protected void grdPoolName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPoolName.PageIndex = e.NewPageIndex;
        Search();

    }
    protected void grdPoolName_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdPoolName.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        //GetPoolArea();
        // sCmd = Session["Char"].ToString();

        Search();

    }

    protected void grdPoolName_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(divcode);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[5].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow && grdPoolName.EditIndex == e.Row.RowIndex)
        {


            DropDownList ddlState = (e.Row.FindControl("ddlState") as DropDownList);

            Division dv = new Division();
            DataSet dsDivision;
            dsDivision = dv.getStatePerDivision(divcode);

            string State = "";

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


                State = (e.Row.FindControl("lblState") as Label).Text;

                if (State == "")
                {
                    State = "---Select---";
                }

                ddlState.Items.FindByText(State).Selected = true;
            }



        }

    }

    protected void grdPoolName_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            int Pool_Id = Convert.ToInt32(e.CommandArgument);

            //Deactivate the Stockist Details
            Stockist dv = new Stockist();
            int iReturn = dv.DeActivate_PoolName_List(Pool_Id);
            if (iReturn > 0)
            {
                //  menu1.Status = "Stockist has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            GetPoolArea();
        }
    }

    private void Update(int eIndex)
    {
        Label lblPoolCode = (Label)grdPoolName.Rows[eIndex].Cells[1].FindControl("lblPoolCode");
        poolcode = Convert.ToInt32(lblPoolCode.Text);
        TextBox txtShortName = (TextBox)grdPoolName.Rows[eIndex].Cells[2].FindControl("txtShortName");
        shortname = txtShortName.Text;
        TextBox txtPoolName = (TextBox)grdPoolName.Rows[eIndex].Cells[3].FindControl("txtPoolName");
        poolname = txtPoolName.Text;
        DropDownList ddlState = (DropDownList)grdPoolName.Rows[eIndex].Cells[4].FindControl("ddlState");
        string StateName = ddlState.SelectedItem.Text;

        HiddenField hdnPoolName = (HiddenField)grdPoolName.Rows[eIndex].FindControl("hdnPoolName");

        string hdnPool = hdnPoolName.Value;

        // Update Doctor Class
        Stockist dv = new Stockist();
        // int iReturn = dv.RecordUpdateHq(poolcode, shortname, poolname, divcode,"admin");
        int iReturn = dv.Stockist_HQ_List_Update(poolcode, shortname, poolname, divcode, "admin", StateName, hdnPool);

        if (iReturn > 0)
        {
            // menu1.Status = "Doctor Class Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");

            txtPoolName.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");

            txtShortName.Focus();
        }
    }
    private void FillState(string div_code)
    {
        DataSet dsDivision = new DataSet();
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
            dsState = st.getState(state_cd);
            ddlSt.DataTextField = "statename";
            ddlSt.DataValueField = "state_code";
            ddlSt.DataSource = dsState;
            ddlSt.DataBind();
        }
    }
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSt.Visible = false;
        TxtSrch.Visible = true;
        Btnsrc.Visible = true;

        int search = Convert.ToInt32(ddlSrch.SelectedValue);
        TxtSrch.Text = string.Empty;
        if (search == 1)
        {
            TxtSrch.Visible = false;
            ddlSt.Visible = false;
            Btnsrc.Visible = true;
            GetPoolArea();
        }
        if (search == 2)
        {
            TxtSrch.Visible = false;
            ddlSt.Visible = true;
            Btnsrc.Visible = true;
            FillState(divcode);

        }
        else if (search == 3)
        {
            TxtSrch.Visible = true;
            Btnsrc.Visible = true;
            ddlSt.Visible = false;

        }
        //if (search == 1)
        //{
        //    TxtSrch.Visible = false;
        //    ddlSt.Visible = false;
        //    Btnsrc.Visible = false;
        //    FillProd();
        //}
        //if (search == 3)
        //{
        //    FillCategory();
        //}

    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["Char"] = string.Empty;
        grdPoolName.PageIndex = 0;
        Search();

    }
    private void Search()
    {
        search = ddlSrch.SelectedValue.ToString();
        Stockist prd = new Stockist();
        if (search == "1")
        {
            GetPoolArea();
        }
        if (search == "3")
        {

            // FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            dsStockist = prd.Get_Territory_Detail(TxtSrch.Text, Session["div_code"].ToString());
            if (dsStockist.Tables[0].Rows.Count > 0)
            {

                grdPoolName.Visible = true;
                grdPoolName.DataSource = dsStockist;
                grdPoolName.DataBind();

                foreach (GridViewRow row in grdPoolName.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");
                    Label lblCount = (Label)row.FindControl("lblCount");
                    //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                    if (lblCount.Text != "0")
                    {
                        // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdPoolName.DataSource = dsStockist;
                grdPoolName.DataBind();

            }
        }
        if (search == "2")
        {
            dsStockist = prd.Get_HQDetail_State(ddlSt.SelectedItem.Text, divcode);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {

                grdPoolName.Visible = true;
                grdPoolName.DataSource = dsStockist;
                grdPoolName.DataBind();

                foreach (GridViewRow row in grdPoolName.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");
                    Label lblCount = (Label)row.FindControl("lblCount");
                    //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                    if (lblCount.Text != "0")
                    {
                        // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdPoolName.DataSource = dsStockist;
                grdPoolName.DataBind();

            }
        }
    }

}
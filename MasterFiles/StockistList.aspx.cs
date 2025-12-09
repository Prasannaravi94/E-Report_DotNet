using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using Bus_EReport;


public partial class MasterFiles_StockistList : System.Web.UI.Page
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
    string ERP_Code = string.Empty;
    string val = string.Empty;

    int time;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            Session["Char"] = "All";


            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            grdStockist.Visible = true;
            FillSample();

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

                // dsState = st.getState_Stock(state_cd, divcode);
                ddlState.DataTextField = "statename";
                ddlState.DataValueField = "state_code";
                ddlState.DataSource = dsState;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("---Select---", "0"));


                State = (e.Row.FindControl("lblState") as Label).Text;

                if (State == "" || State == "---Select---")
                {
                    State = "---Select---";
                   
                }

                ddlState.Items.FindByText(State).Selected = true;
            }

            DropDownList ddlHQName = (e.Row.FindControl("ddlHQ") as DropDownList);

            Stockist ss = new Stockist();

            // DataSet ds = ss.Get_StateWise_HQ(State, divcode);

            DataSet ds = ss.Get_Statewise_HQ_Det(State, divcode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHQName.DataTextField = "Pool_Name";
                ddlHQName.DataValueField = "Pool_Id";
                ddlHQName.DataSource = ds;
                ddlHQName.DataBind();
            }

            string HQName = (e.Row.FindControl("lblHQName") as Label).Text;

            if (HQName == "" || HQName == "--Select--")
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

        }

    }

    private void FillSample()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.GetStockist_Sample();
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            gv_Stock.Visible = true;
            gv_Stock.DataSource = dsStockist;
            gv_Stock.DataBind();
        }
        else
        {
            gv_Stock.DataSource = dsStockist;
            gv_Stock.DataBind();
        }
    }

    //sorting

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

        string sFind = string.Empty;

        if (ddlFields.SelectedValue != "Stockist_Name" && ddlFields.SelectedValue != "Stockist_Designation")
        {
            txtsearch.Text = hdnProduct.Value;
        }
        sFind = " AND " + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND Division_Code = '" + Session["div_code"].ToString() + "' ";


        dsStockist = sk.Search_Stock_Name(sFind);
        dtGrid = dsStockist.Tables[0];
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
        DataTable dtGrid = new DataTable();
        dtGrid = sortedView.ToTable();
        grdStockist.DataSource = dtGrid;
        grdStockist.DataBind();

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Stockist_Creation.aspx");

    }
    protected void grdStockist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dtGrid;

        Stockist sk = new Stockist();
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdStockist.EditIndex = -1;
        FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());

    }
    protected void grdStockist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdStockist.EditIndex = e.NewEditIndex;

        if (!(string.IsNullOrEmpty((string)Session["Alpha"])))
        {
            string Alphabet = Session["Alpha"].ToString();
            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), Alphabet);
        }
        else
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

        TextBox ctrl = (TextBox)grdStockist.Rows[e.NewEditIndex].Cells[2].FindControl("txtStockist_Name");
        ctrl.Focus();
    }
    protected void grdStockist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdStockist.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateStockist(iIndex);
        //  FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());


        if (!(string.IsNullOrEmpty((string)Session["Alpha"])))
        {
            string Alphabet = Session["Alpha"].ToString();
            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), Alphabet);
        }
        else
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
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
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
    }

    protected void grdStockist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStockist.PageIndex = e.NewPageIndex;

        // FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());



        if (!(string.IsNullOrEmpty((string)Session["Alpha"])))
        {
            string Alphabet = Session["Alpha"].ToString();
            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), Alphabet);
        }
        else
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
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
        string sFind = string.Empty;

        if (sSearchBy != "Stockist_Name" && sSearchBy != "Stockist_Designation")
        {
            sSearchText = hdnProduct.Value;
        }
        sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' ";
        Stockist sk = new Stockist();
        dsStockist = sk.Search_Stock_Name(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
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
        dsStockist = sk.Search_Stock_Name_Alpha(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.Visible = true;

            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }
        else
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }

    }

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();

        if (sCmd != "")
        {
            Session["Alpha"] = sCmd;
        }
        else
        {
            Session["Alpha"] = sCmd;
        }

        if (sCmd == "All")
        {
            // FillStockist();

            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), sCmd);
        }
        else
        {
            // FillStockist(sCmd);
            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), sCmd);
        }

        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
    }

    private void FindStockist_Alphabet(string sSearchBy, string sSearchText, string div_code, string sCmd)
    {
        string sFind = string.Empty;

        if (sSearchBy != "Stockist_Name")
        {
            sSearchText = hdnProduct.Value;
        }
        if (sCmd == "All")
        {
            sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "'";
        }
        else
        {

            sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' AND LEFT(stockist_name,1) like '" + sCmd + "' ";

        }
        Stockist sk = new Stockist();
        dsStockist = sk.Search_Alphabet_StockistName(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
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
        TextBox txtStockist_ContactPerson = (TextBox)grdStockist.Rows[eIndex].Cells[4].FindControl("txtStockist_ContactPerson");
        stockist_ContactPerson = txtStockist_ContactPerson.Text;
        TextBox txtStockist_Mobile = (TextBox)grdStockist.Rows[eIndex].Cells[5].FindControl("txtStockist_Mobile");
        stockist_mobileno = txtStockist_Mobile.Text;
        DropDownList ddlHQ = (DropDownList)grdStockist.Rows[eIndex].Cells[6].FindControl("ddlHQ");
        Territory = ddlHQ.SelectedItem.Text;
        DropDownList ddlState = (DropDownList)grdStockist.Rows[eIndex].Cells[6].FindControl("ddlState");
        State = ddlState.SelectedItem.Text;

        //TextBox txtStockist_Designation = (TextBox)grdStockist.Rows[eIndex].Cells[3].FindControl("txtStockist_Designation");
        //ERP_Code = txtStockist_Designation.Text;

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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Stockist exit with the same name !!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist with the Same Name');</script>");
        }

        FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        if (ddlFields.SelectedValue != "")
        {
            gv_Stock.Visible = false;
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }


    }


    protected void btnReActivate_Click(object sender, EventArgs e)
    {
        // gv_Stock.Visible = false;
        grdStockist.Visible = false;
        dlAlpha.Visible = false;

        Response.Redirect("Stockist_Bulk_Activation.aspx");
    }

    protected void btnStockistDeActivate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stockist_Bulk_DeActivation.aspx");
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow gvrow = (GridViewRow)((DropDownList)sender).NamingContainer;

        DropDownList ddlState = (DropDownList)gvrow.FindControl("ddlState");
        DropDownList ddlHQ = (DropDownList)gvrow.FindControl("ddlHQ");

        string State = ddlState.SelectedItem.Text;

        Stockist ss = new Stockist();

        //DataSet ds = ss.Get_StateWise_HQ(State, divcode);

        DataSet ds = ss.Get_Statewise_HQ_Det(State, divcode);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlHQ.Items.Clear();
            ddlHQ.DataTextField = "Pool_Name";
            ddlHQ.DataValueField = "Pool_Id";
            ddlHQ.DataSource = ds;
            ddlHQ.DataBind();
            // ddlHQ.Items.Insert(0, "--Select--");
        }

    }

    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        string search = ddlFields.SelectedValue.ToString();
        txtsearch.Text = string.Empty;
        //grdSalesForce.PageIndex = 0;
        if (search == "---Select---")
        {
            txtsearch.Visible = false;
            btnSearch.Visible = false;
            ddlSrc.Visible = false;
        }

        else if (search == "State Name" || search == "HQ Name")
        {
            txtsearch.Visible = false;
            btnSearch.Visible = true;
            ddlSrc.Visible = true;
        }
        else
        {
            txtsearch.Visible = true;
            ddlSrc.Visible = false;
            btnSearch.Visible = true;
            txtsearch.Focus();
        }


    }
}


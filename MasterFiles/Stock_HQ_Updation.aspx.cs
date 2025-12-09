using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;


public partial class MasterFiles_Stock_HQ_Updation : System.Web.UI.Page
{

    #region "Declaration"
    string divcode = string.Empty;

    string SF_Name = string.Empty;
    string SF_Code = string.Empty;
    string SF_Type = string.Empty;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        heading.InnerHtml = this.Page.Title;
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            FillManagers();
            //FillColor();
            btnAdd_Hq.Visible = false;
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

    private void FillManagers()
    {
        DataSet dsSalesForce;
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_Hierarchy(divcode, "admin");

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
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void BindGridview()
    {
        SF_Name = ddlFieldForce.SelectedItem.Text;
        SF_Code = ddlFieldForce.SelectedValue;

        string S_Code = string.Empty;

        DataSet dsFieldForce;

        if (SF_Name == "admin")
        {
            SF_Type = "1";
        }
        else
        {
            SF_Type = "0";
        }

        SalesForce sf = new SalesForce();

        if (SF_Type == "1")
        {
            dsFieldForce = sf.getSecSales_MR(divcode, "admin");
        }
        else
        {
            dsFieldForce = sf.getSecSales_MR(divcode, SF_Code);
        }


        if (dsFieldForce.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsFieldForce.Tables[0].Rows)
            {
                string SfCode = dr["SF_Code"].ToString();

                S_Code += SfCode + ",";

            }

            S_Code = S_Code.Substring(0, S_Code.Length - 1);

            DataSet dsStockist;
            Stockist objStock = new Stockist();

            dsStockist = objStock.GetStockist_Detail(divcode, S_Code);

            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                gvStockist_HQ.Visible = true;
                btnSubmit.Visible = true;
                gvStockist_HQ.DataSource = dsStockist;
                gvStockist_HQ.DataBind();
            }
            else
            {
                gvStockist_HQ.DataSource = dsStockist;
                gvStockist_HQ.DataBind();
            }

        }

    }

    protected void gvStockist_HQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList ddlHQName = (e.Row.FindControl("ddlHQ") as DropDownList);

            Stockist sk = new Stockist();
            DataSet dsStockist;

            string Sf_Code = "admin";
            dsStockist = sk.getPool_Name(divcode, Sf_Code);

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

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int Stockist_Code;
        string Stockist_Name = string.Empty;
        string ERP_Code = string.Empty;
        string HQ_Name = string.Empty;
        string State = string.Empty;
        Doctor dv = new Doctor();
        int iReturn = -1;
        bool err = false;
        foreach (GridViewRow gridRow in gvStockist_HQ.Rows)
        {
            Label lblStockist_Code = (Label)gridRow.Cells[1].FindControl("lblStockist_Code");
            Stockist_Code = Convert.ToInt32(lblStockist_Code.Text.ToString());

            Label lblStockist_Name = (Label)gridRow.Cells[1].FindControl("lblStockist_Name");
            Stockist_Name = lblStockist_Name.Text.ToString();

            TextBox txtERPCode = (TextBox)gridRow.Cells[1].FindControl("txtERPCode");
            ERP_Code = txtERPCode.Text.ToString();

            DropDownList ddlHQ = (DropDownList)gridRow.Cells[1].FindControl("ddlHQ");
            HQ_Name = ddlHQ.SelectedItem.Text;

            DropDownList ddlState = (DropDownList)gridRow.Cells[1].FindControl("ddlState");
            State = ddlState.SelectedItem.Text;

            Stockist objProduct = new Stockist();
            iReturn = objProduct.Update_Stockist_Detail(divcode, Stockist_Code, ERP_Code, HQ_Name, State);
        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Updated Successfully');window.location='Stock_HQ_Updation.aspx';</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Not Updated');</script>");
        }

    }


    protected void btnGo_Click(object sender, EventArgs e)
    {
        BindGridview();
        btnAdd_Hq.Visible = true;
    }

    protected void btnHq_Click(object sender, EventArgs e)
    {
        Stockist Sk = new Stockist();

        string Sf_Code = "admin";
        int iReturn = Sk.Add_Stockist_HQ(divcode, txtPool_Sname.Text, txtPool_Name.Text, Sf_Code);

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

        BindGridview();
    }

    private void Resetall()
    {
        txtPool_Sname.Text = "";
        txtPool_Name.Text = "";

    }

    //protected void linkcheck_Click(object sender, EventArgs e)
    //{

    //    FillManagers();
    //    ddlFieldForce.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnGo.Visible = true;

    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stock_HQ_Bulk_Edit.aspx");
    }
}
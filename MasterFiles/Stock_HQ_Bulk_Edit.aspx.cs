using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;


public partial class MasterFiles_Stock_HQ_Bulk_Edit : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;

            getstate();
          
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

  
    private void BindGridview()
    {

        string st = string.Empty;
            DataSet dsStockist;
            Stockist objStock = new Stockist();
            if (ddlSt.SelectedValue == "0")
            {
                st = "'---Select---','','0'";
                dsStockist = objStock.getStockist_View_Statwise_select(divcode, st);
            }
            else
            {
                st = ddlSt.SelectedItem.Text.Trim();
                dsStockist = objStock.getStockist_View_Statwise(divcode, st);
            }


         //   dsStockist = objStock.getStockist_View_Statwise(divcode, st);

            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                gvStockist_HQ.Visible = true;
                btnSubmit.Visible = true;
                gvStockist_HQ.DataSource = dsStockist;
                gvStockist_HQ.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                gvStockist_HQ.DataSource = dsStockist;
                gvStockist_HQ.DataBind();
            }

     

    }
    private void getstate()
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
            ddlSt.DataTextField = "statename";
            ddlSt.DataValueField = "state_code";
            ddlSt.DataSource = dsState;
            ddlSt.DataBind();
            ddlSt.Items.Insert(0, new ListItem("---Select---(No state)", "0"));


        }
    }

    protected void gvStockist_HQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
         //   Label ddlHQName = (e.Row.FindControl("ddlHQ") as Label);

            Stockist sk = new Stockist();
            DataSet dsStockist;

          //  dsStockist = sk.getPool_Name(divcode);

            //ddlHQName.DataSource = dsStockist;
            //ddlHQName.DataTextField = "Pool_Name";
            //ddlHQName.DataValueField = "Pool_Id";
            //ddlHQName.DataBind();

            //string HQName = (e.Row.FindControl("lblHQName") as Label).Text;

            //if (HQName == "")
            //{
            //    HQName = "--Select--";
            //    ddlHQName.Items.FindByText(HQName).Selected = true;
            //}
            //else
            //{
            //    ListItem item = ddlHQName.Items.FindByText(HQName);

            //    if (item != null)
            //    {
            //        ddlHQName.Items.FindByText(HQName).Selected = true;
            //    }
            //}

            // ddlHQName.Items.FindByText(HQName).Selected = true;


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




                if (State == "")
                {
                    ddlState.Items.FindByText("---Select---").Selected = true;
                }
                else
                {
                    ddlState.Items.FindByText(State).Selected = true;
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

            TextBox txtHQ = (TextBox)gridRow.Cells[1].FindControl("txthq");
            HQ_Name = txtHQ.Text;

            DropDownList ddlState = (DropDownList)gridRow.Cells[1].FindControl("ddlState");
            State = ddlState.SelectedItem.Text.Trim();
         

            Stockist objProduct = new Stockist();
            iReturn = objProduct.Update_Stockist_Detail_Edit(divcode, Stockist_Code, ERP_Code, HQ_Name.Trim(), State);
        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Stock_HQ_Bulk_Edit.aspx';</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Updated');</script>");
        }

    }


    protected void btnGo_Click(object sender, EventArgs e)
    {
        BindGridview();
     
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Stock_HQ_Updation.aspx");
    }
}
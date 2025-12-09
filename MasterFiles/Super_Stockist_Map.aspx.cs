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


public partial class MasterFiles_Super_Stockist_Map : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsStok = new DataSet();
    string str_sscode = string.Empty;
    int iIndex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            FillSuperStockist();
            Get_State();
        }
    }

    private void FillSuperStockist()
    {
        DataSet dsstk = new DataSet();
        Stockist stk = new Stockist();
        dsstk = stk.GetSuper_stk(div_code);
        if (dsstk.Tables[0].Rows.Count > 0)
        {
            ddlst.DataTextField = "SS_Name";
            ddlst.DataValueField = "SS_Code";
            ddlst.DataSource = dsstk;
            ddlst.DataBind();
        }
    }
    private void Get_State()
    {
        Division dv = new Division();
        DataSet dsDivision;
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
            DataSet dsState;
            dsState = st.getStateChkBox(state_cd);

            // dsState = st.getState_Stock(state_cd, divcode);

            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("---Select---", "0"));

        }
    }
    protected void btnHq_Click(object sender, EventArgs e)
    {
        Stockist Sk = new Stockist();


        int iReturn = Sk.Add_Super_Stockist(div_code, txt_Sname.Text, ddlState.SelectedItem.Text, ddlState.SelectedItem.Value,txtEmail.Text);


        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Created Successfully');</script>");
            Clear();
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('HQ Name Already Exist');</script>");
            txt_Sname.Focus();
        }
        

        FillSuperStockist();
    }
    private void Clear()
    {
        txt_Sname.Text = "";
        ddlState.SelectedIndex = -1;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        pnlStk.Visible = true;
        btnSubmit.Visible = true;
        FillStockist();
    }

    private void FillStockist()
    {
        Stockist dv = new Stockist();
        dsStok = dv.getStockist_List(div_code);
        ChkStock.DataTextField = "Stockist_Name";
        ChkStock.DataSource = dsStok;
        ChkStock.DataBind();


        dsSalesForce = dv.getStk_MapSS(ddlst.SelectedValue,div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsSalesForce.Tables[0].Rows.Count; i++)
            {
                str_sscode = dsSalesForce.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                 for (iIndex = 0; iIndex < ChkStock.Items.Count; iIndex++)
                {
                    if (str_sscode == ChkStock.Items[iIndex].Value)
                    {
                        ChkStock.Items[iIndex].Selected = true;
                        ChkStock.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold ");
                    }

                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Stockist st = new Stockist();
        for (int i = 0; i < ChkStock.Items.Count; i++)
        {
            if (ChkStock.Items[i].Selected)
            {
                string stkcode = ChkStock.Items[i].Value;
                string stkName = ChkStock.Items[i].Text;
                int iReturn = st.RecordAdd_SStockist_Map(stkcode, stkName, ddlst.SelectedValue, ddlst.SelectedItem.Text.Trim(), div_code);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');window.location='Super_Stockist_Map.aspx';</script>");
                }
            }
            else
            {
                string stkcode = ChkStock.Items[i].Value;
                string stkName = ChkStock.Items[i].Text;
                ListedDR lstdr = new ListedDR();
                int iReturn = st.Delete_StockistMap(stkcode, stkName, ddlst.SelectedValue, ddlst.SelectedItem.Text.Trim(), div_code);
            }
        }
    }
}
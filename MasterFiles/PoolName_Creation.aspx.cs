using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_PoolName_Creation : System.Web.UI.Page
{
    DataSet dsStockist = null;
    string Pool_Id = string.Empty;
    string divcode = string.Empty;
    string Pool_sname = string.Empty;
    string Pool_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "PoolName_List.aspx";
        Pool_Id = Request.QueryString["Pool_Id"];
        if (!Page.IsPostBack)
        {
            txtPool_Sname.Focus();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            //if (Pool_Id != "" && Pool_Id != null)
            //{

            //    Stockist sk = new Stockist();
            //    dsStockist = sk.getPoolName(divcode, Pool_Id);
            //    if (dsStockist.Tables[0].Rows.Count > 0)
            //    {
            //        txtPool_Sname.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //        txtPool_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            //    }
            //}

            Get_State();

            if (Pool_Id != "" && Pool_Id != null)
            {
                string Sf_code = "admin";

                Stockist sk = new Stockist();
                dsStockist = sk.Get_HQ_NameDetail(divcode, Pool_Id, Sf_code, "");

                if (dsStockist.Tables[0].Rows.Count > 0)
                {
                    txtPool_Sname.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtPool_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    hdnpoolName.Value = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    string State = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                    if (State == "")
                    {
                        State = "---Select---";
                        ddlState.Items.FindByText(State).Selected = true;
                    }
                    else
                    {
                        ddlState.Items.FindByText(State).Selected = true;
                    }


                    btnSubmit.Text = "Update";
                }
                else
                {
                    btnSubmit.Text = "Save";
                }
            }
            else
            {
                btnSubmit.Text = "Save";
            }

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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Pool_sname = txtPool_Sname.Text;
        Pool_name = txtPool_Name.Text;

        if (Pool_Id == null || Pool_Id == "0")
        {
            string Sf_code = "admin";

            Stockist Sk = new Stockist();

            string State_Name = ddlState.SelectedItem.Text;
            int iReturn = Sk.Create_Stockist_HQ(divcode, txtPool_Sname.Text, txtPool_Name.Text, Sf_code, State_Name);

            // int iReturn = Sk.Add_Stockist_HQ(divcode, Pool_sname, Pool_name,Sf_code);

            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
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
        }
        else
        {
            Stockist Sk = new Stockist();

            int PoolID = Convert.ToInt32(Pool_Id);

            string Sf_code = "admin";

            string State_Name = ddlState.SelectedItem.Text;

            int iReturn = Sk.Stockist_HQ_List_Update(PoolID, Pool_sname, Pool_name, divcode, Sf_code, State_Name, hdnpoolName.Value);

            // int iReturn = Sk.RecordUpdateHq(PoolID, Pool_sname, Pool_name, divcode,Sf_code);

            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                Resetall();
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
        }

    }
    private void Resetall()
    {
        txtPool_Sname.Text = "";
        txtPool_Name.Text = "";
        btnSubmit.Text = "Save";
        ddlState.SelectedIndex = -1;
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("PoolName_List.aspx");
    }
}
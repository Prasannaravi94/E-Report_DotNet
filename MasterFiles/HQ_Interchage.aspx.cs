using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Text;
using System.Data.SqlClient;

public partial class MasterFiles_HQ_Interchage : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Targer_Territory = string.Empty;
    string WorkArea = string.Empty;
    string div_code = string.Empty;
    DataSet dsFromHq = new DataSet();
    DataSet dsToHq = new DataSet();
    string fromHq = string.Empty;
    string toHq = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        Session["backurl"] = "SalesForceList.aspx";
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            // GetWorkName();
            FillSalesForce();
            FillToSalesForce();

            menu1.Title = this.Page.Title;
        }

    }

    private void FillSalesForce()
    {

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            ddlFromFieldForce.DataTextField = "sf_name";
            ddlFromFieldForce.DataValueField = "sf_code";
            ddlFromFieldForce.DataSource = dsSalesForce;
            ddlFromFieldForce.DataBind();
            ddlFromFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }




    private void FillToSalesForce()
    {
        if (chkvacant.Checked == true)
        {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.UserList_getMR_Vacant(div_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {

                ddlToFieldForce.DataTextField = "sf_name";
                ddlToFieldForce.DataValueField = "sf_code";
                ddlToFieldForce.DataSource = dsSalesForce;
                ddlToFieldForce.DataBind();
                ddlToFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
        else
        {
            SalesForce sf1 = new SalesForce();
            dsSalesForce = sf1.SalesForceList_New_GetMr(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlToFieldForce.DataTextField = "sf_name";
                ddlToFieldForce.DataValueField = "sf_code";
                ddlToFieldForce.DataSource = dsSalesForce;
                ddlToFieldForce.DataBind();
                ddlToFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
    }
    protected void ddlFromFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGo1_Click(object sender, EventArgs e)
    {
        DataSet dsterr = new DataSet();
        Territory terr1 = new Territory();
        dsterr = terr1.getTerritory1(ddlToFieldForce.SelectedValue);
        if (dsterr.Tables[0].Rows.Count > 0)
        {
            //pnlmove.Visible = true;
            ddlToFieldForce.Enabled = false;
            pnlmove.Visible = true;
            lblinterchange.Visible = true;
            pnlmove1.Visible = true;
            grdterritory1.Visible = true;
            grdterritory1.Enabled = false;
            grdterritory1.DataSource = dsterr;
            grdterritory1.DataBind();
        }
        else
        {
            ddlToFieldForce.Enabled = true;
            pnlmove.Visible = false;
            lblinterchange.Visible = false;
            pnlmove1.Visible = false;

            grdterritory1.DataSource = null;
            grdterritory1.DataBind();

        }
        btnTransfer.Visible = true;
        //btnRep.Visible = true;
    }

    protected void ddlToFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory1(ddlFromFieldForce.SelectedValue);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();

        }

    }

    protected void btnTransfer1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;


        // Transfer Territory
        ListedDR lst = new ListedDR();
        iReturn = lst.Interchange_MR_New(ddlFromFieldForce.SelectedValue, ddlToFieldForce.SelectedValue, ddlFromFieldForce.SelectedItem.Text, ddlToFieldForce.SelectedItem.Text, div_code);



        if (iReturn != -1)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Interchange Successfully');</script>");
            // ViewTerritory();

            grdTerritory.Visible = false;

            grdterritory1.Visible = false;
            clearall();
        }
    }



    protected void btnTransfer_Click(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        int iReturn = -1;
        //Loop through each row of gridview
        SalesForce sf = new SalesForce();
        dsFromHq = sf.getSfCode_HQ(ddlFromFieldForce.SelectedValue, div_code);
        if (dsFromHq.Tables[0].Rows.Count > 0)
        {
            fromHq = dsFromHq.Tables[0].Rows[0]["sf_hq"].ToString();
        }
        dsToHq = sf.getSfCode_HQ(ddlToFieldForce.SelectedValue, div_code);
        if (dsToHq.Tables[0].Rows.Count > 0)
        {
            toHq = dsToHq.Tables[0].Rows[0]["sf_hq"].ToString();
        }

        SqlDataAdapter da = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();


        // SqlCommand cmd = new SqlCommand("Hq_Interchange_Rollback_SFC_1", con);
        //SqlCommand cmd = new SqlCommand("Hq_Interchange_Rollback_SFC_App", con);
        SqlCommand cmd = new SqlCommand("Hq_Interchange_Rollback_SFC_New", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@from_sfcode", ddlFromFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@to_sfcode", ddlToFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@from_name", ddlFromFieldForce.SelectedItem.Text);

        cmd.Parameters.AddWithValue("@to_name", ddlToFieldForce.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@from_hq", fromHq);
        cmd.Parameters.AddWithValue("@to_hq", toHq);

        cmd.Parameters.Add("@retValue", SqlDbType.Int);
        cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
        con.Close();
        if (iReturn > 0)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Interchange Successfully');</script>");

            grdTerritory.Visible = false;

            grdterritory1.Visible = false;
            clearall();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Territory Nmae Found.Change the Territory Name in Territory Master');</script>");
        }

    }

    protected void btnRep_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(time);
        //int iReturn = -1;
        //foreach (GridViewRow gridRow in grdTerritory.Rows)
        //{
        //    Label lblTerritory_Code = (Label)gridRow.Cells[1].FindControl("lblTerritory_Code");
        //    Territory_Code = lblTerritory_Code.Text.ToString();

        //    CheckBox chkTerr = (CheckBox)gridRow.Cells[0].FindControl("chkTerritory");
        //    bool bCheck = chkTerr.Checked;

        //        // Transfer Territory
        //        ListedDR lst = new ListedDR();
        //        iReturn = lst.TransferTerritory_MR_Rep(ddlFromFieldForce.SelectedValue, ddlToFieldForce.SelectedValue);

        //}

        //if (iReturn != -1)
        //{

        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Transferred Successfully');</script>");
        //    ViewTerritory();
        //}
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        ddlFromFieldForce.Enabled = true;
        ddlToFieldForce.Enabled = true;
        grdTerritory.Visible = false;
        grdterritory1.Visible = false;
        pnlmove.Visible = false;
        pnlmove1.Visible = false;
        lblinterchange.Visible = false;
        btnTransfer.Visible = false;
        ddlFromFieldForce.SelectedIndex = -1;
        ddlToFieldForce.SelectedIndex = -1;
    }

    protected void chkvacant_CheckedChanged(object sender, EventArgs e)
    {
        FillToSalesForce();
    }

    private void clearall()
    {
        ddlFromFieldForce.Enabled = true;
        ddlToFieldForce.Enabled = true;
        grdTerritory.Visible = false;
        grdterritory1.Visible = false;
        pnlmove.Visible = false;
        pnlmove1.Visible = false;
        lblinterchange.Visible = false;
        btnTransfer.Visible = false;




    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory1(ddlFromFieldForce.SelectedValue);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFromFieldForce.Enabled = false;
            pnlmove.Visible = true;
            grdTerritory.Visible = true;
            grdTerritory.Enabled = false;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
        else
        {
            ddlFromFieldForce.Enabled = true;
            pnlmove.Visible = false;
            grdTerritory.DataSource = null;
            grdTerritory.DataBind();

        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SalesForceList.aspx");
    }
}
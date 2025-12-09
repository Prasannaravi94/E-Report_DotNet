using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Reflection;

public partial class MasterFiles_Competitor_Ourprd : System.Web.UI.Page
{
    #region "Declaration"
    string divcode = string.Empty;
    string Comp_Sl_No = string.Empty;
    string Comp_Prd_Sl_No = string.Empty;
    string type = string.Empty;
    string Sl_No = string.Empty;
    string Comp_Name = string.Empty;
    string Comp_Prd_Name = string.Empty;
    string Comp_Prd_name_Mapp = string.Empty;
    string Comp_Name_Mapp_id = string.Empty;
    int Comp_SlNo = 0;
    int Comp_Prd_SlNo = 0;
    int SlNo = 0;
    string Our_Prd_Code = string.Empty;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Convert.ToString(Session["div_code"]);

        if (!Page.IsPostBack)
        {

            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillOur_prd();
            fillCompteti_prd();
            fillmapped_prd();
        }

    }

    private void fillCompteti_prd()
    {
        DataSet dsPrd = null;
        Doctor dv = new Doctor();
        dsPrd = dv.getCompetitor_Prd(divcode);
        if (dsPrd.Tables[0].Rows.Count > 0)
        {
            Chkprd.DataValueField = "Comp_Prd_Sl_No";
            Chkprd.DataTextField = "Comp_Prd_Name";
            Chkprd.DataSource = dsPrd;
            Chkprd.DataBind();
        }

    }

    private void FillOur_prd()
    {
        DataSet dsPrd = null;
        Doctor dv = new Doctor();
        dsPrd = dv.get_OurProduct(divcode);
        if (dsPrd.Tables[0].Rows.Count > 0)
        {
            ddlour_prd.DataValueField = "Product_Code_SlNo";
            ddlour_prd.DataTextField = "Product_Detail_Name";
            ddlour_prd.DataSource = dsPrd;
            ddlour_prd.DataBind();
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Doctor dv = new Doctor();
        DataSet dsPrd_code = null;
        string Competitor_Prd_bulk = string.Empty;
        string Competitor_prd_code =string.Empty;
        string Competitor_prd_name =string.Empty;
        string Competitor_code = string.Empty;
        string Competitor_name = string.Empty;
        int iReturn = -1;

        for (int i = 0; i < Chkprd.Items.Count; i++)
        {
            if (Chkprd.Items[i].Selected)
            {
                dsPrd_code = dv.get_Comp_prdcode(divcode, Chkprd.Items[i].Value);

                if (dsPrd_code.Tables[0].Rows.Count > 0)
                {
                    Competitor_Prd_bulk += Chkprd.Items[i].Value + '#' + Chkprd.Items[i].Text + '~' + dsPrd_code.Tables[0].Rows[0]["Comp_Sl_No"].ToString() + '$' + dsPrd_code.Tables[0].Rows[0]["Comp_Name"].ToString() + '/';
                    Competitor_prd_code +=Chkprd.Items[i].Value+'/';
                    Competitor_prd_name +=Chkprd.Items[i].Text +'/';
                    Competitor_code += dsPrd_code.Tables[0].Rows[0]["Comp_Sl_No"].ToString()+ '/';
                    Competitor_name += dsPrd_code.Tables[0].Rows[0]["Comp_Name"].ToString() + '/';
                }
            }
        }

        if (Session["Our_prd_code"] != null && Session["Our_prd_code"] != "")
        {
            iReturn = dv.UpdateOur_Prd_Competitor_Prd(ddlour_prd.SelectedValue, ddlour_prd.SelectedItem.Text, Competitor_Prd_bulk, Competitor_prd_code, Competitor_prd_name, divcode, Competitor_code, Competitor_name);
        }
        else
        {

            iReturn = dv.AddOur_Prd_Competitor_Prd(ddlour_prd.SelectedValue, ddlour_prd.SelectedItem.Text, Competitor_Prd_bulk, Competitor_prd_code, Competitor_prd_name, divcode, Competitor_code, Competitor_name);
        }

        if (iReturn > 0)
        {
          
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
           // fillmapped_prd();
            Chkprd.ClearSelection();
            txtprd.Text = "";
            txtNew.Text = "";
         
        }


    }
    protected void ddlour_prd_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session["Our_prd_code"] = null;
        fillmapped_prd();
    }

    private void fillmapped_prd()
    {

         DataSet dsPrd = null;
        Doctor dv = new Doctor();
        string competitor_prd_code = string.Empty;
        Chkprd.ClearSelection();
        txtprd.Text = "";

        dsPrd = dv.get_OurProduct_Comp_avail(ddlour_prd.SelectedValue);
        if (dsPrd.Tables[0].Rows.Count > 0)
        {
            competitor_prd_code = dsPrd.Tables[0].Rows[0]["competitor_prd_code"].ToString();
            Session["Our_prd_code"] = dsPrd.Tables[0].Rows[0]["Our_prd_code"].ToString();
        }


        string[] prdname;

        prdname = competitor_prd_code.Split('/');

        foreach (string prd in prdname)
        {
            for (int i = 0; i < Chkprd.Items.Count; i++)
            {
                if (Chkprd.Items[i].Value == prd)
                {
                    Chkprd.Items[i].Selected = true;
                    txtprd.Text += Chkprd.Items[i].Text + ",";
                }
            }
        }

        txtprd.Text = txtprd.Text.TrimEnd(',');
    }
}
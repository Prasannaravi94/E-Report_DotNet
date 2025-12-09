using Bus_EReport;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_Sample_Input_Qty_Edit : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        btnGo.Focus();
        if (!Page.IsPostBack)
        {
            FillManagers();
            FillColor();
           // menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
        }
        FillColor();
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
        ddlFFType.Visible = true;
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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

    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

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
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            FillSalesForce();
        }

    }

    private void FillSalesForce()
    {
        gvDetails.DataSource = null;
        gvDetails.DataBind();

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.sp_get_Rep_access_All_User_sampleinput(ddlFieldForce.SelectedValue.ToString().Trim(), div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSumbit.Visible = true;
            gvDetails.Visible = true;
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
        }
        else
        {
            btnSumbit.Visible = false;
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow gridRow in gvDetails.Rows)
        {

            Label lblsf_code = (Label)gridRow.Cells[1].FindControl("lblsf_code");
            CheckBox chkSample = (CheckBox)gridRow.Cells[0].FindControl("chkSample");
            HiddenField Hdnsample = (HiddenField)gridRow.Cells[0].FindControl("Hdnsample");

            CheckBox chkInput = (CheckBox)gridRow.Cells[0].FindControl("chkInput");
            HiddenField Hdninput = (HiddenField)gridRow.Cells[0].FindControl("Hdninput");

            

            if (lblsf_code.Text != string.Empty)
            {
                chkSample.Checked = Hdnsample.Value == "1" ? true : false;
                chkInput.Checked = Hdninput.Value == "1" ? true : false;
                
            }
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        int Sample = 0;
        int Input = 0;
        SalesForce adsp = new SalesForce();
        
        foreach (GridViewRow gridRow in gvDetails.Rows)
        {
            Sample = 0;
            Input = 0;

            Label lblsf_code = (Label)gridRow.Cells[1].FindControl("lblsf_code");
            CheckBox chkSample = (CheckBox)gridRow.Cells[1].FindControl("chkSample");
            CheckBox chkInput = (CheckBox)gridRow.Cells[1].FindControl("chkInput");


            Sample = chkSample.Checked?1:0;
            Input = chkInput.Checked ? 1 : 0;
            

            DataSet dsScrLckChk = new DataSet();

            if (chkSample.Checked == true || chkInput.Checked == true)
            {
                dsScrLckChk= adsp.sampleinputchk(lblsf_code.Text);
                if(dsScrLckChk.Tables[0].Rows[0]["cnt"].ToString() != "0")
                {
                    
                    iReturn = adsp.sampleinputEditUpdate(lblsf_code.Text, div_code, Sample, Input);
                }
                else
                {
                    iReturn = adsp.sampleinputEditinsert(lblsf_code.Text, div_code, Sample, Input);
                }
               
            }
            else
            {
                iReturn = adsp.sampleinputEditUpdate(lblsf_code.Text, div_code, Sample, Input);
            }
        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('updated Successfully');</script>");
            FillSalesForce();
        }

    }
}
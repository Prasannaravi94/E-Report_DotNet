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


public partial class MasterFiles_Competitor_Master : System.Web.UI.Page
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
    string sCmd = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);

        type = Request.QueryString["type"];
        Comp_Sl_No = Request.QueryString["Comp_Sl_No"];
        Comp_Prd_Sl_No = Request.QueryString["Comp_Prd_Sl_No"];
        Sl_No = Request.QueryString["Sl_No"];
        Comp_Name = Request.QueryString["Comp_Name"];
        Comp_Prd_Name = Request.QueryString["Comp_Prd_Name"];
        Comp_Prd_name_Mapp = Request.QueryString["Comp_Prd_name_Mapp"];
        Comp_Name_Mapp_id = Request.QueryString["Comp_Name_Mapp_id"];

        if (!Page.IsPostBack)
        {

            ViewState["typeCode"] = type;

            if (type != "" && type != null)
            {
                if (type == "1")
                {
                    lblcomp.Visible = true;
                    txtCompetitorName.Visible = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Update";

                    txtCompetitorName.Text = Comp_Name;
                }
                else if (type == "2")
                {
                    lblcomp.Visible = true;
                    txtCompetitorName.Visible = true;
                    lblcomp.Text = "Competitor Product Name";
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Update";

                    txtCompetitorName.Text = Comp_Prd_Name;
                }
                else if (type == "3")
                {
                    lblprd.Visible = true;
                    txtprd.Visible = true;
                    lblname.Visible = true;
                    ddlcompe.Visible = true;
                    //txtNew.Visible = true;
                    //txtNew.Visible = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Update";
                    fillcompetitor();
                    fillprd();
                    ddlcompe.SelectedValue = Comp_Name_Mapp_id;
                    ddlcompe.Enabled = false;
                    //txtNew.Visible = false;

                    string[] prdname;

                    prdname=Comp_Prd_name_Mapp.Split('/');

                    foreach (string prd in prdname)
                    {
                        for(int i =0;i<Chkprd.Items.Count; i++)
                        {
                            if (Chkprd.Items[i].Text == prd)
                            {
                                Chkprd.Items[i].Selected = true;
                                txtprd.Text += Chkprd.Items[i].Text + ",";
                            }
                                
                        }
                    }
                }
            }
            else
            {
                FillCompetitor(0);
                btnComp.Focus();
            }

            menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);



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
    private void FillCompetitor(int typp)
    {
        Doctor dv = new Doctor();
        if (typp == 1 || typp==0)
        {
            DataSet dsCom = null;
            dsCom = dv.getCompetitor(divcode);
            if (dsCom.Tables[0].Rows.Count > 0)
            {
                grdCampet.Visible = true;
                grdprd.Visible = false;
                grdComVsprd.Visible = false;
                grdCampet.DataSource = dsCom;
                grdCampet.DataBind();
            }
            else
            {
                grdCampet.Visible = true;
                grdprd.Visible = false;
                grdComVsprd.Visible = false;
                grdCampet.DataSource = dsCom;
                grdCampet.DataBind();
            }
        }
        else if (typp == 2)
        {
            DataSet dsComPrd = null;
            dsComPrd = dv.getCompetitor_Prd(divcode);
            if (dsComPrd.Tables[0].Rows.Count > 0)
            {
                grdprd.Visible = true;
                grdCampet.Visible = false;
                grdComVsprd.Visible = false;
                grdprd.DataSource = dsComPrd;
                grdprd.DataBind();
            }
            else
            {
                grdprd.Visible = true;
                grdCampet.Visible = false;
                grdComVsprd.Visible = false;
                grdprd.DataSource = dsComPrd;
                grdprd.DataBind();
            }
        }
        else if(typp==3)
        {
            DataSet dsCmpVsPrd = null;
            dsCmpVsPrd = dv.getCompetitor_VsPrd(divcode);
            if (dsCmpVsPrd.Tables[0].Rows.Count > 0)
            {
                grdComVsprd.Visible = true;
                grdprd.Visible = false;
                grdCampet.Visible = false;
                grdComVsprd.DataSource = dsCmpVsPrd;
                grdComVsprd.DataBind();
            }
            else
            {
                grdComVsprd.Visible = true;
                grdprd.Visible = false;
                grdCampet.Visible = false;
                grdComVsprd.DataSource = dsCmpVsPrd;
                grdComVsprd.DataBind();
            }

        }
    }

    protected void btnComp_Click(object sender, EventArgs e)
    {
        txtCompetitorName.Text = "";
        btnSubmit.Text = "Add";
        ViewState["type"] = "Competitor";
        ViewState["typeCode"] = null;
        lblcomp.Visible = true;
        txtCompetitorName.Visible = true;
        btnSubmit.Visible = true;
        lblprd.Visible = false;
        txtprd.Visible = false;
        lblname.Visible = false;
        ddlcompe.Visible = false;
        //txtNew.Visible = false;
        //type = null;
        FillCompetitor(1);
   
    }
    protected void btnPrd_Click(object sender, EventArgs e)
    {
        txtCompetitorName.Text = "";
        btnSubmit.Text = "Add";
        ViewState["type"] = "Product";
        ViewState["typeCode"] = null;
        lblcomp.Text = "Competitor Product Name";
        lblcomp.Visible = true;
        txtCompetitorName.Visible = true;
        btnSubmit.Visible = true;
        lblprd.Visible = false;
        txtprd.Visible = false;
        lblname.Visible = false;
        ddlcompe.Visible = false;
        //txtNew.Visible = false;
       // type = null;
        FillCompetitor(2);
       
    }
    protected void btnComVsPrd_Click(object sender, EventArgs e)
    {
        Chkprd.ClearSelection();
        txtprd.Text = "";
        txtCompetitorName.Text = "";
        btnSubmit.Text = "Add";
        ViewState["type"] = "CompVsProduct";
        ViewState["typeCode"] = null;
        lblname.Visible = true;
        ddlcompe.Visible = true;
        //txtNew.Visible = true;
        lblprd.Visible = true;
        txtprd.Visible = true;
        btnSubmit.Visible = true;
        lblcomp.Visible = false;
        txtCompetitorName.Visible = false;
        //type = null;
        fillcompetitor();
        fillprd();
        FillCompetitor(3);
        
    }

    private void fillcompetitor()
    {
        DataSet dsCom = null;
        Doctor dv = new Doctor();
        dsCom = dv.getCompetitor(divcode);
        if (dsCom.Tables[0].Rows.Count > 0)
        {
            ddlcompe.DataValueField = "Comp_Sl_No";
            ddlcompe.DataTextField = "Comp_Name";
            ddlcompe.DataSource = dsCom;
            ddlcompe.DataBind();
        }
    }

    private void fillprd()
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
    protected void grdCampet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            Comp_SlNo = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivate_Competitor(Comp_SlNo);
            if (iReturn > 0)
            {
                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
              
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillCompetitor(1);
        }
    }

    protected void grdprd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            Comp_Prd_SlNo = Convert.ToInt16(e.CommandArgument);
            //Deactivate

            Doctor dv = new Doctor();

            int iReturn = dv.DeActivate_CompetPrd(Comp_Prd_SlNo);
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillCompetitor(2);

        }
    }

    protected void grdComVsprd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            SlNo = Convert.ToInt16(e.CommandArgument);

            Doctor dv = new Doctor();

            int iReturn = dv.DeActivate_Compet_VsPrd(SlNo);
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillCompetitor(3);

        }
    }
    protected void grdCampet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    
        grdCampet.PageIndex = e.NewPageIndex;
        FillCompetitor(1);
       
    }
    protected void grdCampet_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdprd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdComVsprd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdprd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdprd.PageIndex = e.NewPageIndex;
        FillCompetitor(2);
    }
    protected void grdComVsprd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdComVsprd.PageIndex = e.NewPageIndex;
        FillCompetitor(3);
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Doctor dv = new Doctor();


        if (ViewState["typeCode"] != "" && ViewState["typeCode"] != null)
        {
            if (ViewState["typeCode"].ToString() == "1")
            {

                int iReturn = dv.RecordUpdateCompetitor(divcode, txtCompetitorName.Text,Comp_Sl_No, "1");

                if (iReturn > 0)
                {
                    
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                    txtCompetitorName.Visible = false;
                    lblname.Visible = false;
                    ddlcompe.Visible = false;
                    //txtNew.Visible = false;
                    lblprd.Visible = false;
                    txtprd.Visible = false;
                    lblcomp.Visible = false;
                    btnSubmit.Visible = false;
                    FillCompetitor(1);
                }
              
            }
            else if (ViewState["typeCode"].ToString() == "2")
            {
                int iReturn = dv.RecordUpdateCompetitor(divcode, txtCompetitorName.Text,Comp_Prd_Sl_No, "2");

                if (iReturn > 0)
                {
                   
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                    txtCompetitorName.Visible = false;
                    lblname.Visible = false;
                    ddlcompe.Visible = false;
                    //txtNew.Visible = false;
                    lblprd.Visible = false;
                    txtprd.Visible = false;
                    lblcomp.Visible = false;
                    btnSubmit.Visible = false;
                    FillCompetitor(2);
                }
            }
            else if (ViewState["typeCode"].ToString() == "3")
            {
                string name = "";
                string code = "";

                for (int i = 0; i < Chkprd.Items.Count; i++)
                {
                    if (Chkprd.Items[i].Selected)
                    {
                        name += Chkprd.Items[i].Text + "/";
                        code += Chkprd.Items[i].Value + "/";
                    }
                }

                if (code != "")
                {
                    name = name.Remove(name.Length - 1);
                    code = code.Remove(code.Length - 1);
                }


                int iReturn = dv.RecordUpdateCompetitor_VsPrd(divcode, ddlcompe.SelectedValue, name, code, "3");

                if (iReturn > 0)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                    txtCompetitorName.Visible = false;
                    lblname.Visible = false;
                    ddlcompe.Visible = false;
                    //txtNew.Visible = false;
                    lblprd.Visible = false;
                    txtprd.Visible = false;
                    lblcomp.Visible = false;
                    btnSubmit.Visible = false;
                    Chkprd.ClearSelection();
                    FillCompetitor(3);
                }
            }
        }
        else
        {
            if (ViewState["type"].ToString() == "Competitor")
            {

                int iReturn = dv.RecordAddCompetitor(divcode, txtCompetitorName.Text, "1");

                if (iReturn > 0)
                {
                    //menu1.Status = "Doctor Sub-Category Created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    txtCompetitorName.Text = "";
                    txtCompetitorName.Focus();
                    FillCompetitor(1);
                }

                else if (iReturn == -2)
                {
                    // menu1.Status = "Doctor Sub-Category already Exist";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Competitor Name Already Exist');</script>");
                    txtCompetitorName.Focus();
                }
            }
            else if (ViewState["type"].ToString() == "Product")
            {
                int iReturn = dv.RecordAddCompetitor(divcode, txtCompetitorName.Text, "2");

                if (iReturn > 0)
                {
                    //menu1.Status = "Doctor Sub-Category Created Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    txtCompetitorName.Text = "";
                    txtCompetitorName.Focus();
                    FillCompetitor(2);
                }

                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name Already Exist');</script>");
                    txtCompetitorName.Focus();
                }
            }

            else if (ViewState["type"].ToString() == "CompVsProduct")
            {

                string name = "";
                string code = "";

                for (int i = 0; i < Chkprd.Items.Count; i++)
                {
                    if (Chkprd.Items[i].Selected)
                    {
                        name += Chkprd.Items[i].Text + "/";
                        code += Chkprd.Items[i].Value + "/";
                    }
                }

                if (code != "")
                {
                    name = name.Remove(name.Length - 1);
                    code = code.Remove(code.Length - 1);
                }


                int iReturn = dv.RecordAddCompetitor_VsPrd(divcode, ddlcompe.SelectedValue, ddlcompe.SelectedItem.Text, name, code, "3");

                if (iReturn > 0)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    txtprd.Text = "";
                    Chkprd.ClearSelection();
                    FillCompetitor(3);
                }

                else if (iReturn == -2)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Product Mapped for this Competitor');</script>");
                    txtprd.Focus();
                    txtprd.Text = "";
                    Chkprd.ClearSelection();
                }
            }

        }
    }
    protected void ddlcompe_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Chkprd_SelectedIndexChanged(object sender, EventArgs e)
    {

        string s = "";
        string Value = "";


        for (int i = 0; i < Chkprd.Items.Count; i++)
        {

            if (Chkprd.Items[i].Selected)//changed 1 to i  
            {
                s += Chkprd.Items[i].Text.ToString() + ","; //changed 1 to i
                Value += Chkprd.Items[i].Value.ToString() + ",";
            }

        }
        txtprd.Text = s;
        if (Value != "")
        {
            Session["prdValue"] = Value.Remove(Value.Length - 1);
            Session["prdname"] = s.Remove(s.Length - 1);
        }
        // txtinput.Text = s.TrimEnd(',');


    }
    protected void btnAddOurprb_Click(object sender, EventArgs e)
    {
        Response.Redirect("Competitor_Ourprd.aspx");
    }
}
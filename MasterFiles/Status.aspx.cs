using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Status : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsdiv = null;
    DataSet dsDivision = null;
    DataSet dsGrid = null;
    //string Designation_Short_Name = string.Empty;
    //string Designation_Name = string.Empty;
    //string Desig_Color = string.Empty;
    string type = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            Filldiv();
            FillYear();
            menu1.Title = this.Page.Title;
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
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }

    private void FillYear()
    {
        
            for (int k = Convert.ToInt16(DateTime.Now.Year-1); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
       
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    private void fillgrid()
    {
       
        SalesForce ds = new SalesForce();
        if (ddlMode.SelectedValue == "0")
        {
            dsGrid = ds.getTP_Count(ddlDivision.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                divid.Visible = false;
                grdSalesForce.Visible = true;
                grdDCR.Visible = false;
                grdDCRTPTime.Visible = false;
                grdSalesForce.DataSource = dsGrid;
                grdSalesForce.DataBind();

            }
            else
            {
                divid.Visible = true;
                grdSalesForce.DataSource = dsGrid;
                grdSalesForce.DataBind();
            }
        }
        else if (ddlMode.SelectedValue == "1")
        {
            dsGrid = ds.getDCR_Count(ddlDivision.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString());
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                divid.Visible = false;
                grdSalesForce.Visible = false;
                grdDCRTPTime.Visible = false;
                grdDCR.Visible = true;
                grdDCR.DataSource = dsGrid;
                grdDCR.DataBind();

            }
            else
            {
                divid.Visible = true;
                grdDCR.DataSource = dsGrid;
                grdDCR.DataBind();
            }
        }
        else 
        {
            dsGrid = ds.getDCR_Time_Count(ddlDivision.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),ddlDate.SelectedValue.ToString());
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                divid.Visible = false;
                grdSalesForce.Visible = false;
                grdDCR.Visible = false;
                grdDCRTPTime.Visible = true;
                grdDCRTPTime.DataSource = dsGrid;
                grdDCRTPTime.DataBind();

            }
            else
            {
                divid.Visible = true;
                grdDCRTPTime.DataSource = dsGrid;
                grdDCRTPTime.DataBind();
            }
        }

    }

    
}
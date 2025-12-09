using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
public partial class FlashNews_Design : System.Web.UI.Page
{
    string div_code = string.Empty;
    string strslno = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    DataSet dsAdmin = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsImage = new DataSet();
    DataSet dsListedDR = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int Count;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Flash_News(div_code);
            LblUser.Text = "Welcome " + Session["sf_name"];
            lbldiv.Text = Session["div_name"].ToString();
            if (Session["sf_type"].ToString() == "1")
            {
                Dob_View();
                Dow_View();
                DobDow_View();
            }
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
    private void Dob_View()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            ListedDR lst = new ListedDR();
            dsListedDR = lst.ViewListedDr_Dob(sf_code, DateTime.Now.Month.ToString(), "");
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
            }
        }
    }
    private void Dow_View()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            ListedDR lst = new ListedDR();
            dsListedDR = lst.ViewListedDr_Dow(sf_code, DateTime.Now.Month.ToString(), "");
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor_Dow.DataSource = dsListedDR;
                grdDoctor_Dow.DataBind();
            }
            else
            {
                grdDoctor_Dow.DataSource = dsListedDR;
                grdDoctor_Dow.DataBind();
            }
        }
    }
    private void DobDow_View()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            ListedDR lst = new ListedDR();
            dsListedDR = lst.ViewListedDr_DobDow(sf_code, DateTime.Now.Month.ToString(), "");
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDobDow.DataSource = dsListedDR;
                grdDobDow.DataBind();
            }
            else
            {
                grdDobDow.DataSource = dsListedDR;
                grdDobDow.DataBind();
            }
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        AdminSetup admin = new AdminSetup();
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            SalesForce sf = new SalesForce();
            ListedDR lst = new ListedDR();

            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), div_code);
            dsListedDR = lst.getLstdDr_Wrng_CreationFFWise(Session["sf_code"].ToString(), div_code);

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("Wrong_Creation.aspx");
            }
            else if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("Birthday_Wish.aspx");
            }
            else if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
    protected void btnHomepage_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOB = (Label)e.Row.FindControl("lblDOB");
            if (lblDOB.Text == "01/Jan/1900")
            {
                lblDOB.Text = "";
            }
            if (lblDOB != null)
            {
                DateTime holiday = Convert.ToDateTime(lblDOB.Text.ToString());
                string Holi_Month = holiday.Day + "/" + holiday.Month.ToString();
                string cur_Month = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString();

                if (Holi_Month == cur_Month)
                {
                    e.Row.BackColor = Color.LightBlue;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void grdDoctor_Dow_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOW = (Label)e.Row.FindControl("lblDOW");
            if (lblDOW.Text == "01/Jan/1900")
            {
                lblDOW.Text = "";
            }
            if (lblDOW != null)
            {

                DateTime holiday = Convert.ToDateTime(lblDOW.Text.ToString());
                string Holi_Month = holiday.Day + "/" + holiday.Month.ToString();
                string cur_Month = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString();

                if (Holi_Month == cur_Month)
                {
                    e.Row.BackColor = Color.LightBlue;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
    protected void grdDobDow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDOB = (Label)e.Row.FindControl("lblDOB1");
            Label lblDOW = (Label)e.Row.FindControl("lblDOW1");
            if (lblDOB.Text == "01/Jan/1900" || lblDOW.Text == "01/Jan/1900")
            {
                lblDOB.Text = "";
                lblDOW.Text = "";
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }
}
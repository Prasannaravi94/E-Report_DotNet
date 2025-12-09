using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
using System.Globalization;
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
                LstdDrWrngCreation();
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
    private void LstdDrWrngCreation()
    {
        if (Session["sf_type"].ToString() == "1")
        {
            ListedDR lst = new ListedDR();
            dsListedDR = lst.getLstdDr_Wrng_CreationFFWise(sf_code, div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                gvDoctor.DataSource = dsListedDR;
                gvDoctor.DataBind();
            }
            else
            {
                gvDoctor.DataSource = dsListedDR;
                gvDoctor.DataBind();
            }
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        AdminSetup admin = new AdminSetup();
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            SalesForce sf = new SalesForce();

            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), div_code);

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            if (dsSalesForce.Tables[0].Rows.Count > 0)
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
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            //
            #region Variable
            CheckBox chk;
            Label lblDocCode;
            Label lblCrtDate;
            Label lblActDate;
            int iReturn = 0;
            #endregion
            //

            foreach (GridViewRow gridrow in gvDoctor.Rows)
            {
                #region Variables
                chk = (CheckBox)gridrow.FindControl("chkListedDR");
                lblDocCode = (Label)gridrow.FindControl("lblDocCode");
                lblCrtDate = (Label)gridrow.FindControl("lblCrtDate");
                lblActDate = (Label)gridrow.FindControl("lblActDate");
                #endregion

                if (chk.Checked == true)
                {
                    iReturn = InsertDocBusEntry(sf_code, div_code, lblDocCode, lblCrtDate, lblActDate);
                }
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Process Completed Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    //
    #region InsertDocBusEntry
    private int InsertDocBusEntry(string ddlSFCode, string div_code, Label lblDocCode, Label lblCrtDate, Label lblActDate)
    {
        int iReturn = -1;

        ListedDR lst = new ListedDR();

        DateTime CrtDate = DateTime.ParseExact(lblCrtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime ActDate = DateTime.ParseExact(lblActDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        iReturn = lst.RecordUpdateLstdDr_Wrng_Creation(sf_code, Convert.ToInt32(div_code), Convert.ToInt32(lblDocCode.Text), CrtDate, ActDate);

        return iReturn;
    }
    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_NotificationMsg_delete : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string ListedDrCode = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string sQryStr = string.Empty;
    string sfcode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            //FillDoc();
            BindDate();
            //FillMsg();
        }
    }

    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
            }

            ddlYear.Text = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
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
    private void FillMsg()
    {
        SalesForce saln = new SalesForce();

        dsSalesForce = saln.getnotifymsgdel(div_code, ddlYear.SelectedValue,ddlMonth.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdNotMsg.Visible = true;
            grdNotMsg.DataSource = dsSalesForce;
            grdNotMsg.DataBind();
            btnDelete.Visible = true;
        }
        else
        {
            btnDelete.Visible = false;
            grdNotMsg.DataSource = dsSalesForce;
            grdNotMsg.DataBind();
        }
    }


    protected void btnlink_goclick(object sender, EventArgs e)
    {
        FillMsg();
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdNotMsg.Rows)
        {
            CheckBox chkmsg = (CheckBox)gridRow.Cells[0].FindControl("chkmsg");
            bool bCheck = chkmsg.Checked;
            Label lblTrans_Sl_No = (Label)gridRow.Cells[2].FindControl("lblTrans_Sl_No");
            string Trans_Sl_No = lblTrans_Sl_No.Text.ToString();

            if ((bCheck == true))
            {
                SalesForce saln = new SalesForce();
                iReturn = saln.NotificationMsgDelete(Trans_Sl_No);
            }
        }

        if (iReturn != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
            FillMsg();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
    }
}
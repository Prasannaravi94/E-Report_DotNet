using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ActivityReports_Product_Scheme_Entry : System.Web.UI.Page
{
    string sfCode = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();

    Territory objTerritory = new Territory();
    DataSet dsDoc = null;
    DataSet dsTrans_Bus = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    string div_code = string.Empty;
    string doc_name_row = string.Empty;
    string Camp_flag = string.Empty;
    string sf_code_ap = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sfCode = Session["sf_code"].ToString();
            if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
            {
                sfCode = Session["sf_code"].ToString();
            }
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();
            //if (Session["sf_type"].ToString() == "1")
            //{
            //    sfCode = Session["sf_code"].ToString();
            //    UserControl_MR_Menu Usc_MR = (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            //    Divid.Controls.Add(Usc_MR);
            //    Usc_MR.Title = this.Page.Title;
            //    Usc_MR.FindControl("btnBack").Visible = false;
            //}
            //else if (Session["sf_type"].ToString() == "2")
            //{
            //    sfCode = Session["sf_code"].ToString();
            //    UserControl_MGR_Menu Usc_MGR = (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            //    Divid.Controls.Add(Usc_MGR);
            //    Usc_MGR.Title = this.Page.Title;
            //    Usc_MGR.FindControl("btnBack").Visible = false;
            //}
            //else
            //{
            //    sfCode = Session["sf_code"].ToString();
            //    UserControl_MenuUserControl c1 = (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //    Divid.Controls.Add(c1);
            //    //Divid.FindControl("btnBack").Visible = false;
            //    c1.Title = this.Page.Title;
            //}
            if (!Page.IsPostBack)
            {
                FillState(div_code);
                FillDate();
            }
            txtEffFrom.Attributes.Add("autocomplete", "off");
            txtEffTo.Attributes.Add("autocomplete", "off");
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

    private void FillState(string div_code)
    {
        Division dv = new Division();
        DataSet dsDivision = dv.getStatePerDivision(div_code);
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
            DataSet dsState = st.getStateProd(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();


            ddlState.Items.RemoveAt(0);
        }
    }

    protected void rbtnBased_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDate();
    }

    private void FillDate()
    {
        ListedDR lst = new ListedDR();
        DataSet dsLst = lst.get_Product_Scheme_Date(ddlState.SelectedValue.ToString(), div_code, rbtnBased.SelectedValue.ToString());
        if (dsLst.Tables[0].Rows.Count > 0)
        {
            ddlDate.DataTextField = "Effec_Date";
            ddlDate.DataValueField = "Filter_No";
            ddlDate.DataSource = dsLst;
            ddlDate.DataBind();
        }
        else
        {
            ddlDate.DataSource = dsLst;
            ddlDate.DataBind();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDate();
    }

    public string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "Jan";
        }
        else if (sMonth == "2")
        {
            sReturn = "Feb";
        }

        else if (sMonth == "3")
        {
            sReturn = "Mar";
        }
        else if (sMonth == "4")
        {
            sReturn = "Apr";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "Jun";
        }
        else if (sMonth == "7")
        {
            sReturn = "Jul";
        }
        else if (sMonth == "8")
        {
            sReturn = "Aug";
        }
        else if (sMonth == "9")
        {
            sReturn = "Sep";
        }
        else if (sMonth == "10")
        {
            sReturn = "Oct";
        }
        else if (sMonth == "11")
        {
            sReturn = "Nov";
        }
        else if (sMonth == "12")
        {
            sReturn = "Dec";
        }
        return sReturn;
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

    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        try
        {
            dsDoc = LstDoc.get_Product_Scheme(ddlState.SelectedValue.ToString(), div_code);

            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                btnSave.Visible = true;
                Grd_Scheme.Visible = true;

                Grd_Scheme.DataSource = dsDoc;
                Grd_Scheme.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                btnSave.Visible = false;
                Grd_Scheme.DataSource = null;
                Grd_Scheme.DataBind();
                Grd_Scheme.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            ListedDR LstDoc = new ListedDR();
            string Filter = string.Empty;

            btnSave.Visible = true;
            if (chkView.Checked)
            {
                if (ddlDate.SelectedValue.ToString() != "")
                {
                    FillDoc_Datewise();
                }
                else
                {
                    Grd_Scheme.Visible = false;
                    Grd_Scheme.DataSource = null;
                    Grd_Scheme.DataBind();
                    btnSave.Visible = false;
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                dsDoc = LstDoc.get_Product_Scheme_Check(ddlState.SelectedValue.ToString(), div_code, txtEffFrom.Text, txtEffTo.Text, rbtnBased.SelectedValue.ToString());

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    Filter = dsDoc.Tables[0].Rows[0]["Product_Code"].ToString();
                    if (Filter == "0")
                    {
                        FillDoc();
                    }
                    else
                    {
                        Grd_Scheme.Visible = false;
                        Grd_Scheme.DataSource = null;
                        Grd_Scheme.DataBind();
                        btnSave.Visible = false;
                        btnSubmit.Visible = false;
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Created ');</script>");

                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    private void FillDoc_Datewise()
    {
        ListedDR LstDoc = new ListedDR();
        try
        {
            dsDoc = LstDoc.get_Product_Scheme_View(ddlState.SelectedValue.ToString(), div_code, ddlDate.SelectedItem.ToString());

            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                btnSave.Visible = true;
                Grd_Scheme.Visible = true;

                Grd_Scheme.DataSource = dsDoc;
                Grd_Scheme.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                btnSave.Visible = false;
                Grd_Scheme.DataSource = null;
                Grd_Scheme.DataBind();
                Grd_Scheme.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {

            foreach (GridViewRow gridRow in Grd_Scheme.Rows)
            {
                Label lblProd_Code = (Label)gridRow.Cells[1].FindControl("lblProd_Code");
                Label lblProd_Name = (Label)gridRow.Cells[2].FindControl("lblProd_Name");
                Label lblPack = (Label)gridRow.Cells[3].FindControl("lblPack");
                Label lblSl_No = (Label)gridRow.Cells[4].FindControl("lblSl_No");

                TextBox txtRate = (TextBox)gridRow.Cells[5].FindControl("txtRate");
                TextBox txtDiscount = (TextBox)gridRow.Cells[6].FindControl("txtDiscount");
                TextBox txtSchem_Fixa = (TextBox)gridRow.Cells[7].FindControl("txtSchem_Fixa");
                TextBox txtSchem_Qty = (TextBox)gridRow.Cells[8].FindControl("txtSchem_Qty");

                ListedDR Lst = new ListedDR();
                int Return = -1;

                if (chkView.Checked)
                {
                    Return = Lst.RecordUpdate_Product_Scheme_New(lblSl_No.Text, lblPack.Text, lblProd_Name.Text, lblProd_Code.Text, txtRate.Text, txtDiscount.Text, txtSchem_Fixa.Text,
                       txtSchem_Qty.Text, ddlState.SelectedValue.ToString(), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now), rbtnBased.SelectedValue.ToString(), div_code);
                }
                else
                {
                    Return = Lst.RecordAdd_Product_Scheme_New(lblPack.Text, lblProd_Name.Text, lblProd_Code.Text, txtRate.Text, txtDiscount.Text, txtSchem_Fixa.Text,
                       txtSchem_Qty.Text, ddlState.SelectedValue.ToString(), Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), rbtnBased.SelectedValue.ToString(), div_code);
                }
                if (Return > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Potential Updated Successfully');</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                }
            }
        }
        catch (Exception ex2)
        {

        }
    }




    protected void Grd_Scheme_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = e.Row.RowIndex;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;

public partial class TargetFixationProduct : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    SampleDespatch objSample = new SampleDespatch();
    SalesForce sf = new SalesForce();
    Product objProduct = new Product();
    TargetFixation objTarget = new TargetFixation();
    DataSet dsAdminSetup = null;
    DataSet dsTrans_TarVal = null;
    DataSet dsTrans_TarVal1 = null;
    string target_yearbasedon = string.Empty;
    DataSet dsTP = null;
    int time;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            // menu.FindControl("btnBack").Visible = false;

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

                AdminSetup ad = new AdminSetup();
                dsAdminSetup = ad.getOtherSetupfor_Targetyear(div_code);
                if (dsAdminSetup.Tables[0].Rows.Count > 0)
                {
                    target_yearbasedon = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    ViewState["target_yearbasedon"] = target_yearbasedon;
                }
                FillYear();
                if (target_yearbasedon != "")
                {
                    if (target_yearbasedon != "1" && target_yearbasedon != "4" && target_yearbasedon != "5" && target_yearbasedon != "6" && target_yearbasedon != "7")
                    {
                        lblperiod.Visible = true;
                        ddlmode.Visible = true;
                    }
                    else if (target_yearbasedon == "4")
                    {
                        ddlYear.Items.Clear();
                        FillYear2();
                    }
                    else if (target_yearbasedon == "5")
                    {
                        ddlYear.Items.Clear();
                        FillYear2();

                        lblperiod.Visible = true;
                        ddlmode.Visible = true;
                    }
                    else if (target_yearbasedon == "6")
                    {
                        ddlYear.Items.Clear();
                        FillYear2();

                        lblperiod.Visible = true;
                        ddlmode.Visible = true;
                    }
                    else if (target_yearbasedon == "7")
                    {
                        ddlYear.Items.Clear();
                        FillYear2();

                        lblperiod.Visible = true;
                        lblperiod.Text = "Month";
                        ddlmode.Visible = true;

                        ddlmode.Items.Add(new ListItem("Jan", "4", true));
                        ddlmode.Items.Add(new ListItem("Feb", "5", true));
                        ddlmode.Items.Add(new ListItem("Mar", "6", true));
                        ddlmode.Items.Add(new ListItem("Apr", "7", true));
                        ddlmode.Items.Add(new ListItem("May", "8", true));
                        ddlmode.Items.Add(new ListItem("Jun", "9", true));
                        ddlmode.Items.Add(new ListItem("Jul", "10", true));
                        ddlmode.Items.Add(new ListItem("Aug", "11", true));
                        ddlmode.Items.Add(new ListItem("Sep", "12", true));
                        ddlmode.Items.Add(new ListItem("Oct", "13", true));
                        ddlmode.Items.Add(new ListItem("Nov", "14", true));
                        ddlmode.Items.Add(new ListItem("Dec", "15", true));
                    }
                }
                halfyealy();

                FillManagers();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            FillColor();
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
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(int));
            dt.Columns.Add("Text", typeof(string));

            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                int m = k;
                m++;

                dt.Rows.Add(k, k.ToString() + " - " + m.ToString());
            }
            ddlYear.DataValueField = "Value";
            ddlYear.DataTextField = "Text";
            ddlYear.DataSource = dt;
            ddlYear.DataBind();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }
    }

    private void FillYear2()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");

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
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void BindGrid()
    {
        DataSet dsSalesForce = null;
        dsSalesForce = objTarget.GetTargetFixationValList(ddlFieldForce.SelectedValue, div_code);

        //
        #region Variable
        Label lblSf_Name;
        HiddenField hdnSf_Code;
        TextBox txtMonth1;
        TextBox txtMonth2;
        TextBox txtMonth3;
        TextBox txtMonth4;
        TextBox txtMonth5;
        TextBox txtMonth6;
        TextBox txtMonth7;
        TextBox txtMonth8;
        TextBox txtMonth9;
        TextBox txtMonth10;
        TextBox txtMonth11;
        TextBox txtMonth12;
        #endregion
        //

        if (dsSalesForce.Tables.Count > 0)
        {
            var dtProducts = dsSalesForce.Tables[0];

            if (ViewState["target_yearbasedon"] != null)
            {
                if (ViewState["target_yearbasedon"].ToString() == "4" || ViewState["target_yearbasedon"].ToString() == "5" || ViewState["target_yearbasedon"].ToString() == "6" || ViewState["target_yearbasedon"].ToString() == "7")
                {
                    gvTarget.Visible = false;
                    gvTarget2.Visible = true;
                    btnSubmit.Visible = true;
                    gvTarget2.DataSource = dsSalesForce;
                    gvTarget2.DataBind();

                    foreach (GridViewRow row in gvTarget2.Rows)
                    {
                        #region Variables
                        lblSf_Name = (Label)row.FindControl("lblSf_Name");
                        hdnSf_Code = (HiddenField)row.FindControl("hdnSf_Code");
                        txtMonth1 = (TextBox)row.FindControl("txtMonth1");
                        txtMonth2 = (TextBox)row.FindControl("txtMonth2");
                        txtMonth3 = (TextBox)row.FindControl("txtMonth3");
                        txtMonth4 = (TextBox)row.FindControl("txtMonth4");
                        txtMonth5 = (TextBox)row.FindControl("txtMonth5");
                        txtMonth6 = (TextBox)row.FindControl("txtMonth6");
                        txtMonth7 = (TextBox)row.FindControl("txtMonth7");
                        txtMonth8 = (TextBox)row.FindControl("txtMonth8");
                        txtMonth9 = (TextBox)row.FindControl("txtMonth9");
                        txtMonth10 = (TextBox)row.FindControl("txtMonth10");
                        txtMonth11 = (TextBox)row.FindControl("txtMonth11");
                        txtMonth12 = (TextBox)row.FindControl("txtMonth12");
                        #endregion

                        TargetFixation lst = new TargetFixation();
                        dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));

                        if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
                        {
                            txtMonth1.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jan_Val"].ToString();
                            txtMonth2.Text = dsTrans_TarVal.Tables[0].Rows[0]["Feb_Val"].ToString();
                            txtMonth3.Text = dsTrans_TarVal.Tables[0].Rows[0]["Mar_Val"].ToString();
                            txtMonth4.Text = dsTrans_TarVal.Tables[0].Rows[0]["Apr_Val"].ToString();
                            txtMonth5.Text = dsTrans_TarVal.Tables[0].Rows[0]["May_Val"].ToString();
                            txtMonth6.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jun_Val"].ToString();
                            txtMonth7.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jul_Val"].ToString();
                            txtMonth8.Text = dsTrans_TarVal.Tables[0].Rows[0]["Aug_Val"].ToString();
                            txtMonth9.Text = dsTrans_TarVal.Tables[0].Rows[0]["Sep_Val"].ToString();
                            txtMonth10.Text = dsTrans_TarVal.Tables[0].Rows[0]["Oct_Val"].ToString();
                            txtMonth11.Text = dsTrans_TarVal.Tables[0].Rows[0]["Nov_Val"].ToString();
                            txtMonth12.Text = dsTrans_TarVal.Tables[0].Rows[0]["Dec_Val"].ToString();
                        }
                    }
                }
                else
                {
                    gvTarget2.Visible = false;
                    gvTarget.Visible = true;
                    btnSubmit.Visible = true;
                    gvTarget.DataSource = dsSalesForce;
                    gvTarget.DataBind();

                    foreach (GridViewRow row in gvTarget.Rows)
                    {
                        #region Variables
                        lblSf_Name = (Label)row.FindControl("lblSf_Name");
                        hdnSf_Code = (HiddenField)row.FindControl("hdnSf_Code");
                        txtMonth1 = (TextBox)row.FindControl("txtMonth1");
                        txtMonth2 = (TextBox)row.FindControl("txtMonth2");
                        txtMonth3 = (TextBox)row.FindControl("txtMonth3");
                        txtMonth4 = (TextBox)row.FindControl("txtMonth4");
                        txtMonth5 = (TextBox)row.FindControl("txtMonth5");
                        txtMonth6 = (TextBox)row.FindControl("txtMonth6");
                        txtMonth7 = (TextBox)row.FindControl("txtMonth7");
                        txtMonth8 = (TextBox)row.FindControl("txtMonth8");
                        txtMonth9 = (TextBox)row.FindControl("txtMonth9");
                        txtMonth10 = (TextBox)row.FindControl("txtMonth10");
                        txtMonth11 = (TextBox)row.FindControl("txtMonth11");
                        txtMonth12 = (TextBox)row.FindControl("txtMonth12");
                        #endregion

                        TargetFixation lst = new TargetFixation();
                        dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));

                        TargetFixation lst1 = new TargetFixation();
                        dsTrans_TarVal1 = lst1.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, (Convert.ToInt16(ddlYear.SelectedValue)) + 1);


                        if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
                        {
                            if (dsTrans_TarVal1.Tables[0].Rows.Count > 0)
                            {
                                txtMonth1.Text = dsTrans_TarVal1.Tables[0].Rows[0]["Jan_Val"].ToString();
                                txtMonth2.Text = dsTrans_TarVal1.Tables[0].Rows[0]["Feb_Val"].ToString();
                                txtMonth3.Text = dsTrans_TarVal1.Tables[0].Rows[0]["Mar_Val"].ToString();
                            }
                            txtMonth4.Text = dsTrans_TarVal.Tables[0].Rows[0]["Apr_Val"].ToString();
                            txtMonth5.Text = dsTrans_TarVal.Tables[0].Rows[0]["May_Val"].ToString();
                            txtMonth6.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jun_Val"].ToString();
                            txtMonth7.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jul_Val"].ToString();
                            txtMonth8.Text = dsTrans_TarVal.Tables[0].Rows[0]["Aug_Val"].ToString();
                            txtMonth9.Text = dsTrans_TarVal.Tables[0].Rows[0]["Sep_Val"].ToString();
                            txtMonth10.Text = dsTrans_TarVal.Tables[0].Rows[0]["Oct_Val"].ToString();
                            txtMonth11.Text = dsTrans_TarVal.Tables[0].Rows[0]["Nov_Val"].ToString();
                            txtMonth12.Text = dsTrans_TarVal.Tables[0].Rows[0]["Dec_Val"].ToString();
                        }
                    }
                }
            }
            else
            {
                gvTarget2.Visible = false;
                gvTarget.Visible = true;
                btnSubmit.Visible = true;
                gvTarget.DataSource = dsSalesForce;
                gvTarget.DataBind();

                foreach (GridViewRow row in gvTarget.Rows)
                {
                    #region Variables
                    lblSf_Name = (Label)row.FindControl("lblSf_Name");
                    hdnSf_Code = (HiddenField)row.FindControl("hdnSf_Code");
                    txtMonth1 = (TextBox)row.FindControl("txtMonth1");
                    txtMonth2 = (TextBox)row.FindControl("txtMonth2");
                    txtMonth3 = (TextBox)row.FindControl("txtMonth3");
                    txtMonth4 = (TextBox)row.FindControl("txtMonth4");
                    txtMonth5 = (TextBox)row.FindControl("txtMonth5");
                    txtMonth6 = (TextBox)row.FindControl("txtMonth6");
                    txtMonth7 = (TextBox)row.FindControl("txtMonth7");
                    txtMonth8 = (TextBox)row.FindControl("txtMonth8");
                    txtMonth9 = (TextBox)row.FindControl("txtMonth9");
                    txtMonth10 = (TextBox)row.FindControl("txtMonth10");
                    txtMonth11 = (TextBox)row.FindControl("txtMonth11");
                    txtMonth12 = (TextBox)row.FindControl("txtMonth12");
                    #endregion

                    TargetFixation lst = new TargetFixation();
                    dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));

                    TargetFixation lst1 = new TargetFixation();
                    dsTrans_TarVal1 = lst1.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, (Convert.ToInt16(ddlYear.SelectedValue)) + 1);


                    if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
                    {
                        if (dsTrans_TarVal1.Tables[0].Rows.Count > 0)
                        {
                            txtMonth1.Text = dsTrans_TarVal1.Tables[0].Rows[0]["Jan_Val"].ToString();
                            txtMonth2.Text = dsTrans_TarVal1.Tables[0].Rows[0]["Feb_Val"].ToString();
                            txtMonth3.Text = dsTrans_TarVal1.Tables[0].Rows[0]["Mar_Val"].ToString();
                        }
                        txtMonth4.Text = dsTrans_TarVal.Tables[0].Rows[0]["Apr_Val"].ToString();
                        txtMonth5.Text = dsTrans_TarVal.Tables[0].Rows[0]["May_Val"].ToString();
                        txtMonth6.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jun_Val"].ToString();
                        txtMonth7.Text = dsTrans_TarVal.Tables[0].Rows[0]["Jul_Val"].ToString();
                        txtMonth8.Text = dsTrans_TarVal.Tables[0].Rows[0]["Aug_Val"].ToString();
                        txtMonth9.Text = dsTrans_TarVal.Tables[0].Rows[0]["Sep_Val"].ToString();
                        txtMonth10.Text = dsTrans_TarVal.Tables[0].Rows[0]["Oct_Val"].ToString();
                        txtMonth11.Text = dsTrans_TarVal.Tables[0].Rows[0]["Nov_Val"].ToString();
                        txtMonth12.Text = dsTrans_TarVal.Tables[0].Rows[0]["Dec_Val"].ToString();
                    }
                }
            }
        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtNew_TextChanged(object sender, EventArgs e)
    {
        ddlFieldForce_SelectedIndexChanged(sender, e);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //
        #region Variable
        Label lblTrans_sl_No;
        Label lblSf_Name;
        HiddenField hdnSf_Code;
        TextBox txtMonth1;
        TextBox txtMonth2;
        TextBox txtMonth3;
        TextBox txtMonth4;
        TextBox txtMonth5;
        TextBox txtMonth6;
        TextBox txtMonth7;
        TextBox txtMonth8;
        TextBox txtMonth9;
        TextBox txtMonth10;
        TextBox txtMonth11;
        TextBox txtMonth12;
        int iReturn = 0;
        #endregion
        //

        if (ViewState["target_yearbasedon"] != null)
        {
            if (ViewState["target_yearbasedon"].ToString() == "4" || ViewState["target_yearbasedon"].ToString() == "5" || ViewState["target_yearbasedon"].ToString() == "6" || ViewState["target_yearbasedon"].ToString() == "7")
            {
                System.Threading.Thread.Sleep(time);
                try
                {
                    foreach (GridViewRow gridrow in gvTarget2.Rows)
                    {
                        #region Variables
                        lblSf_Name = (Label)gridrow.FindControl("lblSf_Name");
                        hdnSf_Code = (HiddenField)gridrow.FindControl("hdnSf_Code");
                        txtMonth1 = (TextBox)gridrow.FindControl("txtMonth1");
                        txtMonth2 = (TextBox)gridrow.FindControl("txtMonth2");
                        txtMonth3 = (TextBox)gridrow.FindControl("txtMonth3");
                        txtMonth4 = (TextBox)gridrow.FindControl("txtMonth4");
                        txtMonth5 = (TextBox)gridrow.FindControl("txtMonth5");
                        txtMonth6 = (TextBox)gridrow.FindControl("txtMonth6");
                        txtMonth7 = (TextBox)gridrow.FindControl("txtMonth7");
                        txtMonth8 = (TextBox)gridrow.FindControl("txtMonth8");
                        txtMonth9 = (TextBox)gridrow.FindControl("txtMonth9");
                        txtMonth10 = (TextBox)gridrow.FindControl("txtMonth10");
                        txtMonth11 = (TextBox)gridrow.FindControl("txtMonth11");
                        txtMonth12 = (TextBox)gridrow.FindControl("txtMonth12");
                        #endregion

                        iReturn = TargetFixation_ValuewiseEntry(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5, txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);
                    }

                    if (iReturn < 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target Fixation Valuewise Entry Updated Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message);
                }
            }
            else
            {
                System.Threading.Thread.Sleep(time);
                try
                {

                    foreach (GridViewRow gridrow in gvTarget.Rows)
                    {
                        #region Variables
                        lblSf_Name = (Label)gridrow.FindControl("lblSf_Name");
                        hdnSf_Code = (HiddenField)gridrow.FindControl("hdnSf_Code");
                        txtMonth1 = (TextBox)gridrow.FindControl("txtMonth1");
                        txtMonth2 = (TextBox)gridrow.FindControl("txtMonth2");
                        txtMonth3 = (TextBox)gridrow.FindControl("txtMonth3");
                        txtMonth4 = (TextBox)gridrow.FindControl("txtMonth4");
                        txtMonth5 = (TextBox)gridrow.FindControl("txtMonth5");
                        txtMonth6 = (TextBox)gridrow.FindControl("txtMonth6");
                        txtMonth7 = (TextBox)gridrow.FindControl("txtMonth7");
                        txtMonth8 = (TextBox)gridrow.FindControl("txtMonth8");
                        txtMonth9 = (TextBox)gridrow.FindControl("txtMonth9");
                        txtMonth10 = (TextBox)gridrow.FindControl("txtMonth10");
                        txtMonth11 = (TextBox)gridrow.FindControl("txtMonth11");
                        txtMonth12 = (TextBox)gridrow.FindControl("txtMonth12");
                        #endregion

                        iReturn = TargetFixation_ValuewiseEntry1(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5, txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);

                    }

                    if (iReturn < 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target Fixation Valuewise Entry Updated Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message);
                }
            }
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            try
            {
                foreach (GridViewRow gridrow in gvTarget.Rows)
                {
                    #region Variables
                    lblTrans_sl_No = (Label)gridrow.FindControl("lblTrans_sl_No");
                    lblSf_Name = (Label)gridrow.FindControl("lblSf_Name");
                    hdnSf_Code = (HiddenField)gridrow.FindControl("hdnSf_Code");
                    txtMonth1 = (TextBox)gridrow.FindControl("txtMonth1");
                    txtMonth2 = (TextBox)gridrow.FindControl("txtMonth2");
                    txtMonth3 = (TextBox)gridrow.FindControl("txtMonth3");
                    txtMonth4 = (TextBox)gridrow.FindControl("txtMonth4");
                    txtMonth5 = (TextBox)gridrow.FindControl("txtMonth5");
                    txtMonth6 = (TextBox)gridrow.FindControl("txtMonth6");
                    txtMonth7 = (TextBox)gridrow.FindControl("txtMonth7");
                    txtMonth8 = (TextBox)gridrow.FindControl("txtMonth8");
                    txtMonth9 = (TextBox)gridrow.FindControl("txtMonth9");
                    txtMonth10 = (TextBox)gridrow.FindControl("txtMonth10");
                    txtMonth11 = (TextBox)gridrow.FindControl("txtMonth11");
                    txtMonth12 = (TextBox)gridrow.FindControl("txtMonth12");
                    #endregion

                    iReturn = TargetFixation_ValuewiseEntry1(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5, txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);

                }

                if (iReturn < 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target Fixation Valuewise Entry Updated Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
                }
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }
    }
    //
    #region TargetFixation_ValuewiseEntry
    private int TargetFixation_ValuewiseEntry(HiddenField hdnSf_Code, Label lblSf_Name, TextBox txtMonth1, TextBox txtMonth2, TextBox txtMonth3, TextBox txtMonth4, TextBox txtMonth5, TextBox txtMonth6, TextBox txtMonth7, TextBox txtMonth8, TextBox txtMonth9, TextBox txtMonth10, TextBox txtMonth11, TextBox txtMonth12)
    {
        int iReturn = -1;

        TargetFixation lst = new TargetFixation();

        if (txtMonth1.Text != string.Empty || txtMonth2.Text != string.Empty || txtMonth3.Text != string.Empty ||
            txtMonth4.Text != string.Empty || txtMonth5.Text != string.Empty || txtMonth6.Text != string.Empty ||
            txtMonth7.Text != string.Empty || txtMonth8.Text != string.Empty || txtMonth9.Text != string.Empty ||
            txtMonth10.Text != string.Empty || txtMonth11.Text != string.Empty || txtMonth12.Text != string.Empty)
        {
            dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));

            if (dsTrans_TarVal.Tables[0].Rows.Count == 0)
            {
                iReturn = lst.RecordAddTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), Convert.ToInt32(ddlYear.SelectedValue), lblSf_Name.Text.ToString().Trim(),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth4.Text) ? null : txtMonth4.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth5.Text) ? null : txtMonth5.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth6.Text) ? null : txtMonth6.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth7.Text) ? null : txtMonth7.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth8.Text) ? null : txtMonth8.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth9.Text) ? null : txtMonth9.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth10.Text) ? null : txtMonth10.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth11.Text) ? null : txtMonth11.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth12.Text) ? null : txtMonth12.Text));
            }
            else
            {
                iReturn = lst.RecordUpdateTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), Convert.ToInt32(ddlYear.SelectedValue), 
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth4.Text) ? null : txtMonth4.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth5.Text) ? null : txtMonth5.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth6.Text) ? null : txtMonth6.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth7.Text) ? null : txtMonth7.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth8.Text) ? null : txtMonth8.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth9.Text) ? null : txtMonth9.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth10.Text) ? null : txtMonth10.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth11.Text) ? null : txtMonth11.Text),
                    Convert.ToDecimal(string.IsNullOrEmpty(txtMonth12.Text) ? null : txtMonth12.Text));
            }
        }
        else if (txtMonth1.Text == string.Empty && txtMonth2.Text == string.Empty && txtMonth3.Text == string.Empty &&
                txtMonth4.Text == string.Empty && txtMonth5.Text == string.Empty && txtMonth6.Text == string.Empty &&
                txtMonth7.Text == string.Empty && txtMonth8.Text == string.Empty && txtMonth9.Text == string.Empty &&
                txtMonth10.Text == string.Empty && txtMonth11.Text == string.Empty && txtMonth12.Text == string.Empty)
        {
            dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));

            if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
            {
                if (ViewState["target_yearbasedon"].ToString() != "1" && ViewState["target_yearbasedon"].ToString() != "2" && ViewState["target_yearbasedon"].ToString() != "3" &&
                    ViewState["target_yearbasedon"].ToString() != "5" && ViewState["target_yearbasedon"].ToString() != "6" && ViewState["target_yearbasedon"].ToString() != "7")
                {
                    iReturn = lst.RecordDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue));
                }
                else if (ViewState["target_yearbasedon"].ToString() == "2" && ddlmode.SelectedValue == "1")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 2);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "1")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 4);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "2")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 5);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "3")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 6);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "5" && ddlmode.SelectedValue == "1")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 8);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "5" && ddlmode.SelectedValue == "2")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 9);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "6" && ddlmode.SelectedValue == "1")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 10);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "6" && ddlmode.SelectedValue == "2")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 11);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "6" && ddlmode.SelectedValue == "3")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 12);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "6" && ddlmode.SelectedValue == "4")
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 13);
                }
                else if (ViewState["target_yearbasedon"].ToString() == "7")
                {
                    iReturn = lst.RecordMonDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, "d." + ddlmode.SelectedItem.Text.ToString().Trim() + "_Val");
                }
            }
        }
        return iReturn;
    }
    #endregion
    #region TargetFixation_ValuewiseEntry1
    private int TargetFixation_ValuewiseEntry1(HiddenField hdnSf_Code, Label lblSf_Name, TextBox txtMonth1, TextBox txtMonth2, TextBox txtMonth3, TextBox txtMonth4, TextBox txtMonth5, TextBox txtMonth6, TextBox txtMonth7, TextBox txtMonth8, TextBox txtMonth9, TextBox txtMonth10, TextBox txtMonth11, TextBox txtMonth12)
    {
        int iReturn = -1;

        TargetFixation lst = new TargetFixation();

        if (ViewState["target_yearbasedon"].ToString() == "1")
        {

            if (txtMonth1.Text != string.Empty || txtMonth2.Text != string.Empty || txtMonth3.Text != string.Empty ||
                txtMonth4.Text != string.Empty || txtMonth5.Text != string.Empty || txtMonth6.Text != string.Empty ||
                txtMonth7.Text != string.Empty || txtMonth8.Text != string.Empty || txtMonth9.Text != string.Empty ||
                txtMonth10.Text != string.Empty || txtMonth11.Text != string.Empty || txtMonth12.Text != string.Empty)
            {
                dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));
                dsTrans_TarVal1 = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, (Convert.ToInt16(ddlYear.SelectedValue))+1);

                if (dsTrans_TarVal.Tables[0].Rows.Count == 0)
                {
                    iReturn = lst.RecordAddTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), Convert.ToInt32(ddlYear.SelectedValue), lblSf_Name.Text.ToString().Trim(), null, null, null,
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth4.Text) ? null : txtMonth4.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth5.Text) ? null : txtMonth5.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth6.Text) ? null : txtMonth6.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth7.Text) ? null : txtMonth7.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth8.Text) ? null : txtMonth8.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth9.Text) ? null : txtMonth9.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth10.Text) ? null : txtMonth10.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth11.Text) ? null : txtMonth11.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth12.Text) ? null : txtMonth12.Text));
                }
                else
                {
                    iReturn = lst.RecordUpdateTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null,
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth4.Text) ? null : txtMonth4.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth5.Text) ? null : txtMonth5.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth6.Text) ? null : txtMonth6.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth7.Text) ? null : txtMonth7.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth8.Text) ? null : txtMonth8.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth9.Text) ? null : txtMonth9.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth10.Text) ? null : txtMonth10.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth11.Text) ? null : txtMonth11.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth12.Text) ? null : txtMonth12.Text));
                }
                if (dsTrans_TarVal1.Tables[0].Rows.Count == 0)
                {
                    iReturn = lst.RecordAddTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), (Convert.ToInt32(ddlYear.SelectedValue)) + 1, lblSf_Name.Text.ToString().Trim(), Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                        null, null, null, null, null, null, null, null, null);
                }
                else
                {
                    iReturn = lst.RecordUpdateTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), (Convert.ToInt32(ddlYear.SelectedValue)) + 1, 
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                        null, null, null, null, null, null, null, null, null);
                }

            }
            else if (txtMonth1.Text == string.Empty && txtMonth2.Text == string.Empty && txtMonth3.Text == string.Empty &&
                txtMonth4.Text == string.Empty && txtMonth5.Text == string.Empty && txtMonth6.Text == string.Empty &&
                txtMonth7.Text == string.Empty && txtMonth8.Text == string.Empty && txtMonth9.Text == string.Empty &&
                txtMonth10.Text == string.Empty && txtMonth11.Text == string.Empty && txtMonth12.Text == string.Empty)
            {
                dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));
                
                if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 1);
                }
            }
        }
        else if (ViewState["target_yearbasedon"].ToString() == "2" && ddlmode.SelectedValue == "1")
        {
            iReturn = TargetFixation_ValuewiseEntry(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5, 
                txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);
            return iReturn;
        }
        else if (ViewState["target_yearbasedon"].ToString() == "2" && ddlmode.SelectedValue == "2")
        {
            if (txtMonth1.Text != string.Empty || txtMonth2.Text != string.Empty || txtMonth3.Text != string.Empty ||
                txtMonth4.Text != string.Empty || txtMonth5.Text != string.Empty || txtMonth6.Text != string.Empty ||
                txtMonth7.Text != string.Empty || txtMonth8.Text != string.Empty || txtMonth9.Text != string.Empty ||
                txtMonth10.Text != string.Empty || txtMonth11.Text != string.Empty || txtMonth12.Text != string.Empty)
            {
                dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));
                dsTrans_TarVal1 = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, (Convert.ToInt16(ddlYear.SelectedValue))+1);

                if (dsTrans_TarVal.Tables[0].Rows.Count == 0)
                {
                    iReturn = lst.RecordAddTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), Convert.ToInt32(ddlYear.SelectedValue), lblSf_Name.Text.ToString().Trim(), null, null, null,
                        null, null, null, null, null, null, 
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth10.Text) ? null : txtMonth10.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth11.Text) ? null : txtMonth11.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth12.Text) ? null : txtMonth12.Text));
                }
                else
                {
                    iReturn = lst.RecordUpdateTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null,
                        null, null, null, null, null, null, 
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth10.Text) ? null : txtMonth10.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth11.Text) ? null : txtMonth11.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth12.Text) ? null : txtMonth12.Text));
                }
                if (dsTrans_TarVal1.Tables[0].Rows.Count == 0)
                {
                    iReturn = lst.RecordAddTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), (Convert.ToInt32(ddlYear.SelectedValue)) + 1, lblSf_Name.Text.ToString().Trim(), Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                        null, null, null, null, null, null, null, null, null);
                }
                else
                {
                    iReturn = lst.RecordUpdateTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), (Convert.ToInt32(ddlYear.SelectedValue)) + 1, 
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                        null, null, null, null, null, null, null, null, null);
                }
            }
            else if (txtMonth1.Text == string.Empty && txtMonth2.Text == string.Empty && txtMonth3.Text == string.Empty &&
                txtMonth4.Text == string.Empty && txtMonth5.Text == string.Empty && txtMonth6.Text == string.Empty &&
                txtMonth7.Text == string.Empty && txtMonth8.Text == string.Empty && txtMonth9.Text == string.Empty &&
                txtMonth10.Text == string.Empty && txtMonth11.Text == string.Empty && txtMonth12.Text == string.Empty)
            {
                dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, Convert.ToInt16(ddlYear.SelectedValue));

                if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 3);
                }
            }
        }
        else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "1")
        {
            iReturn = TargetFixation_ValuewiseEntry(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5,
                txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);
            return iReturn;
        }
        else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "2")
        {
            iReturn = TargetFixation_ValuewiseEntry(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5,
                txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);
            return iReturn;
        }
        else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "3")
        {
            iReturn = TargetFixation_ValuewiseEntry(hdnSf_Code, lblSf_Name, txtMonth1, txtMonth2, txtMonth3, txtMonth4, txtMonth5,
                txtMonth6, txtMonth7, txtMonth8, txtMonth9, txtMonth10, txtMonth11, txtMonth12);
            return iReturn;
        }
        else if (ViewState["target_yearbasedon"].ToString() == "3" && ddlmode.SelectedValue == "4")
        {
            if (txtMonth1.Text != string.Empty || txtMonth2.Text != string.Empty || txtMonth3.Text != string.Empty ||
                txtMonth4.Text != string.Empty || txtMonth5.Text != string.Empty || txtMonth6.Text != string.Empty ||
                txtMonth7.Text != string.Empty || txtMonth8.Text != string.Empty || txtMonth9.Text != string.Empty ||
                txtMonth10.Text != string.Empty || txtMonth11.Text != string.Empty || txtMonth12.Text != string.Empty)
            {
                dsTrans_TarVal1 = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, (Convert.ToInt16(ddlYear.SelectedValue)) + 1);
                
                if (dsTrans_TarVal1.Tables[0].Rows.Count == 0)
                {
                    iReturn = lst.RecordAddTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), (Convert.ToInt32(ddlYear.SelectedValue)) + 1, lblSf_Name.Text.ToString().Trim(), Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                        null, null, null, null, null, null, null, null, null);
                }
                else
                {
                    iReturn = lst.RecordUpdateTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt16(div_code), (Convert.ToInt32(ddlYear.SelectedValue)) + 1, 
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth1.Text) ? null : txtMonth1.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth2.Text) ? null : txtMonth2.Text),
                        Convert.ToDecimal(string.IsNullOrEmpty(txtMonth3.Text) ? null : txtMonth3.Text),
                        null, null, null, null, null, null, null, null, null);
                }
            }
            else if (txtMonth1.Text == string.Empty && txtMonth2.Text == string.Empty && txtMonth3.Text == string.Empty &&
                txtMonth4.Text == string.Empty && txtMonth5.Text == string.Empty && txtMonth6.Text == string.Empty &&
                txtMonth7.Text == string.Empty && txtMonth8.Text == string.Empty && txtMonth9.Text == string.Empty &&
                txtMonth10.Text == string.Empty && txtMonth11.Text == string.Empty && txtMonth12.Text == string.Empty)
            {
                dsTrans_TarVal = lst.TargetFixationValuewise_RecordExist(hdnSf_Code.Value, div_code, (Convert.ToInt16(ddlYear.SelectedValue)));

                if (dsTrans_TarVal.Tables[0].Rows.Count > 0)
                {
                    iReturn = lst.RecordFDelTargetFixation_Valuewise(hdnSf_Code.Value, Convert.ToInt32(div_code), Convert.ToInt32(ddlYear.SelectedValue), null, null, null, null, null, null, null, null, null, null, null, null, 7);
                }
            }
        }
        return iReturn;
    }
    #endregion
    protected void CmdGo_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    private void ClearControls()
    {
        gvTarget2.Visible = false;
        gvTarget.Visible = false;
        btnSubmit.Visible = false;
        ddlFieldForce.SelectedIndex = 0;
    }

    private void halfyealy()
    {
        if (ViewState["target_yearbasedon"] != null)
        {
            if (ViewState["target_yearbasedon"].ToString() == "2")
            {
                int yr = Convert.ToInt16(ddlYear.SelectedValue);
                yr++;
                ddlmode.Items.Add(new ListItem("Apr " + ddlYear.SelectedValue + " - Sep " + ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("Oct " + ddlYear.SelectedValue + " - March " + yr, "2", true));
            }
            else if (ViewState["target_yearbasedon"].ToString() == "3")
            {
                int yr = Convert.ToInt16(ddlYear.SelectedValue);
                yr++;
                ddlmode.Items.Add(new ListItem("Apr " + ddlYear.SelectedValue + " - Jun " + ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("July " + ddlYear.SelectedValue + " - Sep " + ddlYear.SelectedValue, "2", true));
                ddlmode.Items.Add(new ListItem("Oct " + ddlYear.SelectedValue + "- Dec " + ddlYear.SelectedValue, "3", true));
                ddlmode.Items.Add(new ListItem("Jan " + yr + " - March " + yr, "4", true));
            }
            else if (ViewState["target_yearbasedon"].ToString() == "5")
            {
                ddlmode.Items.Add(new ListItem("Jan " + ddlYear.SelectedValue + " - Jun " + ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("July " + ddlYear.SelectedValue + " - Dec " + ddlYear.SelectedValue, "2", true));
            }
            else if (ViewState["target_yearbasedon"].ToString() == "6")
            {
                ddlmode.Items.Add(new ListItem("Jan " + ddlYear.SelectedValue + " - Mar " + ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("Apr " + ddlYear.SelectedValue + " - June " + ddlYear.SelectedValue, "2", true));
                ddlmode.Items.Add(new ListItem("July " + ddlYear.SelectedValue + " - Sep " + ddlYear.SelectedValue, "3", true));
                ddlmode.Items.Add(new ListItem("Oct " + ddlYear.SelectedValue + "- Dec " + ddlYear.SelectedValue, "4", true));
            }
            else if (ViewState["target_yearbasedon"].ToString() == "7")
            {
                ddlmode.Items.Clear();
                ddlmode.Items.Add(new ListItem("Jan", "4", true));
                ddlmode.Items.Add(new ListItem("Feb", "5", true));
                ddlmode.Items.Add(new ListItem("Mar", "6", true));
                ddlmode.Items.Add(new ListItem("Apr", "7", true));
                ddlmode.Items.Add(new ListItem("May", "8", true));
                ddlmode.Items.Add(new ListItem("Jun", "9", true));
                ddlmode.Items.Add(new ListItem("Jul", "10", true));
                ddlmode.Items.Add(new ListItem("Aug", "11", true));
                ddlmode.Items.Add(new ListItem("Sep", "12", true));
                ddlmode.Items.Add(new ListItem("Oct", "13", true));
                ddlmode.Items.Add(new ListItem("Nov", "14", true));
                ddlmode.Items.Add(new ListItem("Dec", "15", true));
            }
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlmode.Items.Clear();
        halfyealy();
    }

    protected void gvTarget_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["target_yearbasedon"] != null)
        {
            if (ViewState["target_yearbasedon"].ToString() == "2")
            {
                if (ddlmode.SelectedValue == "1")
                {
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;
                    this.gvTarget.Columns[14].Visible = false;
                    this.gvTarget.Columns[15].Visible = false;

                    this.gvTarget.Columns[4].Visible = true;
                    this.gvTarget.Columns[5].Visible = true;
                    this.gvTarget.Columns[6].Visible = true;
                    this.gvTarget.Columns[7].Visible = true;
                    this.gvTarget.Columns[8].Visible = true;
                    this.gvTarget.Columns[9].Visible = true;
                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;

                    this.gvTarget.Columns[10].Visible = true;
                    this.gvTarget.Columns[11].Visible = true;
                    this.gvTarget.Columns[12].Visible = true;
                    this.gvTarget.Columns[13].Visible = true;
                    this.gvTarget.Columns[14].Visible = true;
                    this.gvTarget.Columns[15].Visible = true;
                }
            }
            else if (ViewState["target_yearbasedon"].ToString() == "3")
            {
                if (ddlmode.SelectedValue == "1")
                {
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;
                    this.gvTarget.Columns[14].Visible = false;
                    this.gvTarget.Columns[15].Visible = false;

                    this.gvTarget.Columns[4].Visible = true;
                    this.gvTarget.Columns[5].Visible = true;
                    this.gvTarget.Columns[6].Visible = true;
                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;
                    this.gvTarget.Columns[14].Visible = false;
                    this.gvTarget.Columns[15].Visible = false;

                    this.gvTarget.Columns[7].Visible = true;
                    this.gvTarget.Columns[8].Visible = true;
                    this.gvTarget.Columns[9].Visible = true;
                }
                else if (ddlmode.SelectedValue == "3")
                {
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;
                    this.gvTarget.Columns[14].Visible = false;
                    this.gvTarget.Columns[15].Visible = false;

                    this.gvTarget.Columns[10].Visible = true;
                    this.gvTarget.Columns[11].Visible = true;
                    this.gvTarget.Columns[12].Visible = true;
                }
                else if (ddlmode.SelectedValue == "4")
                {
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;

                    this.gvTarget.Columns[13].Visible = true;
                    this.gvTarget.Columns[14].Visible = true;
                    this.gvTarget.Columns[15].Visible = true;
                }
            }
            else if (ViewState["target_yearbasedon"].ToString() == "5")
            {
                if (ddlmode.SelectedValue == "1")
                {
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;
                    this.gvTarget2.Columns[14].Visible = false;
                    this.gvTarget2.Columns[15].Visible = false;

                    this.gvTarget2.Columns[4].Visible = true;
                    this.gvTarget2.Columns[5].Visible = true;
                    this.gvTarget2.Columns[6].Visible = true;
                    this.gvTarget2.Columns[7].Visible = true;
                    this.gvTarget2.Columns[8].Visible = true;
                    this.gvTarget2.Columns[9].Visible = true;
                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;

                    this.gvTarget2.Columns[10].Visible = true;
                    this.gvTarget2.Columns[11].Visible = true;
                    this.gvTarget2.Columns[12].Visible = true;
                    this.gvTarget2.Columns[13].Visible = true;
                    this.gvTarget2.Columns[14].Visible = true;
                    this.gvTarget2.Columns[15].Visible = true;
                }
            }
            else if (ViewState["target_yearbasedon"].ToString() == "6")
            {
                if (ddlmode.SelectedValue == "1")
                {
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;
                    this.gvTarget2.Columns[14].Visible = false;
                    this.gvTarget2.Columns[15].Visible = false;

                    this.gvTarget2.Columns[4].Visible = true;
                    this.gvTarget2.Columns[5].Visible = true;
                    this.gvTarget2.Columns[6].Visible = true;
                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;
                    this.gvTarget2.Columns[14].Visible = false;
                    this.gvTarget2.Columns[15].Visible = false;

                    this.gvTarget2.Columns[7].Visible = true;
                    this.gvTarget2.Columns[8].Visible = true;
                    this.gvTarget2.Columns[9].Visible = true;
                }
                else if (ddlmode.SelectedValue == "3")
                {
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;
                    this.gvTarget2.Columns[14].Visible = false;
                    this.gvTarget2.Columns[15].Visible = false;

                    this.gvTarget2.Columns[10].Visible = true;
                    this.gvTarget2.Columns[11].Visible = true;
                    this.gvTarget2.Columns[12].Visible = true;
                }
                else if (ddlmode.SelectedValue == "4")
                {
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;

                    this.gvTarget2.Columns[13].Visible = true;
                    this.gvTarget2.Columns[14].Visible = true;
                    this.gvTarget2.Columns[15].Visible = true;
                }
            }

            else if (ViewState["target_yearbasedon"].ToString() == "7")
            {
                for (int i = 4; i < gvTarget2.Columns.Count; i++)
                {
                    if (i == Convert.ToInt16(ddlmode.SelectedValue))
                    {
                        gvTarget2.Columns[i].Visible = true;
                    }
                    else
                    {
                        gvTarget2.Columns[i].Visible = false;
                    }
                }
            }
        }
    }
}
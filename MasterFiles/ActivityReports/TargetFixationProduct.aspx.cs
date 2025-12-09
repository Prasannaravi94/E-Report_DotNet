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
    string target_yearbasedon = string.Empty;
    DataSet dsTP = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!IsPostBack)
        {
            this.FillMasterList();
            //this.BindGrid();
            // menu.FindControl("btnBack").Visible = false;

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
                    //ddlmode.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (target_yearbasedon == "4")
                {
                    ddlYear.Items.Clear();
                    FillYear2();
                    //ddlYear.Items.Add(new ListItem("--Select--", "0", true));
                    //ddlYear.Items.Add(new ListItem("2015", "2015", true));
                    //ddlYear.Items.Add(new ListItem("2016", "2016", true));
                    //ddlYear.Items.Add(new ListItem("2017", "2017", true));
                    //ddlYear.Items.Add(new ListItem("2018", "2018", true));
                    //ddlYear.Items.Add(new ListItem("2019", "2019", true));
                    //ddlYear.Items.Add(new ListItem("2020", "2020", true));
                }

                else if (target_yearbasedon == "5")
                {
                    ddlYear.Items.Clear();
                    FillYear2();
                    //ddlYear.Items.Add(new ListItem("--Select--", "0", true));
                    //ddlYear.Items.Add(new ListItem("2015", "2015", true));
                    //ddlYear.Items.Add(new ListItem("2016", "2016", true));
                    //ddlYear.Items.Add(new ListItem("2017", "2017", true));
                    //ddlYear.Items.Add(new ListItem("2018", "2018", true));
                    //ddlYear.Items.Add(new ListItem("2019", "2019", true));
                    //ddlYear.Items.Add(new ListItem("2020", "2020", true));

                    lblperiod.Visible = true;
                    ddlmode.Visible = true;
                   // ddlmode.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else if (target_yearbasedon == "6")
                {
                    ddlYear.Items.Clear();
                    FillYear2();
                    //ddlYear.Items.Add(new ListItem("--Select--", "0", true));
                    //ddlYear.Items.Add(new ListItem("2015", "2015", true));
                    //ddlYear.Items.Add(new ListItem("2016", "2016", true));
                    //ddlYear.Items.Add(new ListItem("2017", "2017", true));
                    //ddlYear.Items.Add(new ListItem("2018", "2018", true));
                    //ddlYear.Items.Add(new ListItem("2019", "2019", true));
                    //ddlYear.Items.Add(new ListItem("2020", "2020", true));

                    lblperiod.Visible = true;
                    ddlmode.Visible = true;
                    //ddlmode.Items.Insert(0, new ListItem("--Select--", "0"));
                }

                else if (target_yearbasedon == "7")
                {
                    ddlYear.Items.Clear();
                    FillYear2();
                    //ddlYear.Items.Add(new ListItem("--Select--", "0", true));
                    //ddlYear.Items.Add(new ListItem("2015", "2015", true));
                    //ddlYear.Items.Add(new ListItem("2016", "2016", true));
                    //ddlYear.Items.Add(new ListItem("2017", "2017", true));
                    //ddlYear.Items.Add(new ListItem("2018", "2018", true));
                    //ddlYear.Items.Add(new ListItem("2019", "2019", true));
                    //ddlYear.Items.Add(new ListItem("2020", "2020", true));

                    lblperiod.Visible = true;
                    lblperiod.Text = "Month";
                    ddlmode.Visible = true;

                    ddlmode.Items.Add(new ListItem("Jan", "2", true));
                    ddlmode.Items.Add(new ListItem("Feb", "3", true));
                    ddlmode.Items.Add(new ListItem("Mar", "4", true));
                    ddlmode.Items.Add(new ListItem("Apr", "5", true));
                    ddlmode.Items.Add(new ListItem("May", "6", true));
                    ddlmode.Items.Add(new ListItem("Jun", "7", true));
                    ddlmode.Items.Add(new ListItem("Jul", "8", true));
                    ddlmode.Items.Add(new ListItem("Aug", "9", true));
                    ddlmode.Items.Add(new ListItem("Sep", "10", true));
                    ddlmode.Items.Add(new ListItem("Oct", "11", true));
                    ddlmode.Items.Add(new ListItem("Nov", "12", true));
                    ddlmode.Items.Add(new ListItem("Dec", "13", true));


                }
            }
            halfyealy();
        }
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
               // ddlYear.Items.Add(k.ToString() + " - " + m.ToString());
                //ddlTYear.Items.Add(k.ToString());
                
                dt.Rows.Add(k, k.ToString() + " - " + m.ToString());
               // ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
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


    private void FillMasterList()
    {
        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }

    protected void BindGrid()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_sfhqcode(ddlFieldForce.SelectedValue);

        string hqcode = string.Empty;
        hqcode = dsTP.Tables[0].Rows[0]["sf_cat_code"].ToString();
        DataSet dsProducts = null;

        dsProducts = objTarget.GetTargetFixationList(hqcode,ddlFieldForce.SelectedValue, div_code, ddlYear.SelectedValue);
        if (dsProducts.Tables.Count > 0)
        {
            var dtProducts = dsProducts.Tables[0];
            //var transSlNo = from dt in dtProducts
            //                where dt.Field<string>("Trans_sl_No") != string.Empty
            //                select dt;

            DataRow drow = dtProducts.AsEnumerable().Where(p => p.Field<decimal>("Trans_sl_No") > 0).FirstOrDefault();

            if (drow != null)
            {
                hdnTransSlNo.Value = Convert.ToString(drow["Trans_sl_No"]);
            }

            if (ViewState["target_yearbasedon"] != null)
            {
                if (ViewState["target_yearbasedon"].ToString() == "4" || ViewState["target_yearbasedon"].ToString() == "5" || ViewState["target_yearbasedon"].ToString() == "6" || ViewState["target_yearbasedon"].ToString() == "7")
                {
                    gvTarget.Visible = false;
                    gvTarget2.Visible = true;
                    btnSubmit.Visible = true;
                    gvTarget2.DataSource = dsProducts;
                    gvTarget2.DataBind();
                }
                else
                {
                    gvTarget2.Visible = false;
                    gvTarget.Visible = true;
                    btnSubmit.Visible = true;
                    gvTarget.DataSource = dsProducts;
                    gvTarget.DataBind();
                }
            }
            else
            {
                gvTarget2.Visible = false;
                gvTarget.Visible = true;
                btnSubmit.Visible = true;
                gvTarget.DataSource = dsProducts;
                gvTarget.DataBind();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string output = string.Empty;
        if (hdnTransSlNo.Value == string.Empty)
        {
            output = objTarget.RecordHeadAdd(ddlFieldForce.SelectedValue, div_code, ddlYear.SelectedValue);
        }
        else
        {
            if (ViewState["target_yearbasedon"] != null)
            {
                if (ViewState["target_yearbasedon"].ToString() == "4" || ViewState["target_yearbasedon"].ToString() == "5" || ViewState["target_yearbasedon"].ToString() == "6" || ViewState["target_yearbasedon"].ToString() == "7")
                {
                    for (int rowcnt = 0; rowcnt < gvTarget2.Rows.Count; rowcnt++)
                    {
                        GridViewRow gvrow = gvTarget2.Rows[rowcnt];
                        HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                        for (int monCnt = 1; monCnt <= 12; monCnt++)
                        {
                            TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                            if (txtMonthValue != null)
                            {
                                if (txtMonthValue.Text != string.Empty)
                                {
                                    objTarget.RecordUpdate_TargetMain(hdnTransSlNo.Value);
                                    objTarget.RecordDelete_TargetDetails(hdnTransSlNo.Value, monCnt);

                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int rowcnt = 0; rowcnt < gvTarget.Rows.Count; rowcnt++)
                    {
                        GridViewRow gvrow = gvTarget.Rows[rowcnt];
                        HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                        for (int monCnt = 1; monCnt <= 12; monCnt++)
                        {
                            TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                            if (txtMonthValue != null)
                            {
                                if (txtMonthValue.Text != string.Empty)
                                {
                                    objTarget.RecordUpdate_TargetMain(hdnTransSlNo.Value);
                                    objTarget.RecordDelete_TargetDetails(hdnTransSlNo.Value, monCnt);

                                }
                            }
                        }
                    }
                }
            }
            else
            {

                for (int rowcnt = 0; rowcnt < gvTarget.Rows.Count; rowcnt++)
                {
                    GridViewRow gvrow = gvTarget.Rows[rowcnt];
                    HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                    for (int monCnt = 1; monCnt <= 12; monCnt++)
                    {
                        TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                        if (txtMonthValue != null)
                        {
                            if (txtMonthValue.Text != string.Empty)
                            {
                                objTarget.RecordUpdate_TargetMain(hdnTransSlNo.Value);
                                objTarget.RecordDelete_TargetDetails(hdnTransSlNo.Value, monCnt);

                            }
                        }
                    }
                }
            }

            output = hdnTransSlNo.Value;
            hdnTransSlNo.Value = string.Empty;
          
        }

        if (output != "0")
        {
            if (ViewState["target_yearbasedon"] != null)
            {
                if (ViewState["target_yearbasedon"].ToString() == "4" || ViewState["target_yearbasedon"].ToString() == "5" || ViewState["target_yearbasedon"].ToString() == "6" || ViewState["target_yearbasedon"].ToString() == "7")
                {
                    for (int rowcnt = 0; rowcnt < gvTarget2.Rows.Count; rowcnt++)
                    {
                        GridViewRow gvrow = gvTarget2.Rows[rowcnt];
                        HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                        for (int monCnt = 1; monCnt <= 12; monCnt++)
                        {
                            TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                            if (txtMonthValue != null)
                            {
                                if (txtMonthValue.Text != string.Empty)
                                    objTarget.RecordDetailsAdd(output, hdnProdCode.Value, txtMonthValue.ID.Replace("txtMonth", ""), float.Parse(txtMonthValue.Text), ddlFieldForce.SelectedValue, div_code);
                            }
                        }
                    }
                }
                else
                {
                    for (int rowcnt = 0; rowcnt < gvTarget.Rows.Count; rowcnt++)
                    {
                        GridViewRow gvrow = gvTarget.Rows[rowcnt];
                        HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                        for (int monCnt = 1; monCnt <= 12; monCnt++)
                        {
                            TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                            if (txtMonthValue != null)
                            {
                                if (txtMonthValue.Text != string.Empty)
                                    objTarget.RecordDetailsAdd(output, hdnProdCode.Value, txtMonthValue.ID.Replace("txtMonth", ""), float.Parse(txtMonthValue.Text), ddlFieldForce.SelectedValue, div_code);
                            }
                        }
                    }
                }
            }

            else
            {
                for (int rowcnt = 0; rowcnt < gvTarget.Rows.Count; rowcnt++)
                {
                    GridViewRow gvrow = gvTarget.Rows[rowcnt];
                    HiddenField hdnProdCode = (HiddenField)gvrow.FindControl("hdnProdCode");
                    for (int monCnt = 1; monCnt <= 12; monCnt++)
                    {
                        TextBox txtMonthValue = (TextBox)gvrow.FindControl("txtMonth" + Convert.ToString(monCnt));
                        if (txtMonthValue != null)
                        {
                            if (txtMonthValue.Text != string.Empty)
                                objTarget.RecordDetailsAdd(output, hdnProdCode.Value, txtMonthValue.ID.Replace("txtMonth", ""), float.Parse(txtMonthValue.Text), ddlFieldForce.SelectedValue, div_code);
                        }
                    }
                }
            }

            this.ClearControls();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Target Fixation Saved Successfully!');", true);
        }
    }
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
       //ddlYear.SelectedIndex = 0;
       // ddlmode.Items.Clear();
        //ddlmode.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    private void halfyealy()
    {
        if (ViewState["target_yearbasedon"] != null)
        {

            if (ViewState["target_yearbasedon"].ToString() == "2")
            {
                int yr = Convert.ToInt16(ddlYear.SelectedValue);
                yr++;
                ddlmode.Items.Add(new ListItem("Apr " + ddlYear.SelectedValue + " - Sep " + ddlYear.SelectedValue , "1", true));
                ddlmode.Items.Add(new ListItem("Oct " + ddlYear.SelectedValue + " - March " + yr, "2", true));
            }

            else if (ViewState["target_yearbasedon"].ToString() == "3")
            {
                int yr = Convert.ToInt16(ddlYear.SelectedValue);
                yr++;
                ddlmode.Items.Add(new ListItem("Apr "+ ddlYear.SelectedValue + " - Jun " +  ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("July " + ddlYear.SelectedValue + " - Sep " + ddlYear.SelectedValue, "2", true));
                ddlmode.Items.Add(new ListItem("Oct " + ddlYear.SelectedValue + "- Dec " + ddlYear.SelectedValue, "3", true));
                ddlmode.Items.Add(new ListItem("Jan " +yr +" - March " + yr, "4", true));
            }

            else if (ViewState["target_yearbasedon"].ToString() == "5")
            {
                ddlmode.Items.Add(new ListItem("Jan " + ddlYear.SelectedValue + " - Jun " + ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("July " + ddlYear.SelectedValue + " - Dec " + ddlYear.SelectedValue, "2", true));
            }

            else if (ViewState["target_yearbasedon"].ToString() == "6")
            {               
                ddlmode.Items.Add(new ListItem("Jan " + ddlYear.SelectedValue +" - Mar " +ddlYear.SelectedValue, "1", true));
                ddlmode.Items.Add(new ListItem("Apr " + ddlYear.SelectedValue + " - June " + ddlYear.SelectedValue, "2", true));
                ddlmode.Items.Add(new ListItem("July " + ddlYear.SelectedValue + " - Sep " + ddlYear.SelectedValue, "3", true));
                ddlmode.Items.Add(new ListItem("Oct " + ddlYear.SelectedValue + "- Dec " + ddlYear.SelectedValue, "4", true));

            }
            else if (ViewState["target_yearbasedon"].ToString() == "7")
            {
                ddlmode.Items.Clear();
                ddlmode.Items.Add(new ListItem("Jan", "2", true));
                ddlmode.Items.Add(new ListItem("Feb", "3", true));
                ddlmode.Items.Add(new ListItem("Mar", "4", true));
                ddlmode.Items.Add(new ListItem("Apr", "5", true));
                ddlmode.Items.Add(new ListItem("May", "6", true));
                ddlmode.Items.Add(new ListItem("Jun", "7", true));
                ddlmode.Items.Add(new ListItem("Jul", "8", true));
                ddlmode.Items.Add(new ListItem("Aug", "9", true));
                ddlmode.Items.Add(new ListItem("Sep", "10", true));
                ddlmode.Items.Add(new ListItem("Oct", "11", true));
                ddlmode.Items.Add(new ListItem("Nov", "12", true));
                ddlmode.Items.Add(new ListItem("Dec", "13", true));
            }                                   
        }
    }
    
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlmode.Items.Clear();
        //ddlmode.Items.Insert(0, new ListItem("--Select--", "0"));
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

                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;

                    this.gvTarget.Columns[2].Visible = true;
                    this.gvTarget.Columns[3].Visible = true;
                    this.gvTarget.Columns[4].Visible = true;
                    this.gvTarget.Columns[5].Visible = true;
                    this.gvTarget.Columns[6].Visible = true;
                    this.gvTarget.Columns[7].Visible = true;
                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget.Columns[2].Visible = false;
                    this.gvTarget.Columns[3].Visible = false;
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;

                    this.gvTarget.Columns[8].Visible = true;
                    this.gvTarget.Columns[9].Visible = true;
                    this.gvTarget.Columns[10].Visible = true;
                    this.gvTarget.Columns[11].Visible = true;
                    this.gvTarget.Columns[12].Visible = true;
                    this.gvTarget.Columns[13].Visible = true;
                }
            }

            else if (ViewState["target_yearbasedon"].ToString() == "3")
            {

                if (ddlmode.SelectedValue == "1")
                {
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;

                    this.gvTarget.Columns[2].Visible = true;
                    this.gvTarget.Columns[3].Visible = true;
                    this.gvTarget.Columns[4].Visible = true;
                    

                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget.Columns[2].Visible = false;
                    this.gvTarget.Columns[3].Visible = false;
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;

                    this.gvTarget.Columns[5].Visible = true;
                    this.gvTarget.Columns[6].Visible = true;
                    this.gvTarget.Columns[7].Visible = true;
                    


                }

                else if (ddlmode.SelectedValue == "3")
                {
                    this.gvTarget.Columns[2].Visible = false;
                    this.gvTarget.Columns[3].Visible = false;
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[11].Visible = false;
                    this.gvTarget.Columns[12].Visible = false;
                    this.gvTarget.Columns[13].Visible = false;

                    this.gvTarget.Columns[8].Visible = true;
                    this.gvTarget.Columns[9].Visible = true;
                    this.gvTarget.Columns[10].Visible = true;
                   
                   
                }

                else if (ddlmode.SelectedValue == "4")
                {
                    this.gvTarget.Columns[2].Visible = false;
                    this.gvTarget.Columns[3].Visible = false;
                    this.gvTarget.Columns[4].Visible = false;
                    this.gvTarget.Columns[5].Visible = false;
                    this.gvTarget.Columns[6].Visible = false;
                    this.gvTarget.Columns[7].Visible = false;
                    this.gvTarget.Columns[8].Visible = false;
                    this.gvTarget.Columns[9].Visible = false;
                    this.gvTarget.Columns[10].Visible = false;

                    this.gvTarget.Columns[11].Visible = true;
                    this.gvTarget.Columns[12].Visible = true;
                    this.gvTarget.Columns[13].Visible = true;
                }
            }

            else if (ViewState["target_yearbasedon"].ToString() == "5")
            {

                if (ddlmode.SelectedValue == "1")
                {

                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;

                    this.gvTarget2.Columns[2].Visible = true;
                    this.gvTarget2.Columns[3].Visible = true;
                    this.gvTarget2.Columns[4].Visible = true;
                    this.gvTarget2.Columns[5].Visible = true;
                    this.gvTarget2.Columns[6].Visible = true;
                    this.gvTarget2.Columns[7].Visible = true;
                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget2.Columns[2].Visible = false;
                    this.gvTarget2.Columns[3].Visible = false;
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;

                    this.gvTarget2.Columns[8].Visible = true;
                    this.gvTarget2.Columns[9].Visible = true;
                    this.gvTarget2.Columns[10].Visible = true;
                    this.gvTarget2.Columns[11].Visible = true;
                    this.gvTarget2.Columns[12].Visible = true;
                    this.gvTarget2.Columns[13].Visible = true;
                }
            }

            else if (ViewState["target_yearbasedon"].ToString() == "6")
            {

                if (ddlmode.SelectedValue == "1")
                {
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;

                    this.gvTarget2.Columns[2].Visible = true;
                    this.gvTarget2.Columns[3].Visible = true;
                    this.gvTarget2.Columns[4].Visible = true;


                }
                else if (ddlmode.SelectedValue == "2")
                {
                    this.gvTarget2.Columns[2].Visible = false;
                    this.gvTarget2.Columns[3].Visible = false;
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;

                    this.gvTarget2.Columns[5].Visible = true;
                    this.gvTarget2.Columns[6].Visible = true;
                    this.gvTarget2.Columns[7].Visible = true;



                }

                else if (ddlmode.SelectedValue == "3")
                {
                    this.gvTarget2.Columns[2].Visible = false;
                    this.gvTarget2.Columns[3].Visible = false;
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[11].Visible = false;
                    this.gvTarget2.Columns[12].Visible = false;
                    this.gvTarget2.Columns[13].Visible = false;

                    this.gvTarget2.Columns[8].Visible = true;
                    this.gvTarget2.Columns[9].Visible = true;
                    this.gvTarget2.Columns[10].Visible = true;


                }

                else if (ddlmode.SelectedValue == "4")
                {
                    this.gvTarget2.Columns[2].Visible = false;
                    this.gvTarget2.Columns[3].Visible = false;
                    this.gvTarget2.Columns[4].Visible = false;
                    this.gvTarget2.Columns[5].Visible = false;
                    this.gvTarget2.Columns[6].Visible = false;
                    this.gvTarget2.Columns[7].Visible = false;
                    this.gvTarget2.Columns[8].Visible = false;
                    this.gvTarget2.Columns[9].Visible = false;
                    this.gvTarget2.Columns[10].Visible = false;

                    this.gvTarget2.Columns[11].Visible = true;
                    this.gvTarget2.Columns[12].Visible = true;
                    this.gvTarget2.Columns[13].Visible = true;
                }
            }

            else if (ViewState["target_yearbasedon"].ToString() == "7")
            {

                //  e.Row.Visible = false;

                for (int i = 2; i < gvTarget2.Columns.Count; i++)
                {
                    if (i == Convert.ToInt16(ddlmode.SelectedValue))
                    {
                        //this.gvTarget.Columns[0].Visible = true;
                        gvTarget2.Columns[i].Visible = true;
                    }
                    else
                    {
                        //this.gvTarget.Columns[0].Visible = true;
                        gvTarget2.Columns[i].Visible = false;
                    }

                }
            }
        }
    }
}
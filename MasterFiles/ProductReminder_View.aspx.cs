using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Net;
using Bus_EReport;


public partial class MasterFiles_ProductReminder_View : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    string div_code = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;   
    string[] statecd;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        grdGift.Visible = false;
        tblState.Visible = false;
        DataSet dsTP = null;
        Session["backurl"] = "ProductReminderList.aspx";
        div_code = Session["div_code"].ToString();      
          
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFromYear.Items.Add(k.ToString());
                    ddlToYear.Items.Add(k.ToString());
                    ddlFromYear.SelectedValue = DateTime.Now.Year.ToString();
                    ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            FillState(div_code);
            FillSubDiv(div_code);
            FillBrand(div_code);
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            ddlSubDivChk.Visible = false;
            ddlBrandChk.Visible = false;

            //ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            //sm.RegisterAsyncPostBackControl(btnProceed);
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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {   
        if (ddlMode.SelectedValue == "0")
        {
            lblState.Visible = true;
            ddlState.Visible = true;
            lblDiv.Visible = false;
            ddlDiv.Visible = false;
            lblBrand.Visible = false;
            ddlBrand.Visible = false;

            btnSubmit.Visible = true;
            lblChkState.Visible = false;
            ddlStateChk.Visible = false;
            lblChkDiv.Visible = false;
            ddlSubDivChk.Visible = false;
            lblChkBrand.Visible = false;
            ddlBrandChk.Visible = false;
            //btnProceed.Visible = false;
            btnClear.Visible = false;

            //FillState(div_code);        
        }
        else if (ddlMode.SelectedValue == "1")
        {
            lblState.Visible = false;
            ddlState.Visible = false;
            lblDiv.Visible = true;
            ddlDiv.Visible = true;
            lblBrand.Visible = false;
            ddlBrand.Visible = false;

            btnSubmit.Visible = true;
            lblChkState.Visible = false;
            ddlStateChk.Visible = false;
            lblChkDiv.Visible = false;
            ddlSubDivChk.Visible = false;
            lblChkBrand.Visible = false;
            ddlBrandChk.Visible = false;
            //btnProceed.Visible = false;
            btnClear.Visible = false;

            //FillSubDiv(div_code);           
        }
        else if (ddlMode.SelectedValue == "2")
        {
            lblState.Visible = false;
            ddlState.Visible = false;
            lblDiv.Visible = false;
            ddlDiv.Visible = false;
            lblBrand.Visible = true;
            ddlBrand.Visible = true;

            btnSubmit.Visible = true;
            lblChkState.Visible = false;
            ddlStateChk.Visible = false;
            lblChkDiv.Visible = false;
            ddlSubDivChk.Visible = false;
            lblChkBrand.Visible = false;
            ddlBrandChk.Visible = false;
            //btnProceed.Visible = false;
            btnClear.Visible = false;
            //FillBrand(div_code);
        }


        else if (ddlMode.SelectedValue == "3")
        {
            lblChkState.Visible = true;
            ddlStateChk.Visible = true;
            lblChkDiv.Visible = true;
            ddlSubDivChk.Visible = true;
            lblChkBrand.Visible = true;
            ddlBrandChk.Visible = true;
            //btnProceed.Visible = true;
            btnClear.Visible = true;


            lblState.Visible = false;
            lblDiv.Visible = false;
            lblBrand.Visible = false;
            ddlState.Visible = false;
            ddlDiv.Visible = false;
            ddlBrand.Visible = false;
            btnSubmit.Visible = true;

        }
    }

    protected void grdGift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlGiftType = (DropDownList)e.Row.FindControl("ddlGiftType");
            if (ddlGiftType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlGiftType.SelectedIndex = ddlGiftType.Items.IndexOf(ddlGiftType.Items.FindByText(row["Gift_Type"].ToString()));
            }

            //Label lblMode = (Label)e.Row.FindControl("lblMode");
            //lblMode.Text = lblMode.Text + "<span style='font-weight: bold;color:Red; font-size:14px'> (Deactivate) </span>";
            //lnkdeact.Visible = false;

            Label lblMode = (Label)e.Row.FindControl("lblMode");
            if (lblMode.Text == "Inactive")
            {
                lblMode.Text = "<span style='font-weight: bold;color:Red; font-size:13px'> Inactive  </span>";
            }
            else if (lblMode.Text == "Active")
            {
                lblMode.Text = "<span style='font-weight: bold;color:Green; font-size:13px'> Active  </span>";
            }

            else if (lblMode.Text == "Eff.ToDate Closed")
            {
                lblMode.Text = "<span style='font-weight: bold;color:Blue; font-size:13px'> Eff.ToDate Closed  </span>";
            }
        }
    }
   
    private void FillState(string div_code)
    {
        Input_New dv = new Input_New();
        dsDivision = dv.getStatePerDivision(div_code);
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
            DataSet DssState = null;
            Input_New st = new Input_New();
            dsState = DssState = st.getState(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();

            DssState.Tables[0].Rows.RemoveAt(0);
             
            chkState.DataTextField = "statename";
            chkState.DataValueField = "state_code";
            chkState.DataSource = DssState;
            chkState.DataBind();

            //PnlState.Attributes.Add("style", "background-color:Red;");
        }
    }


    private void FillSubDiv(string div_code)
    {
        Input_New dv = new Input_New();
        DataSet dsDiv = null;
        dsDivision = dv.getDivision_Input(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
           string sStates = string.Empty;
           string Div = string.Empty;
          
               for (int j = 0; j <= dsDivision.Tables[0].Rows.Count - 1; j++)
               {
                   sStates = dsDivision.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                   Div = Div+ sStates + ",";                   
               }

               statecd = Div.Split(',');
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
            DataSet DssDiv = null;
            dsDiv= DssDiv = dv.getDiv_Input(state_cd);
            ddlDiv.DataTextField = "subdivision_name";
            ddlDiv.DataValueField = "subdivision_code";
            ddlDiv.DataSource = dsDiv;
            ddlDiv.DataBind();
            
           
            DssDiv.Tables[0].Rows.RemoveAt(0);

            ChkDiv.DataTextField = "subdivision_name";
            ChkDiv.DataValueField = "subdivision_code";
            ChkDiv.DataSource = DssDiv;
            ChkDiv.DataBind();
            //pnlDiv.Attributes.Add("style", "background-color:Red;");
        }
    }
    private void FillBrand(string div_code)
    {
        Input_New dv = new Input_New();
        DataSet dsDiv = null;

        dsDivision = dv.getBrandiv_Input(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            string sStates = string.Empty;
            string Brand = string.Empty;
            for (int j = 0; j <= dsDivision.Tables[0].Rows.Count - 1; j++)
            {
                sStates = dsDivision.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                Brand =Brand+ sStates + ",";  
            }
            statecd = Brand.Split(',');
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
            DataSet DssBrand = null;
            dsDiv = DssBrand = dv.getBrand_Input(state_cd);
            ddlBrand.DataTextField = "Product_Brd_Name";
            ddlBrand.DataValueField = "Product_Brd_Code";
            ddlBrand.DataSource = dsDiv;
            ddlBrand.DataBind();

            DssBrand.Tables[0].Rows.RemoveAt(0);

            ChkBrand.DataTextField = "Product_Brd_Name";
            ChkBrand.DataValueField = "Product_Brd_Code";
            ChkBrand.DataSource = DssBrand;
            ChkBrand.DataBind();
            //pnlBrand.Attributes.Add("style", "background-color:Red;");
        }
    }

    protected void btnSubmit_Onclick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        ViewGift();
    }

    private void ViewGift()
    {
        ////string ddls = ddlStateChk.SelectedItem.ToString();
        string ddls = ddlStateChk.Text;
        if (ddlMode.SelectedValue == "0")
        {
            if (ddlState.SelectedValue == "0")
            {
                if ((ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
                {
                    Input_New prd = new Input_New();
                    dsProd = prd.ViewGiftAll(div_code, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
                    if (dsProd.Tables[0].Rows.Count > 0)
                    {
                        tblState.Visible = true;
                        lblSt.Text = "State Name: ";
                        lblStatename.Text = Convert.ToString(ddlState.SelectedItem.ToString());

                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = true;
                    }
                    else
                    {
                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = false;
                    }
                }
            }
            else
            {
                if ((ddlState.SelectedIndex > 0) && (ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
                {
                    Input_New prd = new Input_New();
                    dsProd = prd.ViewGift(div_code, Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
                    if (dsProd.Tables[0].Rows.Count > 0)
                    {
                        tblState.Visible = true;
                        lblSt.Text = "State Name: ";
                        lblStatename.Text = Convert.ToString(ddlState.SelectedItem.ToString());

                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = true;
                    }
                    else
                    {
                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = false;
                    }
                }
            }          
        }

        else if (ddlMode.SelectedValue == "1")
        {

            if (ddlDiv.SelectedValue == "0")
            {
                if ((ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
                {
                    Input_New prd = new Input_New();
                    dsProd = prd.ViewGiftAll(div_code, Convert.ToInt32(ddlDiv.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
                    if (dsProd.Tables[0].Rows.Count > 0)
                    {
                        tblState.Visible = true;
                        lblSt.Text = "Sub Division Name: ";
                        lblStatename.Text = Convert.ToString(ddlDiv.SelectedItem.ToString());

                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = true;
                    }
                    else
                    {
                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = false;
                    }
                }
            }
            else
            {
                if ((ddlDiv.SelectedIndex > 0) && (ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
                {
                    Input_New prd = new Input_New();
                    dsProd = prd.ViewGiftDiv_Input(div_code, Convert.ToInt32(ddlDiv.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
                    if (dsProd.Tables[0].Rows.Count > 0) 
                    {
                        tblState.Visible = true;
                        lblSt.Text = "Sub Division Name: ";
                        lblStatename.Text = Convert.ToString(ddlDiv.SelectedItem.ToString());

                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = true;
                    }
                    else
                    {
                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = false;
                    }
                }
            }            
        }

        else if (ddlMode.SelectedValue == "2")
        {

            if (ddlBrand.SelectedValue == "0")
            {
                if ((ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
                {
                    Input_New prd = new Input_New();
                    dsProd = prd.ViewGiftAll(div_code, Convert.ToInt32(ddlBrand.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
                    if (dsProd.Tables[0].Rows.Count > 0)
                    {
                        tblState.Visible = true;
                        lblSt.Text = "Brand Name: ";
                        lblStatename.Text = Convert.ToString(ddlBrand.SelectedItem.ToString());

                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = true;
                    }
                    else
                    {
                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = false;
                    }
                }
            }
            else
            {
                if ((ddlBrand.SelectedIndex > 0) && (ddlFromYear.SelectedIndex > 0) && (ddlToYear.SelectedIndex > 0))
                {
                    Input_New prd = new Input_New();
                    dsProd = prd.ViewGiftBrand_Input(div_code, Convert.ToInt32(ddlBrand.SelectedValue), Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
                    if (dsProd.Tables[0].Rows.Count > 0) 
                    {
                        tblState.Visible = true;
                        lblSt.Text = "Brand Name: ";
                        lblStatename.Text = Convert.ToString(ddlBrand.SelectedItem.ToString());

                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = true;
                    }
                    else
                    {
                        grdGift.Visible = true;
                        grdGift.DataSource = dsProd;
                        grdGift.DataBind();
                        btnExcel.Visible = false;
                    }
                }
            }            
        }


        else if (ddlMode.SelectedValue == "3")
        {
            Input_New prd = new Input_New();
            string k = "";
            string L = "";
            string M = "";

            ////string ddl = ddlStateChk.SelectedItem.ToString();
            string ddl = ddlStateChk.Text;

            for (int i = 0; i < chkState.Items.Count; i++)
            {
                if (chkState.Items[i].Selected)
                {
                    k = k + chkState.Items[i].Value + ",";
                }

            }
            string[] K1 = k.Split(',');
            DataSet dssk = null;
            DataSet dsskk = null;
            //foreach (string item in K1)
            //{
            //    //dssk = prd.ViewChkBox_All(div_code, item, L, M, Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
            //    //dsskk.Tables[0].Rows.Add(dssk);
            //}

            for (int i = 0; i < ChkDiv.Items.Count; i++)
            {
                if (ChkDiv.Items[i].Selected)
                {
                    L = L + ChkDiv.Items[i].Value + ",";
                }
            }
            string[] L1 = L.Split(',');

            for (int i = 0; i < ChkBrand.Items.Count; i++)
            {
                if (ChkBrand.Items[i].Selected)
                {
                    M = M + ChkBrand.Items[i].Value + ",";
                }
            }
            string[] M1 = M.Split(',');

            dsProd = prd.ViewChkBox_All(div_code, k, L, M, Convert.ToInt32(ddlFromYear.SelectedValue), Convert.ToInt32(ddlToYear.SelectedValue));
            if (dsProd.Tables[0].Rows.Count > 0)
            {
                btnExcel.Visible = true;
                grdGift.Visible = true;
                grdGift.DataSource = dsProd;
                grdGift.DataBind();
            }
            else
            {
                btnExcel.Visible = false;
                grdGift.Visible = true;
                grdGift.DataSource = dsProd;
                grdGift.DataBind();
            }         

        }
    }

    //protected void btnProceed_Onclick(object sender, EventArgs e)
    //{
       
    //}


    protected void btnClear_Onclick(object sender, EventArgs e)
    {
        FillState(div_code);
        FillSubDiv(div_code);
        FillBrand(div_code);

        //btnSubmit.Visible = false;
    }

    protected void ChkBrand_Changed(object sender, EventArgs e)
    {

    }
    protected void grdGift_RowEditing(object sender, GridViewEditEventArgs e)
    {
        e.Cancel = true;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //string strFileName = Page.Title;
        //string attachment = "attachment; filename=" + lblSt.Text +" : "+ lblStatename .Text+ ".xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //HtmlForm frm = new HtmlForm();
        //pnlContents.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(pnlContents);
        //frm.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductReminderList.aspx");
    }

   

   
}
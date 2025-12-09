using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProductReminder : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsgift = null;
    DataSet dsState = null;
    DataSet dsSBrand = null;
    DataSet dsSubDivision = null;
    string subdivision_code = string.Empty;
    string giftcode = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string state_code = string.Empty;
    string Brand_code = string.Empty;
    string State_Code = string.Empty;
    string sChkLocation = string.Empty;
    string sChkLocation1 = string.Empty;
    int iIndex;
    string[] statecd;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string division_code = string.Empty;
    DataSet dsdiv = new DataSet();
    DataSet dsSub = new DataSet();
    DataSet dsSt = new DataSet();
    string sChkdiv = string.Empty;
    string sChkbrand = string.Empty;
    int iReturn = -1;
    string[] chkNewdiv;
    string stAll = string.Empty;
    string subAll = string.Empty;
    string stBrand = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductReminderList.aspx";
        div_code = Session["div_code"].ToString();
        division_code = Session["division_code"].ToString();
        giftcode = Request.QueryString["Gift_Code"];
        txtGift_SName.Focus();
        if (!Page.IsPostBack)
        {
            FillCheckBoxList();
            FillCheckBoxList_New();
            FillDiv();
            FillCheckBoxBrand_New();
            for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
            {
                if (div_code == chkDivision.Items[iIndex].Value)
                {
                    chkDivision.Items[iIndex].Selected = true;
                    chkDivision.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");
                }
            }
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (giftcode != "" && giftcode != null)
            {
                Input_New dv = new Input_New();
                dsgift = dv.getGift(div_code, giftcode);
                if (dsgift.Tables[0].Rows.Count > 0)
                {
                    txtGift_SName.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtGiftName.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtGiftValue.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    ddlGiftType.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtEffFrom.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtEffTo.Text = dsgift.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    state_code = dsgift.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    subdivision_code = dsgift.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    Brand_code = dsgift.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();

                    Submit.Text = "Update";
                }
                FillCheckBoxList();
                FillCheckBoxList_New();
                FillCheckBoxBrand_New();
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

    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class
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

            dsState = dv.getSt(state_cd);
            chkboxLocation.DataTextField = "statename";
            chkboxLocation.DataValueField = "state_code";
            chkboxLocation.DataSource = dsState;
            chkboxLocation.DataBind();
        }
        string[] state;
        if (state_code != "")
        {
            iIndex = -1;
            state = state_code.Split(',');
            foreach (string st in state)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");
                    }
                }
            }
        }
    }

    private void FillCheckBoxList_New()
    {
        //List of Sub division are loaded into the checkbox list from Division Class

        Input_New dv = new Input_New();
        dsSubDivision = dv.getSubDiv(div_code);
        chkSubdiv.DataTextField = "subdivision_name";
        chkSubdiv.DataSource = dsSubDivision;
        chkSubdiv.DataBind();
        string[] subdiv;

        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
                {
                    if (st == chkSubdiv.Items[iIndex].Value)
                    {
                        chkSubdiv.Items[iIndex].Selected = true;
                        chkSubdiv.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold");
                        //  chkNil.Checked = false;
                    }
                }
            }
        }
    }

    private void FillCheckBoxBrand_New()
    {
        Input_New dv = new Input_New();
        DataSet dsbrand = null;
        dsbrand = dv.getSubBrand(div_code);
        if (dsbrand.Tables[0].Rows.Count > 0)
        {
            chkBrand.DataTextField = "Product_Brd_Name";
            chkBrand.DataValueField = "Product_Brd_Code";
            chkBrand.DataSource = dsbrand;
            chkBrand.DataBind();
        }

        string[] Brand;
        if (Brand_code != "")
        {
            iIndex = -1;
            Brand = Brand_code.Split(',');
            foreach (string st in Brand)
            {
                for (iIndex = 0; iIndex < chkBrand.Items.Count; iIndex++)
                {
                    if (st == chkBrand.Items[iIndex].Value)
                    {
                        chkBrand.Items[iIndex].Selected = true;
                        chkBrand.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");
                    }
                }
            }
        }
    }

    private void FillDiv()
    {      
        division_code = division_code.Substring(0, division_code.Length - 1);

        Input_New dv = new Input_New();
        dsdiv = dv.getDivEdit(division_code, "");
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            chkDivision.DataTextField = "Division_Name";
            chkDivision.DataValueField = "Division_Code";
            chkDivision.DataSource = dsdiv;
            chkDivision.DataBind();
        }
        //    }
        //}
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        //if (Convert.ToDateTime(txtEffTo.Text) >= System.DateTime.Now.Date)
        {
            System.Threading.Thread.Sleep(time);
            for (int i = 0; i < chkboxLocation.Items.Count; i++)
            {
                if (chkboxLocation.Items[i].Selected)
                {
                    sChkLocation = sChkLocation + chkboxLocation.Items[i].Value + ",";
                }
            }
            for (int i = 0; i < chkSubdiv.Items.Count; i++)
            {
                if (chkSubdiv.Items[i].Selected)
                {
                    sChkLocation1 = sChkLocation1 + chkSubdiv.Items[i].Value + ",";
                }
            }
            for (int i = 0; i < chkDivision.Items.Count; i++)
            {
                if (chkDivision.Items[i].Selected)
                {
                    sChkdiv = sChkdiv + chkDivision.Items[i].Value + ",";
                }
            }
            for (int i = 0; i < chkBrand.Items.Count; i++)
            {
                if (chkBrand.Items[i].Selected)
                {
                    sChkbrand = sChkbrand + chkBrand.Items[i].Value + ",";
                }
            }

            Input_New prd = new Input_New();
           int ReturnId = -1;
            string[] strDivSplit = sChkdiv.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (ReturnId != -2 && ReturnId != -3)
                {
                    if (strdiv != "")
                    {
                        // string dsGift = prd.getGiftCode(strdiv, txtGiftName.Text);
                      string  dsGift = Request.QueryString["Gift_Code"];
                        if (div_code == strdiv)
                        {
                            if (dsGift == null)
                            {
                                iReturn = prd.RecordAddGift(txtGift_SName.Text, txtGiftName.Text, Convert.ToInt32(ddlGiftType.SelectedValue), txtGiftValue.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1, sChkbrand);
                                ReturnId = iReturn;
                            }
                            else
                            {
                                iReturn = prd.RecordUpdateGift(dsGift, txtGift_SName.Text, txtGiftName.Text, Convert.ToInt32(ddlGiftType.SelectedValue), txtGiftValue.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1, sChkbrand);                          
                            }
                        }
                        else
                        {
                            Input_New dv = new Input_New();
                            dsState = dv.getStatePerDivision(strdiv);
                            if (dsState.Tables[0].Rows.Count > 0)
                            {
                                stAll = dsState.Tables[0].Rows[0]["state_code"].ToString();
                            }
                            string dsSubs = dv.getSubPerDivision(strdiv);
                            string dsSBrandss = dv.getBrandPerDivision(strdiv);

                            if (stAll != "" && dsSubs != "" && dsSBrandss != "")
                            {
                                if (dsGift == null)
                                {
                                    iReturn = prd.RecordAddGift(txtGift_SName.Text, txtGiftName.Text, Convert.ToInt32(ddlGiftType.SelectedValue), txtGiftValue.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), Convert.ToInt32(strdiv), stAll, dsSubs, dsSBrandss);
                                }
                                else
                                {
                                    iReturn = dv.RecordUpdateGift(dsGift, txtGift_SName.Text, txtGiftName.Text, Convert.ToInt32(ddlGiftType.SelectedValue), txtGiftValue.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), Convert.ToInt32(strdiv), stAll, dsSubs, dsSBrandss);
                                }
                            }
                        }
                    }                 
                }
            }
            if (iReturn > 0)
            {
                // menu1.Status = "Gift details Created Successfully";
                if (Submit.Text == "Save")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert(' Created Successfully');</script>");
                }
                else if (Submit.Text == "Update")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Updated Successfully');</script>");
                }
                ResetAll();
            }
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Gift Name Already exist');</script>");
                txtGiftName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Gift Short Name Already exist');</script>");
                txtGift_SName.Focus();
            }
        }
        //else
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert(' select Valid Effective To Date ');</script>");
        //    //txtEffTo.Attributes.Add("style", "Color: Red;font-weight:Bold");
        //}
    }

    private void ResetAll()
    {
        txtGift_SName.Text = "";
        txtGiftName.Text = "";
        txtGiftValue.Text = "";
        ddlGiftType.SelectedIndex = 0;
        txtEffFrom.Text = "";
        txtEffTo.Text = "";
        CheckBrand.Checked = false;
        CheckState.Checked = false;
        for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        {
            chkboxLocation.Items[iIndex].Selected = false;
        }
        //   chkNil.Checked = true;
        for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
        {
            chkSubdiv.Items[iIndex].Selected = false;
        }
        for (iIndex = 0; iIndex < chkBrand.Items.Count; iIndex++)
        {
            chkBrand.Items[iIndex].Selected = false;
        }

        for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
        {
            if (div_code == chkDivision.Items[iIndex].Value)
            {
                chkDivision.Items[iIndex].Selected = true;
                chkDivision.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");
            }
            else
            {
                chkDivision.Items[iIndex].Selected = false;
            }
        }
    }
    //protected void chkNil_CheckedChanged(object sender, EventArgs e)
    //{
    //    chkSubdiv.Attributes.Add("onclick", "checkNIL(this);");
    //} 
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductReminderList.aspx");
    }

}
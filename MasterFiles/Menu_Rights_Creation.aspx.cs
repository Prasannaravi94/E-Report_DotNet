using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Menu_Rights_Creation : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsSales = null;
    DataSet dsHODivision = null;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iIndex = -1;
    int iLength = -1;
    string divcode = string.Empty;
    string HO_ID = string.Empty;
    string Ho_Id = string.Empty;
    string HO_div_code = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string sChkLocation = string.Empty;
    string sChkReports = string.Empty;
    string schkOptions = string.Empty;
    string sChkFinal = string.Empty;
    string sChkmas = string.Empty;
    string sChkAct = string.Empty;
    string sChkActR = string.Empty;
    string sChkMIS = string.Empty;
    string sChkOpt = string.Empty;
    string Menu_Name = string.Empty;
    DataSet dsSub = new DataSet();
    string sub_id = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        // divcode = Convert.ToString(Session["div_code"]);
      //  Session["backurl"] = "Menu_Rights_View.aspx";
        sf_type = Session["sf_type"].ToString();
        Ho_Id = Session["HO_ID"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        HO_ID = Request.QueryString["HO_ID"];
      //  txtName.Focus();
        //div_code = Request.QueryString["division_code"];

        if (!Page.IsPostBack)
        {
            
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            ////menu1.FindControl("btnBack").Visible = false;
            if ((HO_ID != "") && (HO_ID != null))
            {
                menu1.Title = this.Page.Title;
                //Session["backurl"] = "Sub_HO_ID_View.aspx";

                SalesForce sale = new SalesForce();
                dsSales = sale.get_Sub_Ho_Id(HO_ID);

                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    txtName.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtUserName.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtPassword.Text = dsSales.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                    string strPassword;
                    strPassword = txtPassword.Text;
                    txtPassword.Attributes.Add("value", strPassword);

                    division_code = dsSales.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }
                
                //if (sf_type == "")
                //{
                //    FillCheckBoxList(HO_ID);
                //}
                if (sf_type == "3")
                {
                    FillCheckBoxList(HO_ID);
                }
                UserLogin dv = new UserLogin();
                dsSub = dv.Get_Sub_Id(HO_ID);
                if (dsSub.Tables[0].Rows.Count > 0)
                {
                    sub_id = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                if (sub_id != "0")
                {
                    //ddlmenu.Items.Remove("MR");
                    //ddlmenu.Items.Remove("1");
                    ddlmenu.Items.Remove(ddlmenu.Items.FindByText("MR"));
                    ddlmenu.Items.Remove(ddlmenu.Items.FindByText("MGR"));

                }
                else
                {

                }
            }
            else
            {
                FillDiv();
            }
        }
       
    }
    private void fillRights()
    {
        if (ddlmenu.SelectedValue == "0")
        {
            Fill_Rights(HO_ID);
        }
        else if (ddlmenu.SelectedValue == "1")
        {
            Fill_MGR_Rights(HO_ID);
        }
        else
        {
            Fill_MR_Rights(HO_ID);
        }
    }
   
    private void FillCheckBoxList(string HO_ID)
    {
        Division dv = new Division();
        dsHODivision = dv.getSubHODiv(Ho_Id, div_code);
        chkDivision.DataTextField = "Division_Name";
        chkDivision.DataSource = dsHODivision;
        chkDivision.DataBind();
        string[] div;
        if (division_code != "")
        {
            iIndex = -1;
            div = division_code.Split(',');
            foreach (string code in div)
            {
                for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
                {
                    if (code == chkDivision.Items[iIndex].Value)
                    {

                        chkDivision.Items[iIndex].Selected = true;
                        chkDivision.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
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

    private void FillDiv()
    {

        Division dv = new Division();
        dsHODivision = dv.getSubHODivision(Ho_Id, div_code);
        chkDivision.DataTextField = "Division_Name";
        chkDivision.DataSource = dsHODivision;
        chkDivision.DataBind();
        //string[] div;
        //if (div_code != "")
        //{
        //    iIndex = -1;
        //    div = div_code.Split(',');
        //    foreach (string code in div)
        //    {
        //        for (iIndex = 0; iIndex < chkDivision.Items.Count; iIndex++)
        //        {
        //            if (code == chkDivision.Items[iIndex].Value)
        //            {

        //                chkDivision.Items[iIndex].Selected = true;
        //                chkDivision.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

        //            }

        //        }
        //    }
        //}
    }
        

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string div_code = string.Empty;
        int i;

        for (i = 0; i < chkDivision.Items.Count; i++)
        {
            if (chkDivision.Items[i].Selected)
                div_code = div_code + chkDivision.Items[i].Value.ToString() + ",";
        }


        if (HO_ID == null)
        {
            SalesForce sf = new SalesForce();
            int iReturn = sf.Sub_HO_ID_RecordAdd(txtUserName.Text.Trim(), txtPassword.Text.Trim(), txtName.Text.Trim(),"", div_code,"","", Ho_Id);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('HO ID created successfully');</script>");
                Reset_Controls();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Name Already Exist');</script>");
                txtUserName.Focus();
            }
        }

        else
        {
            SalesForce sf = new SalesForce();
            int HO_Id = Convert.ToInt16(HO_ID);
            int iReturn = sf.Update_Sub_HO_Id(HO_Id, txtName.Text, txtUserName.Text, txtPassword.Text, div_code);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Name Already Exist');</script>");
                txtUserName.Focus();
            }
        }
    }

    private void Reset_Controls()
    {
        txtName.Text = "";
        txtPassword.Text = "";
        txtUserName.Text = "";

        for (int i = 0; i < chkDivision.Items.Count; i++)
        {
            chkDivision.Items[i].Selected = false;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Reset_Controls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkaccess.Items.Count; i++)
        {
            if (chkaccess.Items[i].Selected)
            {
                sChkLocation = sChkLocation + chkaccess.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < chkReports.Items.Count; i++)
        {
            if (chkReports.Items[i].Selected)
            {
                sChkReports = sChkReports + chkReports.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkOptions.Items.Count; i++)
        {
            if (ChkOptions.Items[i].Selected)
            {
                schkOptions = schkOptions + ChkOptions.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < chkmasters.Items.Count; i++)
        {
            if (chkmasters.Items[i].Selected)
            {
                sChkmas = sChkmas + chkmasters.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkActive.Items.Count; i++)
        {
            if (ChkActive.Items[i].Selected)
            {
                sChkAct = sChkAct + ChkActive.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkAcReports.Items.Count; i++)
        {
            if (ChkAcReports.Items[i].Selected)
            {
                sChkActR = sChkActR + ChkAcReports.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkMIS.Items.Count; i++)
        {
            if (ChkMIS.Items[i].Selected)
            {
                sChkMIS = sChkMIS + ChkMIS.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkOpt.Items.Count; i++)
        {
            if (ChkOpt.Items[i].Selected)
            {
                sChkOpt = sChkOpt + ChkOpt.Items[i].Value + ",";
            }
        }
        UserLogin ul = new UserLogin();
        sChkFinal = sChkLocation + sChkReports + schkOptions + sChkmas + sChkAct + sChkActR + sChkMIS + sChkOpt;
        //if (sChkFinal != "")
        //{
            int iReturn = ul.Master_Menu_ADD(sChkFinal, HO_ID);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added Successfully');</script>");
            }
        //}
    }
    private void Fill_Rights(string HO_ID)
    {
        UserLogin dv = new UserLogin();
        dsHODivision = dv.Master_Menu_View(HO_ID);
        chkaccess.DataTextField = "Menu_Name";
        chkaccess.DataValueField = "Menu_Name";
        
        //chkaccess.DataSource = dsHODivision;
        //chkaccess.DataBind();
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            Menu_Name = dsHODivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        string[] div;
        if (Menu_Name != "")
        {
            iIndex = -1;
            div = Menu_Name.Split(',');
            foreach (string code in div)
            {
                for (iIndex = 0; iIndex < chkaccess.Items.Count; iIndex++)
                {
                    if (code == chkaccess.Items[iIndex].Value)
                    {

                        chkaccess.Items[iIndex].Selected = true;
                        chkaccess.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < chkReports.Items.Count; iIndex++)
                {
                    if (code == chkReports.Items[iIndex].Value)
                    {

                        chkReports.Items[iIndex].Selected = true;
                        chkReports.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkOptions.Items.Count; iIndex++)
                {
                    if (code == ChkOptions.Items[iIndex].Value)
                    {

                        ChkOptions.Items[iIndex].Selected = true;
                        ChkOptions.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < chkmasters.Items.Count; iIndex++)
                {
                    if (code == chkmasters.Items[iIndex].Value)
                    {

                        chkmasters.Items[iIndex].Selected = true;
                        chkmasters.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkActive.Items.Count; iIndex++)
                {
                    if (code == ChkActive.Items[iIndex].Value)
                    {

                        ChkActive.Items[iIndex].Selected = true;
                        ChkActive.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkAcReports.Items.Count; iIndex++)
                {
                    if (code == ChkAcReports.Items[iIndex].Value)
                    {

                        ChkAcReports.Items[iIndex].Selected = true;
                        ChkAcReports.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkMIS.Items.Count; iIndex++)
                {
                    if (code == ChkMIS.Items[iIndex].Value)
                    {

                        ChkMIS.Items[iIndex].Selected = true;
                        ChkMIS.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                } for (iIndex = 0; iIndex < ChkOpt.Items.Count; iIndex++)
                {
                    if (code == ChkOpt.Items[iIndex].Value)
                    {

                        ChkOpt.Items[iIndex].Selected = true;
                        ChkOpt.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
            }
        }
    }
    private void Fill_MGR_Rights(string HO_ID)
    {
        UserLogin dv = new UserLogin();
        dsHODivision = dv.MGR_Menu_View(HO_ID, div_code);
        //chkaccess.DataTextField = "MGR_Menu_Name";
        //chkaccess.DataValueField = "MGR_Menu_Name";

        //chkaccess.DataSource = dsHODivision;
        //chkaccess.DataBind();
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            Menu_Name = dsHODivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        string[] div;
        if (Menu_Name != "")
        {
            iIndex = -1;
            div = Menu_Name.Split(',');
            foreach (string code in div)
            {
                for (iIndex = 0; iIndex < chkInf.Items.Count; iIndex++)
                {
                    if (code == chkInf.Items[iIndex].Value)
                    {

                        chkInf.Items[iIndex].Selected = true;
                        chkInf.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < chkmgrAct.Items.Count; iIndex++)
                {
                    if (code == chkmgrAct.Items[iIndex].Value)
                    {

                        chkmgrAct.Items[iIndex].Selected = true;
                        chkmgrAct.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkMgrMis.Items.Count; iIndex++)
                {
                    if (code == ChkMgrMis.Items[iIndex].Value)
                    {

                        ChkMgrMis.Items[iIndex].Selected = true;
                        ChkMgrMis.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkMgrOpt.Items.Count; iIndex++)
                {
                    if (code == ChkMgrOpt.Items[iIndex].Value)
                    {

                        ChkMgrOpt.Items[iIndex].Selected = true;
                        ChkMgrOpt.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
              
            }
        }
    }
    private void Fill_MR_Rights(string HO_ID)
    {
        UserLogin dv = new UserLogin();
        dsHODivision = dv.MR_Menu_View(HO_ID, div_code);
        //chkaccess.DataTextField = "MGR_Menu_Name";
        //chkaccess.DataValueField = "MGR_Menu_Name";

        //chkaccess.DataSource = dsHODivision;
        //chkaccess.DataBind();
        if (dsHODivision.Tables[0].Rows.Count > 0)
        {
            Menu_Name = dsHODivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        string[] div;
        if (Menu_Name != "")
        {
            iIndex = -1;
            div = Menu_Name.Split(',');
            foreach (string code in div)
            {
                for (iIndex = 0; iIndex < chkMrInf.Items.Count; iIndex++)
                {
                    if (code == chkMrInf.Items[iIndex].Value)
                    {

                        chkMrInf.Items[iIndex].Selected = true;
                        chkMrInf.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkMrAct.Items.Count; iIndex++)
                {
                    if (code == ChkMrAct.Items[iIndex].Value)
                    {

                        ChkMrAct.Items[iIndex].Selected = true;
                        ChkMrAct.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkMrMis.Items.Count; iIndex++)
                {
                    if (code == ChkMrMis.Items[iIndex].Value)
                    {

                        ChkMrMis.Items[iIndex].Selected = true;
                        ChkMrMis.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }
                for (iIndex = 0; iIndex < ChkMrOpt.Items.Count; iIndex++)
                {
                    if (code == ChkMrOpt.Items[iIndex].Value)
                    {

                        ChkMrOpt.Items[iIndex].Selected = true;
                        ChkMrOpt.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }

                }

            }
        }
    }
    protected void ddlmenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillRights();
        if (ddlmenu.SelectedValue == "0")
        {
            pnladmin.Visible = true;
            pnlmgr.Visible = false;
            pnlMR.Visible = false;
            btnSave.Visible = true;
            btnMgr.Visible = false;
            btnMR.Visible = false;
        }
        else if (ddlmenu.SelectedValue == "1")
        {
            pnladmin.Visible = false;
            pnlmgr.Visible = true;
            pnlMR.Visible = false;
            btnSave.Visible = false;
            btnMR.Visible = false;
            btnMgr.Visible = true;
        }
        else
        {
            pnladmin.Visible = false;
            pnlmgr.Visible = false;
            pnlMR.Visible = true;
            btnSave.Visible = false;
            btnMR.Visible = true;
            btnMgr.Visible = false;
        }
    }
    protected void btnMgr_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkInf.Items.Count; i++)
        {
            if (chkInf.Items[i].Selected)
            {
                sChkLocation = sChkLocation + chkInf.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < chkmgrAct.Items.Count; i++)
        {
            if (chkmgrAct.Items[i].Selected)
            {
                sChkReports = sChkReports + chkmgrAct.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkMgrMis.Items.Count; i++)
        {
            if (ChkMgrMis.Items[i].Selected)
            {
                schkOptions = schkOptions + ChkMgrMis.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkMgrOpt.Items.Count; i++)
        {
            if (ChkMgrOpt.Items[i].Selected)
            {
                sChkmas = sChkmas + ChkMgrOpt.Items[i].Value + ",";
            }
        }

        UserLogin ul = new UserLogin();
        sChkFinal = sChkLocation + sChkReports + schkOptions + sChkmas;
        //if (sChkFinal != "")
        //{
            int iReturn = ul.MGR_Menu_ADD(sChkFinal, HO_ID);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added Successfully');</script>");
           // }
        }
    }
    protected void btnMR_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkMrInf.Items.Count; i++)
        {
            if (chkMrInf.Items[i].Selected)
            {
                sChkLocation = sChkLocation + chkMrInf.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkMrAct.Items.Count; i++)
        {
            if (ChkMrAct.Items[i].Selected)
            {
                sChkReports = sChkReports + ChkMrAct.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkMrMis.Items.Count; i++)
        {
            if (ChkMrMis.Items[i].Selected)
            {
                schkOptions = schkOptions + ChkMrMis.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < ChkMrOpt.Items.Count; i++)
        {
            if (ChkMrOpt.Items[i].Selected)
            {
                sChkmas = sChkmas + ChkMrOpt.Items[i].Value + ",";
            }
        }

        UserLogin ul = new UserLogin();
        sChkFinal = sChkLocation + sChkReports + schkOptions + sChkmas;
        //if (sChkFinal != "")
        //{
            int iReturn = ul.MR_Menu_ADD(sChkFinal, HO_ID);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Added Successfully');</script>");
            }
        //}
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("Menu_Rights_View.aspx");
    }
}
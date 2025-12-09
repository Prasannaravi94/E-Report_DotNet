using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_DoctorCampaign : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsDoc2 = null;
    string DocSubCatCode = string.Empty;
    string divcode = string.Empty;
    string Doc_SubCat_SName = string.Empty;
    string DocSubCatName = string.Empty;
    string EffFrom = string.Empty;
    string EffTo = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int All_DrsTagg = 0;
    int iIndex = -1;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string[] statecd;
    string strinput_value = string.Empty;
    string strinput_name = string.Empty;
    string strstate_value = string.Empty;
    string strstate_name = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "DoctorCampaignList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        DocSubCatCode = Request.QueryString["Doc_SubCatCode"];
        heading.InnerText = this.Page.Title;

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            fillInput();
            fillState();
            if (DocSubCatCode != "" && DocSubCatCode != null)
            {
                Doctor dv = new Doctor();
                dsDoc = dv.getDocSubCat(divcode, DocSubCatCode);

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    txtDoc_SubCat_SName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDocSubCatName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtEffFrom.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtEffTo.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtDr_tagg.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    ddlfor.SelectedValue = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                    if (dsDoc.Tables[0].Rows[0].ItemArray.GetValue(6).ToString() == "1")
                    {
                        chkall_drs.Checked = true;
                    }
                    else
                    {
                        chkall_drs.Checked = false;
                    }

                    txtvisit.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();

                    string input = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();

                    string strtxtWeeKName = string.Empty;
                    string[] strweek2;
                    iIndex = -1;
                    strweek2 = input.Split(',');
                    //  Session["Value"] = str.Remove(str.Length - 1);
                    Session["Value"] = input;
                    foreach (string Wk2 in strweek2)
                    {
                        for (iIndex = 0; iIndex < Chkinput.Items.Count; iIndex++)
                        {
                            if (Wk2 == Chkinput.Items[iIndex].Value)
                            {
                                Chkinput.Text = "";
                                Chkinput.Items[iIndex].Selected = true;

                                if (Chkinput.Items[iIndex].Selected == true)
                                {
                                    strtxtWeeKName += Chkinput.Items[iIndex].Text + ",";

                                }
                            }
                        }
                    }

                    if (strtxtWeeKName != "")
                    {
                        txtinput.Text = strtxtWeeKName.Remove(strtxtWeeKName.Length - 1);
                    }

                    txtRs.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();

                    string state = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();

                    string strtxtWeeKName2 = string.Empty;
                    string[] strstate;
                    iIndex = -1;
                    strstate = state.Split(',');
                    //  Session["Value"] = str.Remove(str.Length - 1);
                    Session["Value"] = state;
                    foreach (string Wk3 in strstate)
                    {
                        for (iIndex = 0; iIndex < chkstate.Items.Count; iIndex++)
                        {
                            if (Wk3 == chkstate.Items[iIndex].Value)
                            {
                                chkstate.Text = "";
                                chkstate.Items[iIndex].Selected = true;

                                if (chkstate.Items[iIndex].Selected == true)
                                {
                                    strtxtWeeKName2 += chkstate.Items[iIndex].Text + ",";

                                }
                            }
                        }
                    }

                    if (strtxtWeeKName2 != "")
                    {
                        txtstate.Text = strtxtWeeKName2.Remove(strtxtWeeKName2.Length - 1);
                    }

                    txtsms.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

                    Doctor dvv = new Doctor();
                    dsDoc2 = dvv.getDocSubCat_Campfor(divcode, DocSubCatCode, dsDoc.Tables[0].Rows[0]["Camp_for"].ToString());

                    if (dsDoc2.Tables[0].Rows.Count > 0)
                    {
                        string str = dsDoc2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        FillType_For();

                        string Camp_for_Code2 = string.Empty;
                        string[] strweek;
                        iIndex = -1;
                        strweek = str.Split(',');
                        //  Session["Value"] = str.Remove(str.Length - 1);
                        Session["Value"] = str;
                        foreach (string Wk in strweek)
                        {
                            for (iIndex = 0; iIndex < chk_for.Items.Count; iIndex++)
                            {
                                if (Wk == chk_for.Items[iIndex].Value)
                                {
                                    chk_for.Text = "";
                                    chk_for.Items[iIndex].Selected = true;

                                    if (chk_for.Items[iIndex].Selected == true)
                                    {
                                        Camp_for_Code2 += chk_for.Items[iIndex].Text + ",";
                                        chk_for.Items[iIndex].Attributes.Add("style", "Color: magenta;font-weight:Bold");

                                    }
                                }
                            }
                        }
                    }

                }

            }
            txtDoc_SubCat_SName.Focus();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Doc_SubCat_SName = txtDoc_SubCat_SName.Text;
        DocSubCatName = txtDocSubCatName.Text;
        //EffFrom = Convert.ToDateTime(txtEffFrom.Text);
        //EffTo = Convert.ToDateTime(txtEffTo.Text);
        if (DocSubCatCode == null)
        {
            if (Session["inputValue"] != null)
            {

                strinput_value = Session["inputValue"].ToString();
                strinput_name = Session["inputname"].ToString();
            }

            if (Session["stateValue"] != null)
            {
                strstate_value = Session["stateValue"].ToString();
                strstate_name = Session["statename"].ToString();
            }



            if (chkall_drs.Checked == true)
            {
                All_DrsTagg = 1;
            }
            else
            {
                All_DrsTagg = 0;
            }
            string Camp_for_Code = "";
            string Camp_for_Name = "";

            for (int iIndex = 0; iIndex < chk_for.Items.Count; iIndex++)
            {
                if (chk_for.Items[iIndex].Selected == true)
                {
                    Camp_for_Code += chk_for.Items[iIndex].Value + ",";
                    Camp_for_Name += chk_for.Items[iIndex].Text + ",";
                }
            }

            if (Camp_for_Code != "")
            {

                Camp_for_Code = Camp_for_Code.Remove(Camp_for_Code.Length - 1);
                Camp_for_Name = Camp_for_Name.Remove(Camp_for_Name.Length - 1);
            }

            // Add New Doctor Sub-Category
            Doctor dv = new Doctor();
            int iReturn = dv.RecordAddSubCat(divcode, Doc_SubCat_SName, DocSubCatName, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), txtDr_tagg.Text, ddlfor.SelectedValue, Camp_for_Code, Camp_for_Name, All_DrsTagg, txtvisit.Text, strinput_value, strinput_name, txtRs.Text, strstate_value, strstate_name, txtsms.Text);

            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Sub-Category Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {
                // menu1.Status = "Doctor Sub-Category already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
                txtDocSubCatName.Focus();
            }
            else if (iReturn == -3)
            {
                // menu1.Status = "Doctor Sub-Category already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtDoc_SubCat_SName.Focus();
            }
        }
        else
        {
            // Update Doctor Sub-Category
            Doctor dv = new Doctor();
            int DocSCatCode = Convert.ToInt16(DocSubCatCode);

            if (Session["inputValue"] != null)
            {

                strinput_value = Session["inputValue"].ToString();
                strinput_name = Session["inputname"].ToString();
            }

            if (Session["stateValue"] != null)
            {
                strstate_value = Session["stateValue"].ToString();
                strstate_name = Session["statename"].ToString();
            }



            if (chkall_drs.Checked == true)
            {
                All_DrsTagg = 1;
            }
            else
            {
                All_DrsTagg = 0;
            }
            string Camp_for_Code = "";
            string Camp_for_Name = "";

            for (int iIndex = 0; iIndex < chk_for.Items.Count; iIndex++)
            {
                if (chk_for.Items[iIndex].Selected == true)
                {
                    Camp_for_Code += chk_for.Items[iIndex].Value + ",";
                    Camp_for_Name += chk_for.Items[iIndex].Text + ",";
                }
            }

            if (Camp_for_Code != "")
            {

                Camp_for_Code = Camp_for_Code.Remove(Camp_for_Code.Length - 1);
                Camp_for_Name = Camp_for_Name.Remove(Camp_for_Name.Length - 1);
            }

            int iReturn = dv.RecordUpdateSubCatnew(DocSCatCode, Doc_SubCat_SName, DocSubCatName, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text), divcode, txtDr_tagg.Text, ddlfor.SelectedValue, Camp_for_Code, Camp_for_Name, All_DrsTagg, txtvisit.Text, strinput_value, strinput_name, txtRs.Text, strstate_value, strstate_name, txtsms.Text);
            if (iReturn > 0)
            {
                // menu1.Status = "Doctor Sub-Category Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='DoctorCampaignList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                // menu1.Status = "Doctor Sub-Category already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
                txtDocSubCatName.Focus();
            }
            else if (iReturn == -3)
            {
                // menu1.Status = "Doctor Sub-Category already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtDoc_SubCat_SName.Focus();

            }
        }
    }
    private void Resetall()
    {
        txtDoc_SubCat_SName.Text = "";
        txtDocSubCatName.Text = "";
        txtEffFrom.Text = "";
        txtEffTo.Text = "";
        txtDr_tagg.Text = "";
        ddlfor.SelectedValue = "";
        chk_for.ClearSelection();
        txtvisit.Text = "";
        txtinput.Text = "";
        txtRs.Text = "";
        txtstate.Text = "";
        txtsms.Text = "";
        chkstate.ClearSelection();
        Chkinput.ClearSelection();
    }

    protected void ddlfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillType_For();

        Doctor dvv = new Doctor();
        dsDoc2 = dvv.getDocSubCat_Campfor(divcode, DocSubCatCode, ddlfor.SelectedValue);
        if (dsDoc2.Tables[0].Rows.Count > 0)
        {
            string str = dsDoc2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            // FillType_For();

            string Camp_for_Code2 = string.Empty;
            string[] strweek;
            iIndex = -1;
            strweek = str.Split(',');
            //  Session["Value"] = str.Remove(str.Length - 1);
            Session["Value"] = str;
            foreach (string Wk in strweek)
            {
                for (iIndex = 0; iIndex < chk_for.Items.Count; iIndex++)
                {
                    if (Wk == chk_for.Items[iIndex].Value)
                    {
                        //chk_for.Text = "";
                        chk_for.Items[iIndex].Selected = true;

                        if (chk_for.Items[iIndex].Selected == true)
                        {
                            Camp_for_Code2 += chk_for.Items[iIndex].Text + ",";
                            chk_for.Items[iIndex].Attributes.Add("style", "Color: magenta;font-weight:Bold");

                        }
                    }
                }
            }
        }
    }

    private void FillType_For()
    {
        Doctor spec = new Doctor();
        DataSet dsChkSp = new DataSet();

        if (ddlfor.SelectedValue == "Category")
        {
            dsChkSp = spec.getCat_ForDrCam(divcode);
            chk_for.DataSource = dsChkSp;
            chk_for.DataTextField = "Doc_Cat_Name";
            chk_for.DataValueField = "Doc_Cat_Code";
            chk_for.DataBind();
        }
        else if (ddlfor.SelectedValue == "Speciality")
        {
            dsChkSp = spec.getSpec_DrCamp(divcode);
            chk_for.DataSource = dsChkSp;
            chk_for.DataTextField = "Doc_Special_Name";
            chk_for.DataValueField = "Doc_Special_Code";
            chk_for.DataBind();
        }
        else if (ddlfor.SelectedValue == "Class")
        {
            dsChkSp = spec.getSpec_DrClass(divcode);
            chk_for.DataSource = dsChkSp;
            chk_for.DataTextField = "Doc_ClsName";
            chk_for.DataValueField = "Doc_ClsCode";
            chk_for.DataBind();
        }
        else if (ddlfor.SelectedValue == "Brand")
        {
            dsChkSp = spec.getSpec_DrBrand(divcode);
            chk_for.DataSource = dsChkSp;
            chk_for.DataTextField = "Product_Brd_Name";
            chk_for.DataValueField = "Product_Brd_Code";
            chk_for.DataBind();
        }

        else if (ddlfor.SelectedValue == "Product")
        {
            dsChkSp = spec.getSpec_DrProduct(divcode);
            chk_for.DataSource = dsChkSp;
            chk_for.DataTextField = "Product_Detail_Name";
            chk_for.DataValueField = "Product_Code_SlNo";
            chk_for.DataBind();
        }


    }

    protected void Chkinput_SelectedIndexChanged(object sender, EventArgs e)
    {

        string s = "";
        string Value = "";


        for (int i = 0; i < Chkinput.Items.Count; i++)
        {

            if (Chkinput.Items[i].Selected)//changed 1 to i  
            {
                s += Chkinput.Items[i].Text.ToString() + ","; //changed 1 to i
                Value += Chkinput.Items[i].Value.ToString() + ",";
            }

        }
        txtinput.Text = s;
        if (Value != "")
        {
            Session["inputValue"] = Value.Remove(Value.Length - 1);
            Session["inputname"] = s.Remove(s.Length - 1);
        }
       // txtinput.Text = s.TrimEnd(',');


    }

    private void fillInput()
    {
        Doctor dr = new Doctor();
        DataSet dsInput = new DataSet();

        dsInput = dr.getInput_ForDrCam(divcode);
        if (dsInput.Tables[0].Rows.Count > 0)
        {
            Chkinput.DataSource = dsInput;
            Chkinput.DataTextField = "Gift_Name";
            Chkinput.DataValueField = "Gift_Code";
            Chkinput.DataBind();
        }
    }

    private void fillState()
    {
        Division Div = new Division();
        DataSet dsState = new DataSet();
        dsState = Div.getStatePerDivision(divcode);

        if (dsState.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
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
            dsState = st.getStateAddChkBox_ForCamp(state_cd);
            chkstate.DataTextField = "statename";
            chkstate.DataValueField = "state_code";
            chkstate.DataSource = dsState;
            chkstate.DataBind();
        }
    }

    protected void chkstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string statename = "";
        string stateValue = "";


        for (int i = 0; i < chkstate.Items.Count; i++)
        {

            if (chkstate.Items[i].Selected)//changed 1 to i  
            {
                statename += chkstate.Items[i].Text.ToString() + ","; //changed 1 to i
                stateValue += chkstate.Items[i].Value.ToString() + ",";
            }

        }
        txtstate.Text = statename;
        if (stateValue != "")
        {
            Session["stateValue"] = stateValue.Remove(stateValue.Length - 1);
            Session["statename"] = statename.Remove(statename.Length - 1);
        }
       // txtstate.Text = statename.TrimEnd(',');
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DoctorCampaignList.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Other_Setup : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsadmin = null;
    DataSet dsDesignation = null;
    string strMail = string.Empty;
    Designation design = new Designation();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = Page.Title;

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;

            AdminSetup adm = new AdminSetup();

            DataSet dsTP = null;
            DataSet dsInput = null;
            //dsTP = adm.getSetup_BindYear(div_code);
            ddl_Sample_Year.Items.Add(("--Select--").ToString());
            //if (dsTP.Tables[0].Rows.Count > 0)
            //{
            //    if (dsTP.Tables[0].Rows.Count.ToString() != "")
            //    {
            //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            //        {
            //            ddl_Sample_Year.Items.Add(k.ToString());
            //            //ddl_Sample_Year.SelectedValue = DateTime.Now.Year.ToString();
            //        }
            //    }
            //}
            //else
            //{
                for (int k = Convert.ToInt32((DateTime.Now.Year - 1).ToString()); k <= DateTime.Now.Year + 1; k++)
                {
                    ddl_Sample_Year.Items.Add(k.ToString());
                    //ddl_Sample_Year.SelectedValue = DateTime.Now.Year.ToString();
                }
            //}

            //dsInput = adm.getSetup_Bind_InputYear(div_code);
            ddl_Input_Year.Items.Add(("--Select--").ToString());
            //if (dsInput.Tables[0].Rows.Count > 0)
            //{
            //    if (dsInput.Tables[0].Rows.Count.ToString() != "")
            //    {
            //        for (int k = Convert.ToInt16(dsInput.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            //        {
            //            ddl_Input_Year.Items.Add(k.ToString());
            //            //ddl_Input_Year.SelectedValue = DateTime.Now.Year.ToString();
            //        }
            //    }
            //}
            //else
            //{
                for (int k = Convert.ToInt32((DateTime.Now.Year - 1).ToString()); k <= DateTime.Now.Year + 1; k++)
                {
                    ddl_Input_Year.Items.Add(k.ToString());
                    //ddl_Input_Year.SelectedValue = DateTime.Now.Year.ToString();
                }
            //}


            dsadmin = adm.getSetup_forTargetFix(div_code);
            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                {
                    rdotarfix.SelectedValue = "1";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "2")
                {
                    rdotarfix.SelectedValue = "2";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "3")
                {
                    rdotarfix.SelectedValue = "3";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "4")
                {
                    rdotarfix.SelectedValue = "4";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "5")
                {
                    rdotarfix.SelectedValue = "5";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "6")
                {
                    rdotarfix.SelectedValue = "6";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "7")
                {
                    rdotarfix.SelectedValue = "7";
                    rdotarfix.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    rdotarcalen.SelectedValue = "1";
                    rdotarcalen.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "2")
                {
                    rdotarcalen.SelectedValue = "2";
                    rdotarcalen.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "3")
                {
                    rdotarcalen.SelectedValue = "3";
                    rdotarcalen.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "4")
                {
                    rdotarcalen.SelectedValue = "4";
                    rdotarcalen.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "5")
                {
                    rdotarcalen.SelectedValue = "5";
                    rdotarcalen.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "M")
                {
                    rdodrbus.SelectedValue = "M";
                    rdodrbus.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "T")
                {
                    rdodrbus.SelectedValue = "T";
                    rdodrbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "R")
                {
                    rdodrbus.SelectedValue = "R";
                    rdodrbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "N")
                {
                    rdodrbus.SelectedValue = "N";
                    rdodrbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "D")
                {
                    rdodrbus.SelectedValue = "D";
                    rdodrbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "Y")
                {
                    rbtCRMMgr.SelectedValue = "Y";
                    rbtCRMMgr.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "M")
                {
                    rbtCRMMgr.SelectedValue = "M";
                    rbtCRMMgr.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "A")
                {
                    rbtCRMMgr.SelectedValue = "A";
                    rbtCRMMgr.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "N")
                {
                    rbtCRMMgr.SelectedValue = "N";
                    rbtCRMMgr.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "LM")
                {
                    rbtCRMAprl.SelectedValue = "LM";
                    rbtCRMAprl.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "All")
                {
                    rbtCRMAprl.SelectedValue = "All";
                    rbtCRMAprl.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                rdoHosbus.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "M")
                {
                    rdoHosbus.SelectedValue = "M";
                    rdoHosbus.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "T")
                {
                    rdoHosbus.SelectedValue = "T";
                    rdoHosbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "R")
                {
                    rdoHosbus.SelectedValue = "R";
                    rdoHosbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "N")
                {
                    rdoHosbus.SelectedValue = "N";
                    rdoHosbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "D")
                {
                    rdoHosbus.SelectedValue = "D";
                    rdoHosbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                rdossLock.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString() == "Y")
                {
                    rdossLock.SelectedValue = "Y";
                    rdossLock.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString() == "N")
                {
                    rdossLock.SelectedValue = "N";
                    rdossLock.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                rdo_ListDRbus.SelectedValue = "R";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "M")
                {
                    rdo_ListDRbus.SelectedValue = "M";
                    rdo_ListDRbus.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "T")
                {
                    rdo_ListDRbus.SelectedValue = "T";
                    rdo_ListDRbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "R")
                {
                    rdo_ListDRbus.SelectedValue = "R";
                    rdo_ListDRbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }

                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "N")
                {
                    rdo_ListDRbus.SelectedValue = "N";
                    rdo_ListDRbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "D")
                {
                    rdo_ListDRbus.SelectedValue = "D";
                    rdo_ListDRbus.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }



                rdoentitlemr.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() == "Y")
                {
                    rdoentitlemr.SelectedValue = "Y";
                    rdoentitlemr.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(21).ToString() == "N")
                {
                    rdoentitlemr.SelectedValue = "N";
                    rdoentitlemr.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                rdoentitlemgr.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() == "Y")
                {
                    rdoentitlemgr.SelectedValue = "Y";
                    rdoentitlemgr.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() == "N")
                {
                    rdoentitlemgr.SelectedValue = "N";
                    rdoentitlemgr.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                //new

                RadioSample.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString() == "Y")
                {
                    RadioSample.SelectedValue = "Y";
                    RadioSample.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString() == "N")
                {
                    RadioSample.SelectedValue = "N";
                    RadioSample.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                RadioInput.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "Y")
                {
                    RadioInput.SelectedValue = "Y";
                    RadioInput.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "N")
                {
                    RadioInput.SelectedValue = "N";
                    RadioInput.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }
                //new

                //RbtDCRApprRemks.SelectedValue = "N";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString() == "Y")
                {
                    RbtDCRApprRemks.SelectedValue = "Y";
                    RbtDCRApprRemks.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString() == "N")
                {
                    RbtDCRApprRemks.SelectedValue = "N";
                    RbtDCRApprRemks.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
                }


                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(20) != DBNull.Value)
                {
                    ddlLockDay.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                }


                txtleave.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtdelay.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                ddlStart_date.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                ddlEnd_date.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                txtshortname.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();

                ddlresig.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();

                txtresig.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();

                ddl_Sample_Month.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                ddl_Sample_Year.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();

                ddl_Input_Month.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                ddl_Input_Year.SelectedValue = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();

                if (dsadmin.Tables[0].Rows[0]["Mail_System"].ToString() == "1")
                {
                    rdomail.Checked = true;

                }
                else
                {

                    rdonoMail.Checked = true;
                }
                dsDesignation = design.getDesignationMGRMailcrn(div_code);
                if (dsDesignation.Tables[0].Rows.Count > 0)
                {
                    gvDesignationmail.DataSource = dsDesignation;
                    gvDesignationmail.DataBind();
                }
            }
            dsadmin = design.getDesignation_Sys_ApprovalMailcrn("", div_code);

            foreach (GridViewRow row in gvDesignationmail.Rows)
            {
                CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
                CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
                Label lblDesignation = (Label)row.FindControl("lblDesignation");
                if (dsadmin.Tables[0].Rows[row.RowIndex]["Designation_Short_Name"].ToString() == lblDesignation.Text)
                {
                    if (dsadmin.Tables[0].Rows[row.RowIndex]["Mail_Allowed"].ToString() == "1")
                    {
                        ChkBoxId.Checked = true;
                    }
                    else
                    {
                        ChkBoxNo.Checked = true;
                    }
                }

            }

        }
        
        if (rdomail.Checked)
        {
            strMail = "1";
            gvDesignationmail.Visible = true;
            Label1.Visible = true;
            Label2.Visible = true;
        }
        else if (rdonoMail.Checked)
        {
            strMail = "0";
            gvDesignationmail.Visible = false;

        }
    }
    protected void chkId_OnCheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvDesignationmail.Rows)
        {
            CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
            CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
            if (ChkBoxId.Checked == true)
            {

                ChkBoxNo.Checked = false;
            }

        }
    }

    protected void chkNo_OnCheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvDesignationmail.Rows)
        {
            CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
            CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
            if (ChkBoxNo.Checked == true)
            {
                ChkBoxId.Checked = false;
            }

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AdminSetup dv = new AdminSetup();

        string Year = ddl_Sample_Year.SelectedValue;
        string YearInput = ddl_Input_Year.SelectedValue;
        if (ddl_Sample_Year.SelectedValue == "--Select--")
        {
            Year = 0.ToString();
        }
        if (ddl_Input_Year.SelectedValue == "--Select--")
        {
            YearInput = 0.ToString();
        }
        if (rdomail.Checked)
        {
            strMail = "1";
        }
        else if (rdonoMail.Checked)
        {
            strMail = "0";
        }

        int iReturn = dv.TargetFix_Setup(div_code, rdotarfix.SelectedValue, rdotarcalen.SelectedValue, rdodrbus.SelectedValue, txtleave.Text, txtdelay.Text, ddlStart_date.SelectedValue, ddlEnd_date.SelectedValue, txtshortname.Text, Convert.ToInt16(ddlresig.SelectedValue), txtresig.Text, rbtCRMMgr.SelectedValue, rbtCRMAprl.SelectedValue, rdoHosbus.SelectedValue, rdossLock.SelectedValue, ddl_Sample_Month.SelectedValue, Year, ddl_Input_Month.SelectedValue, YearInput, rdo_ListDRbus.SelectedValue, Convert.ToInt32(ddlLockDay.SelectedValue), rdoentitlemr.SelectedValue, rdoentitlemgr.SelectedValue, RbtDCRApprRemks.SelectedValue,RadioSample.SelectedValue,RadioInput.SelectedValue, strMail);
        int strChkId;
        foreach (GridViewRow row in gvDesignationmail.Rows)
        {
            CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
            CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
            Label lblDesignation = (Label)row.FindControl("lblDesignation");
            if (ChkBoxId.Checked == true)
            {
                strChkId = 1;
            }
            else
            {
                strChkId = 0;
            }

            iReturn = dv.RecordUpdate_DesigMGRMailcrn(strChkId, lblDesignation.Text, div_code);
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Setup has been updated Successfully');</script>");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
}
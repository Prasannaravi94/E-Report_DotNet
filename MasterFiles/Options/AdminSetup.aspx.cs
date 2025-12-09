using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_AdminSetup : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsadmin = null;
    DataSet dsadm = null;
    string div_code = string.Empty;
    DataSet dsTerr = new DataSet();
    string Doc_MulPlan = string.Empty;
    string DCR_TPBased = string.Empty;
    string DCR_DocCnt = string.Empty;
    string DCR_CheCnt = string.Empty;
    string DCR_StkCnt = string.Empty;
    string DCR_UnLstDocCnt = string.Empty;
    string DCR_ProdSelCnt = string.Empty;
    string DCR_ProdQtyZero = string.Empty;
    string DCR_Sess = string.Empty;
    string DCR_Time = string.Empty;
    int RemarksLength = -1;
    string strDCRTP = string.Empty;
    string MaxDocCnt = string.Empty;
    DataSet dsTerritory = new DataSet();
    string MaxCheCnt = string.Empty;
    string MaxStkCnt = string.Empty;
    int iIndex = -1;
    string doc_code = string.Empty;
    string terr_code = string.Empty;
    int UnLstDr_reqd = -1;
    string territory_code = string.Empty;
    string[] terr_cd;
    int pob = -1;
    int terr_sl_no = 0;
    int doc_disp = -1;
    int sess_dcr = 1;
    int time_dcr = 1;
    int prod_Qty_zero = -1;
    int prod_selection = -1;
    int max_dcr_prod = -1;
    int sess_mand_dcr = 1;
    int time_mand_dcr = 1;
    bool isValid = false;
    int iDelayedSystem = 0;
    int iApprovalSystem = 0;
    int iHolidayCalc = 0;
    int iDelayAllowDays = 0;
    int iHolidayStatus = 0;
    int iSundayStatus = 0;
    int lock_sysyem = 0;
    int iDrRem = 0;
    int iNewChem = 0;
    int iNewUn = 0;
    int iDocApp = 0;
    int iDeactApp = 0;
    int iAddDeact = 0;
    int FFWiseDly = 0;
    string desig_cd = string.Empty;
    string des_cd = string.Empty;
    string sDesig = string.Empty;
    string sDes = string.Empty;
    string Designation_Short_Name = string.Empty;
    string[] desigcd;
    string[] descd;
    DataSet dsDesignation = null;
    DataSet dsDesignation_v = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //hHeading.InnerText = Page.Title;
        if (!Page.IsPostBack)
        {
            Session["click"] = null;
            FillEntry_Mode();
            FillDisplay_Mode();
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;                       
            AdminSetup dv = new AdminSetup();
            dsadmin = dv.getAdminSetup(div_code);
            FillWorkName();
            GetWorkName();
            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                txtDRAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtChemAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                txtStkAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                ddlWorkingAreaList.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(32).ToString();
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() == "0")
                {
                    rdoMultiDRNo.Checked = true;
                }
                else
                {
                    rdoMultiDRYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "0")
                {
                    rdoFFDCRTimeNo.Checked = true;
                }
                else
                {

                    rdoFFDCRTimeYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "0")
                {
                    rdoFFDCRNo.Checked = true;
                }
                else
                {

                    rdoFFDCRYes.Checked = true;
                }
                txtChemAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                txtDRAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                txtUNLAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                txtStkAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                txtHosAllow.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "1")
                {
                    rdoDCRNone.Checked = true;
                }
                else
                {
                    rdoDCRNone.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "2")
                {
                    rdoDCRSVLNo.Checked = true;
                }
                else
                {
                    rdoDCRSVLNo.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "3")
                {
                    rdoDCRSpeciality.Checked = true;
                }
                else
                {
                    rdoDCRSpeciality.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "4")
                {
                    rdoDCRCategory.Checked = true;
                }
                else
                {
                    rdoDCRCategory.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "5")
                {
                    rdoClass.Checked = true;
                }
                else
                {
                    rdoClass.Checked = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "6")
                {
                    rdoCampaign.Checked = true;
                }
                else
                {
                    rdoCampaign.Checked = false;
                }


                if (dsadmin.Tables[0].Rows[0]["DCR_Dr_Mandatory"].ToString() == "1")
                {
                    rdodcrDr.SelectedValue = "1";
                }
                else
                {
                    rdodcrDr.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["Display_Patchwise_DR"].ToString() == "1")
                {
                    rdoDrPatch.SelectedValue = "1";
                }
                else
                {
                    rdoDrPatch.SelectedValue = "0";
                }



                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "1")
                {
                    rdoFFUNLYes.Checked = true;
                }
                else
                {
                    rdoFFUNLNo.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString() == "1")
                {
                    rdoFFDCRQtyYes.Checked = true;
                }
                else
                {
                    rdoFFDCRQtyNo.Checked = true;
                }

                //if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "0")
                //{
                //    rdoFFDCRQtyYes.Checked = true;
                //}
                //else
                //{
                //    rdoFFDCRQtyNo.Checked = true;
                //}

                //txtFFRemarks.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                if (dsadmin.Tables[0].Rows[0]["TpBased"].ToString() == "0")
                {
                    rdoDCRTP.Checked = true;
                }
                else
                {

                    rdoDCRWTP.Checked = true;
                }
                txtNoofTourPlan.Text = dsadmin.Tables[0].Rows[0]["No_of_TP_View"].ToString();
                txtFFProd.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "1")
                {
                    rdopobprod.Checked = true;
                    rdoFFPOBYes.Checked = true;
                    rdoFFPOBNo.Checked = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "2")
                {
                    rdopobdoc.Checked = true;
                    rdoFFPOBYes.Checked = true;
                    rdoFFPOBNo.Checked = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "3")
                {
                    rdopobdocrx.Checked = true;
                    rdoFFPOBYes.Checked = true;
                    rdoFFPOBNo.Checked = false;
                }
                else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "0")
                {
                    rdopobprod.Checked = false;
                    rdopobdoc.Checked = false;
                    rdopobdocrx.Checked = false;
                    rdoFFPOBYes.Checked = false;
                    rdoFFPOBNo.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "1")
                {
                    rdoSessMYes.Checked = true;
                }
                else
                {
                    rdoSessMNo.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString() == "1")
                {
                    rdoTimeMYes.Checked = true;
                }
                else
                {
                    rdoTimeMNo.Checked = true;
                }
                txtMaxProd.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                Session["TPView"] = txtNoofTourPlan.Text;

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString() == "0")
                {
                    rdoDlyNo.Checked = true;
                }
                else
                {
                    rdoDlyYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString() == "0")
                {
                    rdoDlyHolidayNo.Checked = true;
                }
                else
                {
                    rdoDlyHoliday.Checked = true;
                }

                txtNoDaysDly.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "0")
                {
                    rdoAutoHldNo.Checked = true;
                }
                else
                {
                    rdoAutoHldYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString() == "0")
                {
                    rdoAutoSunNo.Checked = true;
                }
                else
                {
                    rdoAutoSunYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString() == "0")
                {
                    rdoAprNo.Checked = true;
                }
                else
                {
                    rdoAprYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString() == "0")
                {
                    rdoDRNo.Checked = true;
                }
                else
                {
                    rdoDRYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString() == "0")
                {
                    rdoCheNo.Checked = true;
                }
                else
                {
                    rdoCheYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString() == "0")
                {
                    rdoUnNo.Checked = true;
                }
                else
                {
                    rdoUnYes.Checked = true;
                }

                txtFFRemarks.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                //for (int i = 28; i < dsadmin.Tables[0].Columns.Count; i++)
                //{
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(31).ToString() == "0")
                {
                    rdodocNo.Checked = true;
                }
                else
                {
                    rdodocYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(33).ToString() == "0")
                {
                    rdodeactNo.Checked = true;
                }
                else
                {
                    rdodeactYes.Checked = true;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(34).ToString() == "0")
                {
                    rdoadddeaNo.Checked = true;
                }
                else
                {
                    rdoadddeaYes.Checked = true;
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(35).ToString() == "1")
                {
                    rdolock_yes.Checked = true;
                }
                else
                {
                    rdolock_no.Checked = true;
                }
                //txtlock_mr.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
                //txtlock_week_holi.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
                txtlock_timelimit.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(37).ToString() == "1")
                {
                    rdoprdfeedYes_No.SelectedValue = "1";
                    ddlprdfeed.Visible = true;
                    FillProduct_Feedback();
                }
                else
                {
                    rdoprdfeedYes_No.SelectedValue = "0";
                    ddlprdfeed.Visible = false;
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(38).ToString() == "1")
                {
                    rdoRxqntyYes_No.SelectedValue = "1";
                }
                //else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(38).ToString() == "2")
                //{
                //    rdoRxqntyYes_No.SelectedValue = "2";
                //}
                else
                {
                    rdoRxqntyYes_No.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["ChemPOB_Qty_Needed"].ToString() == "1")
                {
                    rdoChemistQty.SelectedValue = "1";
                }
                else
                {
                    rdoChemistQty.SelectedValue = "0";
                }

                if (dsadmin.Tables[0].Rows[0]["PrdSample_Qty_Needed"].ToString() == "1")
                {
                    rdoDr_SampleQty_Needed.SelectedValue = "1";
                }
                else
                {
                    rdoDr_SampleQty_Needed.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["ChemSample_Qty_Needed"].ToString() == "1")
                {
                    rdoChem_SampleQty_Needed.SelectedValue = "1";
                }
                else
                {
                    rdoChem_SampleQty_Needed.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(39).ToString() == "1")
                {
                    rdopobLstYes_No.SelectedValue = "1";
                }
                else
                {
                    rdopobLstYes_No.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(40).ToString() == "1")
                {
                    rdopobChemYes_No.SelectedValue = "1";
                }
                else
                {
                    rdopobChemYes_No.SelectedValue = "0";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(41).ToString() == "1")
                {
                    rdoFFDayYes_No.SelectedValue = "1";
                }
                else
                {
                    rdoFFDayYes_No.SelectedValue = "0";
                }

                txtUnDRAllowed.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(42).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(43).ToString() == "1")
                {
                    rdoprd_priority.SelectedValue = "1";
                    Label27.Visible = true;
                    txtprdrange.Visible = true;
                }
                else
                {
                    rdoprd_priority.SelectedValue = "0";
                }

                //if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(44).ToString() == "1")
                //{
                //    rdoTpBas.SelectedValue = "1";
                //}

                //else
                //{
                //    rdoTpBas.SelectedValue = "0";
                //}


                if (dsadmin.Tables[0].Rows[0]["TpBasesd_DCR"].ToString() == "1")
                {
                    rdoTpBas.SelectedValue = "1";
                }
                else
                {
                    rdoTpBas.SelectedValue = "0";
                }

                txtprdrange.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(45).ToString();

                if (dsadmin.Tables[0].Rows[0]["TPDCR_Deviation"].ToString() == "1")
                {
                    rdoTPdevia.SelectedValue = "1";
                }
                else
                {
                    rdoTPdevia.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["TPDCR_MGRAppr"].ToString() == "1")
                {
                    rdotpdcr_appr.SelectedValue = "1";
                }
                else
                {
                    rdotpdcr_appr.SelectedValue = "0";
                }

                txtRxqntyYes_No.Text = dsadmin.Tables[0].Rows[0]["PrdRx_Qty_Caption"].ToString();
                txtChemistQty.Text = dsadmin.Tables[0].Rows[0]["ChemPOB_Qty_Caption"].ToString();
                txtDr_SampleQty_Caption.Text = dsadmin.Tables[0].Rows[0]["PrdSample_Qty_Caption"].ToString();
                txtChem_SampleQty_Caption.Text = dsadmin.Tables[0].Rows[0]["ChemSample_Qty_Caption"].ToString();

                if (dsadmin.Tables[0].Rows[0]["Input_Mand"].ToString() == "1")
                {
                    rdoInput_Mand.SelectedValue = "1";
                }
                else
                {
                    rdoInput_Mand.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["Dr_POBQty_DefaZero"].ToString() == "1")
                {
                    rdoDrPOBQty0.SelectedValue = "1";
                }
                else
                {
                    rdoDrPOBQty0.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["Chem_SampleQty_DefaZero"].ToString() == "1")
                {
                    rdoChemSampleQty0.SelectedValue = "1";
                }
                else
                {
                    rdoChemSampleQty0.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["Chem_POBQty_DefaZero"].ToString() == "1")
                {
                    rdoChemPOBQty0.SelectedValue = "1";
                }
                else
                {
                    rdoChemPOBQty0.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["FieldForceWise_Delay"].ToString() == "1")
                {
                    chkFFWiseDly.Checked = true;
                }
                else
                {
                    chkFFWiseDly.Checked = false;
                }

                if (dsadmin.Tables[0].Rows[0]["Prod_SampleQty_Validation_Needed"].ToString() == "1")
                {
                    rdoProductSample.SelectedValue = "1";
                }
                else
                {
                    rdoProductSample.SelectedValue = "0";
                }
                if (dsadmin.Tables[0].Rows[0]["InputQty_Validation_Needed"].ToString() == "1")
                {
                    rdoInputSample.SelectedValue = "1";
                }
                else
                {
                    rdoInputSample.SelectedValue = "0";
                }


                txtFFWiseDly.Text = dsadmin.Tables[0].Rows[0]["FFWise_Delay_Days"].ToString();



                Designation design = new Designation();
                DataSet dsDesignation = new DataSet();

                dsDesignation = design.getDesignationMR(div_code);
                if (dsDesignation.Tables[0].Rows.Count > 0)
                {
                    gvDesignation.DataSource = dsDesignation;
                    gvDesignation.DataBind();
                }

                if (dsDesignation.Tables[0].Rows.Count > 0)
                {
                    grdtp_setup.DataSource = dsDesignation;
                    grdtp_setup.DataBind();
                }

                foreach (GridViewRow row in grdtp_setup.Rows)
                {
                    DropDownList ddlstart_date = (DropDownList)row.FindControl("ddlstart_date");
                    DropDownList ddlend_date = (DropDownList)row.FindControl("ddlend_date");
                    Label lblDesignation = (Label)row.FindControl("lblDesignation");

                    dsDesignation = design.getDesignation_Baselevel_Tp(div_code, lblDesignation.Text);
                    if (dsDesignation.Tables[0].Rows.Count > 0)
                    {

                        ddlstart_date.SelectedValue = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        ddlend_date.SelectedValue = dsDesignation.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    }
                }

                //Designation Desig = new Designation();
                dsadmin = design.getDesignation_Sys_Approval("", div_code);

                foreach (GridViewRow row in gvDesignation.Rows)
                {
                    CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
                    CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
                    Label lblDesignation = (Label)row.FindControl("lblDesignation");
                    if (dsadmin.Tables[0].Rows[row.RowIndex]["Designation_Short_Name"].ToString() == lblDesignation.Text)
                    {
                        if (dsadmin.Tables[0].Rows[row.RowIndex]["TP_Approval_Sys"].ToString() == "1")
                        {
                            ChkBoxId.Checked = true;
                        }
                        else
                        {
                            ChkBoxNo.Checked = true;
                        }
                    }
                }

                AdminSetup adm = new AdminSetup();
                dsadm = adm.get_DCR_Setups_Entry(div_code);
                foreach (GridViewRow row_1 in grdentrymode.Rows)
                {
                    CheckBox chklisteddr = (CheckBox)row_1.FindControl("chklisteddr");
                    CheckBox chkchemis = (CheckBox)row_1.FindControl("chkchemis");
                    CheckBox chkstockist = (CheckBox)row_1.FindControl("chkstockist");
                    CheckBox chkunlisted = (CheckBox)row_1.FindControl("chkunlisted");
                    CheckBox chkhospi = (CheckBox)row_1.FindControl("chkhospi");
                    Label typ = (Label)row_1.FindControl("lblmode");
                    CheckBoxList ChkDesig = (CheckBoxList)row_1.FindControl("ChkDesig");
                    TextBox txtDesig = (TextBox)row_1.FindControl("txtDesig");
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == typ.Text)
                    {
                        DataSet dsWrk = adm.getDCRSetup_Desig(typ.Text, div_code);

                        if (dsadm.Tables[0].Rows[row_1.RowIndex]["Msl"].ToString() == "1")
                        {
                            chklisteddr.Checked = true;
                        }
                        else
                        {
                            chklisteddr.Checked = false;
                        }
                        if (dsadm.Tables[0].Rows[row_1.RowIndex]["Chm"].ToString() == "1")
                        {
                            chkchemis.Checked = true;
                        }
                        else
                        {
                            chkchemis.Checked = false;
                        }
                        if (dsadm.Tables[0].Rows[row_1.RowIndex]["Stk"].ToString() == "1")
                        {
                            chkstockist.Checked = true;
                        }
                        else
                        {
                            chkstockist.Checked = false;
                        }

                        if (dsadm.Tables[0].Rows[row_1.RowIndex]["Unl"].ToString() == "1")
                        {
                            chkunlisted.Checked = true;
                        }
                        else
                        {
                            chkunlisted.Checked = false;
                        }
                        if (dsadm.Tables[0].Rows[row_1.RowIndex]["Hos"].ToString() == "1")
                        {
                            chkhospi.Checked = true;
                        }
                        else
                        {
                            chkhospi.Checked = false;
                        }


                        string value = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        string strtxtDes_Value = string.Empty;
                        string[] strDes;
                        iIndex = -1;
                        strDes = value.Split(',');
                        // Session["Value"] = str.Remove(str.Length - 1);
                        Session["Value"] = value;
                        foreach (string dg in strDes)
                        {
                            for (iIndex = 0; iIndex < ChkDesig.Items.Count; iIndex++)
                            {
                                if (dg == ChkDesig.Items[iIndex].Value)
                                {
                                    ChkDesig.Text = "";
                                    ChkDesig.Items[iIndex].Selected = true;

                                    if (ChkDesig.Items[iIndex].Selected == true)
                                    {
                                        strtxtDes_Value += ChkDesig.Items[iIndex].Text + ",";
                                    }
                                }
                            }
                        }


                        if (strtxtDes_Value != "")
                        {
                            txtDesig.Text = strtxtDes_Value.Remove(strtxtDes_Value.Length - 1);
                        }
                    }

                    typ.Visible = true;
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == "ses")
                    {
                        typ.Text = "Session";
                    }
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == "tm")
                    {
                        typ.Text = "Time";

                    }
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == "pob")
                    {
                        typ.Text = "Pob";
                    }
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == "prd")
                    {
                        typ.Text = "Product";
                    }
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == "inp")
                    {
                        typ.Text = "Input";
                    }
                    if (dsadm.Tables[0].Rows[row_1.RowIndex]["typ"].ToString() == "rem")
                    {
                        typ.Text = "Remarks";
                    }
                }

                dsadmin = adm.get_DCR_Setup_Display(div_code);

                foreach (GridViewRow row_2 in grddisplaymode.Rows)
                {
                    CheckBox chklisteddr_v = (CheckBox)row_2.FindControl("chklisteddr_v");
                    CheckBox chkchemis_v = (CheckBox)row_2.FindControl("chkchemis_v");
                    CheckBox chkstockist_v = (CheckBox)row_2.FindControl("chkstockist_v");
                    CheckBox chkunlisted_v = (CheckBox)row_2.FindControl("chkunlisted_v");
                    CheckBox chkhospi_v = (CheckBox)row_2.FindControl("chkhospi_v");
                    Label typ_v = (Label)row_2.FindControl("lblmode_v");
                    CheckBoxList ChkDesig_v = (CheckBoxList)row_2.FindControl("ChkDesig_v");
                    TextBox txtDesig_v = (TextBox)row_2.FindControl("txtDesig_v");

                    if (dsadmin.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == typ_v.Text)
                    {
                        DataSet dsWrk = adm.getDCRSetup_Desig(typ_v.Text, div_code);

                        if (dsadmin.Tables[0].Rows[row_2.RowIndex]["Msl_v"].ToString() == "1")
                        {
                            chklisteddr_v.Checked = true;
                        }
                        else
                        {
                            chklisteddr_v.Checked = false;
                        }
                        if (dsadmin.Tables[0].Rows[row_2.RowIndex]["Chm_v"].ToString() == "1")
                        {
                            chkchemis_v.Checked = true;
                        }
                        else
                        {
                            chkchemis_v.Checked = false;
                        }
                        if (dsadmin.Tables[0].Rows[row_2.RowIndex]["Stk_v"].ToString() == "1")
                        {
                            chkstockist_v.Checked = true;
                        }
                        else
                        {
                            chkstockist_v.Checked = false;
                        }
                        if (dsadmin.Tables[0].Rows[row_2.RowIndex]["Unl_v"].ToString() == "1")
                        {
                            chkunlisted_v.Checked = true;
                        }
                        else
                        {
                            chkunlisted_v.Checked = false;
                        }
                        if (dsadmin.Tables[0].Rows[row_2.RowIndex]["Hos_v"].ToString() == "1")
                        {
                            chkhospi_v.Checked = true;
                        }
                        else
                        {
                            chkhospi_v.Checked = false;
                        }

                        string value_v = dsWrk.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        string strtxtDes_Value_v = string.Empty;
                        string[] strDes_v;
                        iIndex = -1;
                        strDes_v = value_v.Split(',');
                        // Session["Value"] = str.Remove(str.Length - 1);
                        Session["value_v"] = value_v;
                        foreach (string dg_v in strDes_v)
                        {
                            for (iIndex = 0; iIndex < ChkDesig_v.Items.Count; iIndex++)
                            {
                                if (dg_v == ChkDesig_v.Items[iIndex].Value)
                                {
                                    ChkDesig_v.Text = "";
                                    ChkDesig_v.Items[iIndex].Selected = true;

                                    if (ChkDesig_v.Items[iIndex].Selected == true)
                                    {
                                        strtxtDes_Value_v += ChkDesig_v.Items[iIndex].Text + ",";
                                    }
                                }
                            }
                        }


                        if (strtxtDes_Value_v != "")
                        {
                            txtDesig_v.Text = strtxtDes_Value_v.Remove(strtxtDes_Value_v.Length - 1);
                        }
                    }


                    typ_v.Visible = true;
                    if (dsadm.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == "ses")
                    {
                        typ_v.Text = "Session";
                    }
                    if (dsadm.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == "tm")
                    {
                        typ_v.Text = "Time";

                    }
                    if (dsadm.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == "pob")
                    {
                        typ_v.Text = "Pob";
                    }
                    if (dsadm.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == "prd")
                    {
                        typ_v.Text = "Product";
                    }
                    if (dsadm.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == "inp")
                    {
                        typ_v.Text = "Input";
                    }
                    if (dsadm.Tables[0].Rows[row_2.RowIndex]["typ"].ToString() == "rem")
                    {
                        typ_v.Text = "Remarks";
                    }
                }


                //}


            }

        }

        if (rdoDCRTP.Checked)
        {
            strDCRTP = "0";
            gvDesignation.Visible = true;
        }
        else if (rdoDCRWTP.Checked)
        {
            strDCRTP = "1";
            gvDesignation.Visible = false;
        }
    }
    protected void chkId_OnCheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvDesignation.Rows)
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
        foreach (GridViewRow row in gvDesignation.Rows)
        {
            CheckBox ChkBoxId = (CheckBox)row.FindControl("chkId");
            CheckBox ChkBoxNo = (CheckBox)row.FindControl("chkNo");
            if (ChkBoxNo.Checked == true)
            {
                ChkBoxId.Checked = false;
            }

        }
    }
    private void FillWorkName()
    {
        AdminSetup adm = new AdminSetup();
        dsadm = adm.FillWorkArea();
        if (dsadm.Tables[0].Rows.Count > 0)
        {
            ddlWorkingAreaList.DataTextField = "wrk_area_Name";
            ddlWorkingAreaList.DataValueField = "wrk_area_SName";
            ddlWorkingAreaList.DataSource = dsadm;
            ddlWorkingAreaList.DataBind();
        }
    }
    private void GetWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ddlWorkingAreaList.Items.Count; i++)
            {
                if (ddlWorkingAreaList.Items[i].Text == dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString())
                {
                    //  ddlWorkingAreaList.SelectedValue = ddlWorkingAreaList.SelectedValue;          
                    ddlWorkingAreaList.Items[i].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                    lblNoofTourPlan.Text = "No of " + dsTerritory.Tables[0].Rows[0]["wrk_area_SName"] + " Selection in TP";
                }
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        isValid = true;
        if (rdoMultiDRYes.Checked == true)
        {
            Doc_MulPlan = "1";
        }
        if (rdoMultiDRNo.Checked == true)
        {
            Doc_MulPlan = "0";
        }
        if (rdoFFUNLYes.Checked)
        {
            UnLstDr_reqd = 1;
        }
        if (rdoFFUNLNo.Checked)
        {
            UnLstDr_reqd = 0;
        }
        if (rdoFFDCRQtyYes.Checked)
        {
            prod_Qty_zero = 1;
        }
        if (rdoFFDCRQtyNo.Checked)
        {
            prod_Qty_zero = 0;
        }
        // ------- Changes done by Saravanan ----//
        if (rdoDCRTP.Checked)
        {
            strDCRTP = "0";
        }
        else if (rdoDCRWTP.Checked)
        {
            strDCRTP = "1";
        }


        if (rdoDCRNone.Checked)
        {
            doc_disp = 1;
        }
        else if (rdoDCRSVLNo.Checked)
        {
            doc_disp = 2;
        }
        else if (rdoDCRSpeciality.Checked)
        {
            doc_disp = 3;
        }
        else if (rdoDCRCategory.Checked)
        {
            doc_disp = 4;
        }
        else if (rdoClass.Checked)
        {
            doc_disp = 5;
        }
        else if (rdoCampaign.Checked)
        {
            doc_disp = 6;
        }


        if (rdoFFDCRYes.Checked)
        {
            sess_dcr = 1;
        }
        else
        {
            sess_dcr = 0;
        }
        if (rdoFFDCRTimeYes.Checked)
        {
            time_dcr = 1;
        }
        else
        {
            time_dcr = 0;
        }

        if (rdoSessMYes.Checked)
        {
            sess_mand_dcr = 1;
        }
        else
        {
            sess_mand_dcr = 0;
        }

        if (rdoTimeMYes.Checked)
        {
            time_mand_dcr = 1;
        }
        else
        {
            time_mand_dcr = 0;
        }

        if (rdoFFPOBYes.Checked)
        {
            if (rdopobprod.Checked)
            {
                pob = 1;
            }
            else if (rdopobdoc.Checked)
            {
                pob = 2;
            }
            else if (rdopobdocrx.Checked)
            {
                pob = 3;
            }
        }
        else
        {
            pob = 0;
        }

        if (txtFFProd.Text.Length > 0)
            prod_selection = Convert.ToInt16(txtFFProd.Text);

        if (txtFFRemarks.Text.Length > 0)
            RemarksLength = Convert.ToInt16(txtFFRemarks.Text);

        if (txtMaxProd.Text.Length > 0)
            max_dcr_prod = Convert.ToInt16(txtMaxProd.Text);

        //IF Remove Session is selected then Mandatory should be "No"
        //if (rdoFFDCRYes.Checked)
        //{
        //    if (rdoSessMYes.Checked)
        //    {
        //        isValid = false;
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Mandatory option should be set No If Remove Session option is enabled.Please resubmit');</script>");
        //        rdoSessMNo.Checked = true;
        //    }
        //}

        //if (rdoFFDCRTimeYes.Checked)
        //{
        //    if (rdoTimeMYes.Checked)
        //    {
        //        isValid = false;
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Time Mandatory option should be set No If Remove Time option is enabled.Please Resubmit');</script>");
        //        rdoTimeMNo.Checked = true;
        //    }
        //}

        //Maximum Product Seslected
        if (txtFFProd.Text.Trim().Length > 0)
        {
            if (txtMaxProd.Text.Trim().Length > 0)
            {
                if ((Convert.ToInt16(txtFFProd.Text.Trim())) < (Convert.ToInt16(txtMaxProd.Text.Trim())))
                {
                    isValid = false;
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('No. of Products should be less than or equal to Max products.');</script>");
                    txtMaxProd.Focus();
                }
            }
        }


        //Approval System added by Sridevi on 06/04/2015
        if (rdoAprYes.Checked)
            iApprovalSystem = 1;
        else
            iApprovalSystem = 0;

        //Delayed System added by Sridevi on 06/04/2015
        if (rdoDlyYes.Checked)
            iDelayedSystem = 1;
        else
            iDelayedSystem = 0;

        if (rdoDlyHoliday.Checked)
            iHolidayCalc = 1;
        else
            iHolidayCalc = 0;

        if (txtNoDaysDly.Text.Trim().Length > 0)
            iDelayAllowDays = Convert.ToInt16(txtNoDaysDly.Text);

        if (rdoAutoHldYes.Checked)
            iHolidayStatus = 1;
        else
            iHolidayStatus = 0;

        if (rdoAutoSunYes.Checked)
            iSundayStatus = 1;
        else
            iSundayStatus = 0;


        //New Che
        if (rdoCheYes.Checked)
            iNewChem = 1;
        else
            iNewChem = 0;

        //New Un
        if (rdoUnYes.Checked)
            iNewUn = 1;
        else
            iNewUn = 0;

        //Doc Rem
        if (rdoDRYes.Checked)
            iDrRem = 1;
        else
            iDrRem = 0;
        //Doc App
        if (rdodocYes.Checked)
            iDocApp = 1;
        else
            iDocApp = 0;

        //Doc deact App
        if (rdodeactYes.Checked)
            iDeactApp = 1;
        else
            iDeactApp = 0;
        //Doc ADD/Deact App
        if (rdoadddeaYes.Checked)
            iAddDeact = 1;
        else
            iAddDeact = 0;

        if (rdolock_yes.Checked)
        {
            lock_sysyem = 1;
        }
        else
        {
            lock_sysyem = 0;
        }
        if (chkFFWiseDly.Checked)
        {
           FFWiseDly = 1;
        }
        else
        {
            FFWiseDly = 0;
        }
        if (isValid)
        {
            // Update Setup           
            AdminSetup dv = new AdminSetup();
            int iReturn = dv.RecordUpdate(Doc_MulPlan, ddlWorkingAreaList.SelectedItem.ToString(),
                txtNoofTourPlan.Text.ToString(), Convert.ToInt32(txtDRAllow.Text.Trim()),
                Convert.ToInt32(txtChemAllow.Text.Trim()), Convert.ToInt32(txtStkAllow.Text.Trim()),
                Convert.ToInt32(txtUNLAllow.Text.Trim()), Convert.ToInt32(txtHosAllow.Text.Trim()),
                doc_disp, sess_dcr, time_dcr, UnLstDr_reqd, prod_Qty_zero, prod_selection,
                pob, sess_mand_dcr, time_mand_dcr, max_dcr_prod, ddlWorkingAreaList.SelectedValue.ToString(),
                iDelayedSystem, iHolidayCalc, iDelayAllowDays, iHolidayStatus,
                iSundayStatus, iApprovalSystem, div_code, strDCRTP, RemarksLength,
                iDrRem, iNewChem, iNewUn, txtDRAllowed.Text.ToString(), txtChemAllowed.Text.ToString(),
                iDocApp, iDeactApp, iAddDeact, txtStkAllowed.Text.ToString(), lock_sysyem,
                txtlock_timelimit.Text.Trim(), Convert.ToInt16(rdoprdfeedYes_No.SelectedValue.ToString()),
                Convert.ToInt16(rdoRxqntyYes_No.SelectedValue.ToString()),
                Convert.ToInt16(rdopobLstYes_No.SelectedValue.ToString()),
                Convert.ToInt16(rdopobChemYes_No.SelectedValue.ToString()),
                Convert.ToInt16(rdoFFDayYes_No.SelectedValue.ToString()),
                txtUnDRAllowed.Text, Convert.ToInt16(rdoprd_priority.SelectedValue.ToString()),
                Convert.ToInt16(rdoTpBas.SelectedValue),
                txtprdrange.Text, Convert.ToInt16(rdoTPdevia.SelectedValue), Convert.ToInt16(rdotpdcr_appr.SelectedValue),
                Convert.ToInt16(rdoChemistQty.SelectedValue), txtRxqntyYes_No.Text.ToString(), txtChemistQty.Text.ToString(),
                Convert.ToInt16(rdoDr_SampleQty_Needed.SelectedValue), Convert.ToInt16(rdoChem_SampleQty_Needed.SelectedValue),
                Convert.ToInt16(rdoInput_Mand.SelectedValue), txtDr_SampleQty_Caption.Text.ToString(), txtChem_SampleQty_Caption.Text.ToString(), Convert.ToInt16(rdoDrPOBQty0.SelectedValue),
                Convert.ToInt16(rdoChemSampleQty0.SelectedValue), Convert.ToInt16(rdoChemPOBQty0.SelectedValue), FFWiseDly, txtFFWiseDly.Text.Trim(), rdoDrPatch.SelectedValue, rdodcrDr.SelectedValue, Convert.ToInt16(rdoProductSample.SelectedValue), Convert.ToInt16(rdoInputSample.SelectedValue));
            if (iReturn > 0)
            {
                AdminSetup adm = new AdminSetup();
                iReturn = adm.RecordUpdate_LockSystemMGR(div_code, lock_sysyem, Doc_MulPlan);

                if (Doc_MulPlan == "1")
                {
                    dsadmin = dv.get_listed_doctor(div_code);

                    foreach (DataRow dataRow in dsadmin.Tables[0].Rows)
                    {
                        doc_code = dataRow["ListedDrCode"].ToString();
                        terr_code = dataRow["Territory_Code"].ToString();
                        terr_cd = terr_code.Split(',');
                        terr_sl_no = 0;
                        foreach (string terrcode in terr_cd)
                        {
                            if (terr_sl_no == 0)
                                territory_code = terrcode;

                            terr_sl_no = terr_sl_no + 1;
                        }

                        iReturn = dv.RecordUpdate_ListedDR(territory_code, doc_code, div_code);
                    }
                }

                int strChkId;
                foreach (GridViewRow row in gvDesignation.Rows)
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

                   // iReturn = dv.RecordUpdate_DesigMR(strChkId, lblDesignation.Text);
                }

                foreach (GridViewRow row in grdtp_setup.Rows)
                {
                    DropDownList ddlstart_date = (DropDownList)row.FindControl("ddlstart_date");
                    DropDownList ddlend_date = (DropDownList)row.FindControl("ddlend_date");
                    Label lblDesignation = (Label)row.FindControl("lblDesignation");


                    //if ((ddlstart_date.SelectedValue.ToString() != "0") && (ddlend_date.SelectedValue.ToString() != "0"))
                    //{

                    iReturn = dv.RecordUpdate_baselevel_tp(lblDesignation.Text, Convert.ToInt16(ddlstart_date.SelectedValue), Convert.ToInt16(ddlend_date.SelectedValue), div_code);
                    //}
                }

                int strlistdr;
                int strchem;
                int strstk;
                int strunlist;
                int strhos;

                foreach (GridViewRow row in grdentrymode.Rows)
                {
                    CheckBox chklisteddr = (CheckBox)row.FindControl("chklisteddr");
                    CheckBox chkchemis = (CheckBox)row.FindControl("chkchemis");
                    CheckBox chkstockist = (CheckBox)row.FindControl("chkstockist");
                    CheckBox chkunlisted = (CheckBox)row.FindControl("chkunlisted");
                    CheckBox chkhospi = (CheckBox)row.FindControl("chkhospi");
                    Label typ = (Label)row.FindControl("lblmode");
                    Label typ_2 = (Label)row.FindControl("lblmode2");
                    TextBox txtDesig = (TextBox)row.FindControl("txtDesig");
                    string des = txtDesig.Text.ToString();

                    string strtxtDes_text = string.Empty;
                    string strtxtDes_Value = string.Empty;
                    string[] strDes;
                    iIndex = -1;
                    strDes = des.Split(',');
                    Session["Value"] = des;

                    foreach (string dg in strDes)
                    {
                        for (iIndex = 0; iIndex < ChkDesig.Items.Count; iIndex++)
                        {
                            if (dg == ChkDesig.Items[iIndex].Text)
                            {
                                ChkDesig.Items[iIndex].Selected = true;

                                if (ChkDesig.Items[iIndex].Selected == true)
                                {
                                    strtxtDes_Value += ChkDesig.Items[iIndex].Value + ",";
                                }
                            }
                        }
                    }
                    if (strtxtDes_Value != "")
                    {
                        strtxtDes_Value = strtxtDes_Value.Remove(strtxtDes_Value.Length - 1);
                    }

                    typ_2.Visible = false;
                    typ.Visible = true;

                    if (chklisteddr.Checked == true)
                    {
                        strlistdr = 1;
                    }
                    else
                    {
                        strlistdr = 0;
                    }
                    if (chkchemis.Checked == true)
                    {
                        strchem = 1;
                    }
                    else
                    {
                        strchem = 0;
                    }
                    if (chkstockist.Checked == true)
                    {
                        strstk = 1;
                    }
                    else
                    {
                        strstk = 0;
                    }
                    if (chkunlisted.Checked == true)
                    {
                        strunlist = 1;
                    }
                    else
                    {
                        strunlist = 0;
                    }
                    if (chkhospi.Checked == true)
                    {
                        strhos = 1;
                    }
                    else
                    {
                        strhos = 0;
                    }

                    iReturn = dv.RecordUpdate_EntryDCR_Setups(strlistdr, strchem, strstk, strunlist, strhos, typ_2.Text, div_code, strtxtDes_Value);
                }

                int strlistdr_v;
                int strchem_v;
                int strstk_v;
                int strunlist_v;
                int strhos_v;

                foreach (GridViewRow row_1 in grddisplaymode.Rows)
                {
                    CheckBox chklisteddr_v = (CheckBox)row_1.FindControl("chklisteddr_v");
                    CheckBox chkchemis_v = (CheckBox)row_1.FindControl("chkchemis_v");
                    CheckBox chkstockist_v = (CheckBox)row_1.FindControl("chkstockist_v");
                    CheckBox chkunlisted_v = (CheckBox)row_1.FindControl("chkunlisted_v");
                    CheckBox chkhospi_v = (CheckBox)row_1.FindControl("chkhospi_v");
                    Label typ_v = (Label)row_1.FindControl("lblmode_v");
                    Label typ_v2 = (Label)row_1.FindControl("lblmode_v2");
                    TextBox txtDesig_v = (TextBox)row_1.FindControl("txtDesig_v");
                    string des_v = txtDesig_v.Text.ToString();

                    string strtxtDes_text_v = string.Empty;
                    string strtxtDes_Value_v = string.Empty;
                    string[] strDes_v;
                    iIndex = -1;
                    strDes_v = des_v.Split(',');
                    Session["Value"] = des_v;

                    foreach (string dg_v in strDes_v)
                    {
                        for (iIndex = 0; iIndex < ChkDesig_v.Items.Count; iIndex++)
                        {
                            if (dg_v == ChkDesig_v.Items[iIndex].Text)
                            {
                                ChkDesig_v.Items[iIndex].Selected = true;

                                if (ChkDesig_v.Items[iIndex].Selected == true)
                                {
                                    strtxtDes_Value_v += ChkDesig_v.Items[iIndex].Value + ",";
                                }
                            }
                        }
                    }
                    if (strtxtDes_Value_v != "")
                    {
                        strtxtDes_Value_v = strtxtDes_Value_v.Remove(strtxtDes_Value_v.Length - 1);
                    }

                    typ_v2.Visible = false;

                    if (chklisteddr_v.Checked == true)
                    {
                        strlistdr_v = 1;
                    }
                    else
                    {
                        strlistdr_v = 0;
                    }
                    if (chkchemis_v.Checked == true)
                    {
                        strchem_v = 1;
                    }
                    else
                    {
                        strchem_v = 0;
                    }
                    if (chkstockist_v.Checked == true)
                    {
                        strstk_v = 1;
                    }
                    else
                    {
                        strstk_v = 0;
                    }
                    if (chkunlisted_v.Checked == true)
                    {
                        strunlist_v = 1;
                    }
                    else
                    {
                        strunlist_v = 0;
                    }
                    if (chkhospi_v.Checked == true)
                    {
                        strhos_v = 1;
                    }
                    else
                    {
                        strhos_v = 0;
                    }

                    iReturn = dv.RecordUpdate_DisplayDCR_Setups(strlistdr_v, strchem_v, strstk_v, strunlist_v, strhos_v, typ_v2.Text, div_code, strtxtDes_Value_v);
                }
                //menu1.Status = "Setup has been updated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Setup has been updated Successfully');</script>");

            }
        }
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Work_Type_Setup.aspx");
    }

    private void FillEntry_Mode()
    {
        AdminSetup adm = new AdminSetup();
        dsadmin = adm.getDCR_EntryMode(div_code);
        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            grdentrymode.Visible = true;
            grdentrymode.DataSource = dsadmin;
            grdentrymode.DataBind();
        }
        else
        {
            grdentrymode.DataSource = dsadmin;
            grdentrymode.DataBind();
        }
    }
    private void FillDisplay_Mode()
    {
        AdminSetup ad = new AdminSetup();
        dsadmin = ad.getDCR_DisplayMode(div_code);
        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            grddisplaymode.Visible = true;
            grddisplaymode.DataSource = dsadmin;
            grddisplaymode.DataBind();
        }
        else
        {
            grddisplaymode.DataSource = dsadmin;
            grddisplaymode.DataBind();
        }
    }

    protected void ChkDesig_v_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";

        GridViewRow gv2 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkDes_v = (CheckBoxList)gv2.FindControl("ChkDesig_v");

        TextBox txtDesig_v = (TextBox)gv2.FindControl("txtDesig_v");
        HiddenField hdnDesigId_v = (HiddenField)gv2.FindControl("hdnDesigId_v");
        txtDesig_v.Text = "";
        hdnDesigId_v.Value = "";

        if (chkDes_v.Items[0].Text == "ALL" && chkDes_v.Items[0].Selected == true)
        {
            for (int i = 0; i < chkDes_v.Items.Count; i++)
            {
                chkDes_v.Items[i].Selected = true;
            }
        }

        int countSelected = chkDes_v.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkDes_v.Items.Count - 1)
        {
            for (int i = 0; i < chkDes_v.Items.Count; i++)
            {
                chkDes_v.Items[i].Selected = false;
            }
        }

        for (int i = 0; i < chkDes_v.Items.Count; i++)
        {
            if (chkDes_v.Items[i].Selected)
            {
                if (chkDes_v.Items[i].Text != "ALL")
                {
                    name1 += chkDes_v.Items[i].Text + ",";
                    id1 += chkDes_v.Items[i].Text + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtDesig_v.Text = name1.TrimEnd(',');
        hdnDesigId_v.Value = id1.TrimEnd(',');
    }
    protected void ChkDesig_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";

        GridViewRow gv1 = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList chkDes = (CheckBoxList)gv1.FindControl("ChkDesig");

        TextBox txtDesig = (TextBox)gv1.FindControl("txtDesig");
        HiddenField hdnDesigId = (HiddenField)gv1.FindControl("hdnDesigId");
        txtDesig.Text = "";
        hdnDesigId.Value = "";

        if (chkDes.Items[0].Text == "ALL" && chkDes.Items[0].Selected == true)
        {
            for (int i = 0; i < chkDes.Items.Count; i++)
            {
                chkDes.Items[i].Selected = true;
            }
        }

        int countSelected = chkDes.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == chkDes.Items.Count - 1)
        {
            for (int i = 0; i < chkDes.Items.Count; i++)
            {
                chkDes.Items[i].Selected = false;
            }
        }

        for (int i = 0; i < chkDes.Items.Count; i++)
        {
            if (chkDes.Items[i].Selected)
            {
                if (chkDes.Items[i].Text != "ALL")
                {
                    name1 += chkDes.Items[i].Text + ",";
                    id1 += chkDes.Items[i].Text + ",";
                }
            }
        }

        if (name1 == "")
        {
            name1 = "----Select----";
        }

        txtDesig.Text = name1.TrimEnd(',');
        hdnDesigId.Value = id1.TrimEnd(',');
    }

    protected void grdentrymode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList ChkDesig = (CheckBoxList)e.Row.FindControl("ChkDesig");
            TextBox txtDesig = (TextBox)e.Row.FindControl("txtDesig");
            HiddenField hdnDesigId = (HiddenField)e.Row.FindControl("hdnDesigId");
            Designation des = new Designation();
            dsDesignation = des.getDesinationCode_ForDCRSetup(div_code);
            if (dsDesignation.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                desig_cd = string.Empty;


                for (int j = 0; j < dsDesignation.Tables[0].Rows.Count; j++)
                {
                    sDesig += dsDesignation.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + ",";
                }

                desigcd = sDesig.Split(',');

                foreach (string st_cd in desigcd)
                {
                    if (i == 0)
                    {
                        desig_cd = desig_cd + st_cd;
                    }
                    else
                    {
                        if (st_cd.Trim().Length > 0)
                        {
                            desig_cd = desig_cd + "," + st_cd;
                        }
                    }
                    i++;
                }
                Designation desig = new Designation();
                dsDesignation = desig.getDesignationAddChkBox(desig_cd);
                ChkDesig.DataTextField = "Designation_Short_Name";
                ChkDesig.DataValueField = "Designation_Code";
                ChkDesig.DataSource = dsDesignation;
                ChkDesig.DataBind();
            }

            txtDesig.Text = "----Select----";

            string[] Desination;
            if (Designation_Short_Name != "")
            {
                iIndex = -1;
                Desination = desig_cd.Split(',');
                foreach (string st in Desination)
                {
                    for (iIndex = 0; iIndex < ChkDesig.Items.Count; iIndex++)
                    {
                        if (st == ChkDesig.Items[iIndex].Value)
                        {
                            ChkDesig.Items[iIndex].Selected = true;
                        }
                    }
                }
            }

        }
    }

    protected void grddisplaymode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList ChkDesig_v = (CheckBoxList)e.Row.FindControl("ChkDesig_v");
            TextBox txtDesig_v = (TextBox)e.Row.FindControl("txtDesig_v");
            HiddenField hdnDesigId_v = (HiddenField)e.Row.FindControl("hdnDesigId_v");
            Designation des = new Designation();
            dsDesignation_v = des.getDesinationCode_ForDCRSetup(div_code);
            if (dsDesignation_v.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                desig_cd = string.Empty;


                for (int j = 0; j < dsDesignation_v.Tables[0].Rows.Count; j++)
                {
                    sDesig += dsDesignation_v.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + ",";
                }

                desigcd = sDesig.Split(',');

                foreach (string st_cd in desigcd)
                {
                    if (i == 0)
                    {
                        desig_cd = desig_cd + st_cd;
                    }
                    else
                    {
                        if (st_cd.Trim().Length > 0)
                        {
                            desig_cd = desig_cd + "," + st_cd;
                        }
                    }
                    i++;
                }
                Designation desig = new Designation();
                dsDesignation_v = desig.getDesignationAddChkBox(desig_cd);
                ChkDesig_v.DataTextField = "Designation_Short_Name";
                ChkDesig_v.DataValueField = "Designation_Code";
                ChkDesig_v.DataSource = dsDesignation_v;
                ChkDesig_v.DataBind();
            }

            txtDesig_v.Text = "----Select----";

            string[] Desination;
            if (Designation_Short_Name != "")
            {
                iIndex = -1;
                Desination = desig_cd.Split(',');
                foreach (string st in Desination)
                {
                    for (iIndex = 0; iIndex < ChkDesig_v.Items.Count; iIndex++)
                    {
                        if (st == ChkDesig_v.Items[iIndex].Value)
                        {
                            ChkDesig_v.Items[iIndex].Selected = true;
                        }
                    }
                }
            }

        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Session["click"] == null)
        {
            Label38.Visible = true;
            rdolock_yes.Visible = true;
            rdolock_no.Visible = true;
            //lbllock_mr.Visible = true;
            //txtlock_mr.Visible = true;
            //lbllock_week_holi.Visible = true;
            //txtlock_week_holi.Visible = true;
            lbllock_timelimit.Visible = true;
            txtlock_timelimit.Visible = true;
            btnlockdays.Visible = true;

            Session["click"] = "click2";

        }
        else
        {
            Label38.Visible = false;
            rdolock_yes.Visible = false;
            rdolock_no.Visible = false;
            //lbllock_mr.Visible = false;
            //txtlock_mr.Visible = false;
            //lbllock_week_holi.Visible = false;
            //txtlock_week_holi.Visible = false;
            lbllock_timelimit.Visible = false;
            txtlock_timelimit.Visible = false;
            btnlockdays.Visible = false;
            Session["click"] = null;

        }
    }

    private void FillProduct_Feedback()
    {
        AdminSetup adm = new AdminSetup();
        dsadm = adm.getprd_feedback(div_code);
        if (dsadm.Tables[0].Rows.Count > 0)
        {
            ddlprdfeed.DataTextField = "FeedBack_Name";
            ddlprdfeed.DataValueField = "FeedBack_Id";
            ddlprdfeed.DataSource = dsadm;
            ddlprdfeed.DataBind();
        }
    }
    protected void rdoprdfeedYes_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoprdfeedYes_No.SelectedValue == "1")
        {
            ddlprdfeed.Visible = true;
            FillProduct_Feedback();
        }
        else
        {
            ddlprdfeed.Visible = false;
        }
    }

    protected void rdoprd_priority_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoprd_priority.SelectedValue == "1")
        {
            Label27.Visible = true;
            txtprdrange.Visible = true;

        }
        else
        {
            Label27.Visible = false;
            txtprdrange.Visible = false;
        }
    }

    protected void btnlockdays_Click(object sender, EventArgs e)
    {
        string strQry = string.Empty;
        int iReturn = -1;
        DB_EReporting db = new DB_EReporting();
        if (rdolock_yes.Checked )
        {
            strQry = "update mas_salesforce set sf_short_name='" + txtNoDaysDly.Text + "' where Division_Code='" + div_code + ",'";
            iReturn = db.ExecQry(strQry);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Saved Successfully');</script>");
            }

        }
        else if ( chkFFWiseDly.Checked)
        {           
            strQry = "update mas_salesforce set sf_short_name='" + txtFFWiseDly.Text + "' where Division_Code='" + div_code + ",'";
            iReturn = db.ExecQry(strQry);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Saved Successfully');</script>");
            }

        }
    }

}
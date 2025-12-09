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

public partial class MasterFiles_Options_Mob_App_Setting : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsadmin = new DataSet();
    DataSet dsadm = new DataSet();
    DataSet dsAdminSetup = null;
    DataSet dsSalesForce = new DataSet();
    DataSet dsadmi = new DataSet();
    int iIndex = -1;
    string chkhaf = string.Empty;

    int check = 0;
    int geo_code = 0;
    int geo_fencing = 0;
    private int Fencingche;
    private int Fencingstock;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            Fillsalesforce();
            FillHalfDay_Work();
            FillHalfDay_Work_MGR();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;

            app_record();
            appset_record();

            AdminSetup dv = new AdminSetup();
            dsadmin = dv.getMobApp_Setting(div_code);

            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                {
                    rdomandt.SelectedValue = "0";
                }
                else
                {
                    rdomandt.SelectedValue = "1";
                }

               
                 Radio90.SelectedValue = "1";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString() == "0")
                {
                    Radio90.SelectedValue = "0";
                }
                else
                {
                    Radio90.SelectedValue = "1";
                }

                Radio91.SelectedValue = "1";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString() == "0")
                {
                    Radio91.SelectedValue = "0";
                }
                else
                {
                    Radio91.SelectedValue = "1";
                }
                Radio92.SelectedValue = "1";
                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString() == "0")
                {
                    Radio92.SelectedValue = "0";
                }
                else
                {
                    Radio92.SelectedValue = "1";
                }

                 if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
                {
                    rdogeo.SelectedValue = "1";
                }
                else
                {
                    rdogeo.SelectedValue = "0";
                }
                txtcover.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                txtvisit1.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtvisit2.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                txtvisit3.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                txtvisit4.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString() == "0")
                {
                    rdoprd_entry_doc.SelectedValue = "0";
                }
                else
                {
                    rdoprd_entry_doc.SelectedValue = "1";
                }
                txtRx_Cap_doc.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                txtSamQty_Cap_doc.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString() == "0")
                {
                    rdoinput_Ent_doc.SelectedValue = "0";
                }
                else
                {
                    rdoinput_Ent_doc.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString() == "0")
                {
                    rdoNeed_chem.SelectedValue = "0";
                }
                else
                {
                    rdoNeed_chem.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString() == "0")
                {
                    rdoProduct_entr_chem.SelectedValue = "0";
                }
                else
                {
                    rdoProduct_entr_chem.SelectedValue = "1";
                }

                txtqty_Cap_chem.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString() == "0")
                {
                    rdoinpu_entry_chem.SelectedValue = "0";
                }
                else
                {
                    rdoinpu_entry_chem.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString() == "0")
                {
                    rdoNeed_stock.SelectedValue = "0";
                }
                else
                {
                    rdoNeed_stock.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString() == "0")
                {
                    rdoprdentry_stock.SelectedValue = "0";
                }
                else
                {
                    rdoprdentry_stock.SelectedValue = "1";
                }

                txtQty_Cap_stock.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString() == "0")
                {
                    rdoinpu_entry_stock.SelectedValue = "0";
                }
                else
                {
                    rdoinpu_entry_stock.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString() == "0")
                {
                    rdoneed_unlistDr.SelectedValue = "0";
                }
                else
                {
                    rdoneed_unlistDr.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(20).ToString() == "0")
                {
                    rdoprdentry_unlistDr.SelectedValue = "0";
                }
                else
                {
                    rdoprdentry_unlistDr.SelectedValue = "1";
                }

                txtRxQty_Cap_unlistDr.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();

                txtSamQty_Cap_unlistDr.Text = dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString() == "0")
                {
                    rdoinpuEnt_Need_unlistDr.SelectedValue = "0";
                }
                else
                {
                    rdoinpuEnt_Need_unlistDr.SelectedValue = "1";
                }

                if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString() == "0")
                {
                    rdodevice.SelectedValue = "0";
                }
                else
                {
                    rdodevice.SelectedValue = "1";
                }
            }

            for (int i = 0; i < chkhaf_work.Items.Count; i++)
            {
               
                AdminSetup adm = new AdminSetup();
                dsadm = dv.getMobApp_Setting_halfday(div_code, chkhaf_work.Items[i].Value);

                if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                {
                    chkhaf_work.Items[i].Selected = true;
                }
                else
                {
                    chkhaf_work.Items[i].Selected = false;
                }

            }

            for (int i = 0; i < chkhaf_work_mgr.Items.Count; i++)
            {

                AdminSetup adm = new AdminSetup();
                DataSet dsAdmm = new DataSet();
                string strQry = string.Empty;
                DataSet dsAdmin = null;
                DB_EReporting db_ER = new DB_EReporting();

                strQry = "  select Hlfdy_flag,WorkType_Code_M from Mas_WorkType_Mgr where Division_Code='" + div_code + "' and WorkType_Code_M='" + chkhaf_work_mgr.Items[i].Value + "' ";
                dsAdmm = db_ER.Exec_DataSet(strQry);

                //dsAdmm = dv.getMobApp_Setting_halfday(div_code, chkhaf_work_mgr.Items[i].Value);

                if (dsAdmm.Tables[0].Rows.Count > 0)
                {

                    if (dsAdmm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
                    {
                        chkhaf_work_mgr.Items[i].Selected = true;
                    }
                    else
                    {
                        chkhaf_work_mgr.Items[i].Selected = false;
                    }
                }

            }



            foreach (GridViewRow gridRow in grdgps.Rows)
            {
                CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
                bool bCheck = chkId.Checked;
                Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
                string sf_Code = lblSF_Code.Text.ToString();
                CheckBox chkfencing = (CheckBox)gridRow.Cells[0].FindControl("chkfencing");


                CheckBox GeoFencingche = (CheckBox)gridRow.Cells[4].FindControl("chkfencingche");
                CheckBox GeoFencingstock = (CheckBox)gridRow.Cells[5].FindControl("chkfencingstock");

                if (sf_Code != "")
                {

                    AdminSetup ad = new AdminSetup();
                    dsadmi = dv.getMobApp_geo(sf_Code);

                    if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
                    {

                        chkId.Checked = true;

                    }
                    else
                    {
                        chkId.Checked = false;
                    }
                    if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "1")
                    {

                        chkfencing.Checked = true;

                    }
                    else
                    {
                        chkfencing.Checked = false;
                    }

                    if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() == "1")
                    {

                        GeoFencingche.Checked = true;

                    }
                    else
                    {
                        GeoFencingche.Checked = false;
                    }
                    if (dsadmi.Tables[0].Rows[0].ItemArray.GetValue(4).ToString() == "1")
                    {
                        GeoFencingstock.Checked = true;
                    }
                    else
                    {
                        GeoFencingstock.Checked = false;
                    }
                }
            }
            
        }
      
    }

    private void appset_record()
    {
        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.getting_mob_app_record2(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            Radio31.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Radio32.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txt_srtdate.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txt_enddate.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            Radio33.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            Radio34.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Radio35.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
        
            Radio37.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            Radio38.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            Radio39.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();

            Radio40.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            Radio41.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            Radio42.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            Radio43.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            Radio44.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
      
            Radio46.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            Radio47.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
            Radio48.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
            Radio49.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();

            Radio50.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
            Radio51.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(22).ToString();
            Radio52.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();
            Radio53.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();
            Radio54.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();
            Radio55.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();
            Radio56.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();
            Radio57.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();
            Radio58.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(29).ToString();
            Radio59.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(30).ToString();
            Radio60.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(31).ToString();
            Radio61.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(32).ToString();
            Radio62.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(33).ToString();
            Radio63.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(34).ToString();
            Radio64.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(35).ToString();
            Radio65.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(36).ToString();
            Radio66.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(37).ToString();
            Radio67.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(38).ToString();
            Radio68.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(39).ToString();
            Radio69.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(40).ToString();
            Radio70.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(41).ToString();
            Radio71.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(42).ToString();
            Radio72.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(43).ToString();
            Radio73.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(44).ToString();
            Radio75.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(45).ToString();
            Radio76.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(46).ToString();
            Radio77.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(47).ToString();
            Radio78.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(48).ToString();
            Radio79.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(49).ToString();
            Radio80.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(50).ToString();
            Radio81.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(51).ToString();
            Radio82.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(52).ToString();
            Radio83.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(53).ToString();
            Radio84.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(54).ToString();
            Radio85.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(55).ToString();
            Radio86.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(56).ToString();
            Radio87.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(57).ToString();
            Radio88.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(58).ToString();
            Radio89.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(63).ToString();
            txtDpc.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(59).ToString();
            txtCPC.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(60).ToString();
            txtSPC.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(61).ToString();
            txtUPC.Text = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(62).ToString();
            Radio92.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(64).ToString();
            RadioBtnList1.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(65).ToString();
            RadioBtnList2.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(66).ToString();
            RadioBtnList3.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(67).ToString();
        }
    }

     private void app_record()
    {
        AdminSetup aa = new AdminSetup();
        dsAdminSetup = aa.getting_mob_app_record(div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            Radio.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Radio2.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            Radio3.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Radio4.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            Radio5.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            Radio6.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Radio7.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            Radio8.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            Radio9.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            Radio10.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            Radio17.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            Radio11.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            Radio14.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
            Radio15.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
            Radio16.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
            Radio90.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            Radio91.SelectedValue = dsAdminSetup.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
        }
    }

    private void FillHalfDay_Work()
    {
        AdminSetup adm = new AdminSetup();

        dsadmin = adm.gethalf_Daywrk(div_code);

        chkhaf_work.DataSource = dsadmin;
        chkhaf_work.DataTextField = "Worktype_Name_B";
        chkhaf_work.DataValueField = "WorkType_Code_B";
        chkhaf_work.DataBind();
    }

    private void FillHalfDay_Work_MGR()
    {
        AdminSetup adm = new AdminSetup();
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsAd = new DataSet();
        string strQry = string.Empty;
        strQry = " select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr " +
                  " where Division_Code='" + div_code + "' and active_flag=0 ";
        dsAd = db_ER.Exec_DataSet(strQry);
        if (dsAd.Tables[0].Rows.Count > 0)
        {
            chkhaf_work_mgr.DataSource = dsAd;
            chkhaf_work_mgr.DataTextField = "Worktype_Name_M";
            chkhaf_work_mgr.DataValueField = "WorkType_Code_M";
            chkhaf_work_mgr.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
            AdminSetup admin = new AdminSetup();
            string strQry = string.Empty;
            int iReturn = admin.RecordUpdate_MobApp(Convert.ToInt16(rdomandt.SelectedValue.ToString()), Convert.ToInt16(rdogeo.SelectedValue.ToString()),
            float.Parse(txtcover.Text.ToString()), txtvisit1.Text.ToString(),
            txtvisit2.Text.ToString(), txtvisit3.Text.ToString(),
            txtvisit4.Text.ToString(), Convert.ToInt16(rdoprd_entry_doc.SelectedValue.ToString()),
            Convert.ToInt16(rdoinput_Ent_doc.SelectedValue.ToString()), Convert.ToInt16(rdoNeed_chem.SelectedValue.ToString()), 
            Convert.ToInt16(rdoProduct_entr_chem.SelectedValue.ToString()), txtqty_Cap_chem.Text.ToString(), 
            Convert.ToInt16(rdoinpu_entry_chem.SelectedValue.ToString()), Convert.ToInt16(rdoNeed_stock.SelectedValue.ToString()), 
            Convert.ToInt16(rdoprdentry_stock.SelectedValue.ToString()), txtQty_Cap_stock.Text.ToString(), 
            Convert.ToInt16(rdoinpu_entry_stock.SelectedValue.ToString()), Convert.ToInt16(rdoneed_unlistDr.SelectedValue.ToString()),
            Convert.ToInt16(rdoprdentry_unlistDr.SelectedValue.ToString()),  txtRxQty_Cap_unlistDr.Text.ToString(), 
            txtSamQty_Cap_unlistDr.Text.ToString(),   Convert.ToInt16(rdoinpuEnt_Need_unlistDr.SelectedValue.ToString()), 
            div_code, Convert.ToInt16(rdodevice.SelectedValue),
            Convert.ToInt16(Radio.SelectedValue.ToString()), Convert.ToInt16(Radio2.SelectedValue.ToString()),
            Convert.ToInt16(Radio3.SelectedValue.ToString()),
            Convert.ToInt16(Radio4.SelectedValue.ToString()), Convert.ToInt16(Radio5.SelectedValue.ToString()), 
            Convert.ToInt16(Radio6.SelectedValue.ToString()),Convert.ToInt16(Radio7.SelectedValue.ToString()), 
            Convert.ToInt16(Radio8.SelectedValue.ToString()),Convert.ToInt16(Radio9.SelectedValue.ToString()), 
            Convert.ToInt16(Radio10.SelectedValue.ToString()),
            txtRx_Cap_doc.Text.ToString(), txtSamQty_Cap_doc.Text.ToString(),
            Convert.ToInt16(Radio11.SelectedValue.ToString()),
            Convert.ToInt16(Radio14.SelectedValue.ToString()), Convert.ToInt16(Radio15.SelectedValue.ToString()),
            Convert.ToInt16(Radio16.SelectedValue.ToString()), 
            Convert.ToInt16(Radio31.SelectedValue.ToString()),
            Convert.ToInt16(Radio32.SelectedValue.ToString()), txt_srtdate.Text,
            txt_enddate.Text,Convert.ToInt16(Radio33.SelectedValue.ToString()), 
            Convert.ToInt16(Radio34.SelectedValue.ToString()),Convert.ToInt16(Radio35.SelectedValue.ToString()),
            Convert.ToInt16(Radio37.SelectedValue.ToString()), Convert.ToInt16(Radio38.SelectedValue.ToString()),
            Convert.ToInt16(Radio39.SelectedValue.ToString()), Convert.ToInt16(Radio40.SelectedValue.ToString()),
            Convert.ToInt16(Radio41.SelectedValue.ToString()), Convert.ToInt16(Radio42.SelectedValue.ToString()),
            Convert.ToInt16(Radio43.SelectedValue.ToString()), Convert.ToInt16(Radio44.SelectedValue.ToString()),
            Convert.ToInt16(Radio46.SelectedValue.ToString()),
            txtDpc.Text,txtCPC.Text,txtSPC.Text,txtUPC.Text,
            Convert.ToInt16(Radio85.SelectedValue.ToString()), Convert.ToInt16(Radio86.SelectedValue.ToString()),
            Convert.ToInt16(Radio87.SelectedValue.ToString()), Convert.ToInt16(Radio88.SelectedValue.ToString()),
            Convert.ToInt16(Radio81.SelectedValue.ToString()), Convert.ToInt16(Radio82.SelectedValue.ToString()),
            Convert.ToInt16(Radio83.SelectedValue.ToString()), Convert.ToInt16(Radio84.SelectedValue.ToString()),
             Convert.ToInt16(Radio75.SelectedValue.ToString()), Convert.ToInt16(Radio76.SelectedValue.ToString()),
             Convert.ToInt16(Radio77.SelectedValue.ToString()), Convert.ToInt16(Radio78.SelectedValue.ToString()),
             Convert.ToInt16(Radio79.SelectedValue.ToString()), Convert.ToInt16(Radio80.SelectedValue.ToString()),
              Convert.ToInt16(Radio47.SelectedValue.ToString()), Convert.ToInt16(Radio48.SelectedValue.ToString()),
              Convert.ToInt16(Radio49.SelectedValue.ToString()),
            Convert.ToInt16(Radio50.SelectedValue.ToString()), Convert.ToInt16(Radio51.SelectedValue.ToString()), 
            Convert.ToInt16(Radio52.SelectedValue.ToString()), Convert.ToInt16(Radio53.SelectedValue.ToString()),
            Convert.ToInt16(Radio54.SelectedValue.ToString()),  Convert.ToInt16(Radio55.SelectedValue.ToString()),
            Convert.ToInt16(Radio56.SelectedValue.ToString()), Convert.ToInt16(Radio57.SelectedValue.ToString()),
            Convert.ToInt16(Radio58.SelectedValue.ToString()), Convert.ToInt16(Radio59.SelectedValue.ToString()), 
            Convert.ToInt16(Radio60.SelectedValue.ToString()), Convert.ToInt16(Radio61.SelectedValue.ToString()),
            Convert.ToInt16(Radio62.SelectedValue.ToString()), Convert.ToInt16(Radio63.SelectedValue.ToString()),
            Convert.ToInt16(Radio64.SelectedValue.ToString()), Convert.ToInt16(Radio65.SelectedValue.ToString()),
            Convert.ToInt16(Radio66.SelectedValue.ToString()), Convert.ToInt16(Radio67.SelectedValue.ToString()), 
            Convert.ToInt16(Radio68.SelectedValue.ToString()), Convert.ToInt16(Radio69.SelectedValue.ToString()), 
            Convert.ToInt16(Radio70.SelectedValue.ToString()), Convert.ToInt16(Radio71.SelectedValue.ToString()), 
            Convert.ToInt16(Radio72.SelectedValue.ToString()),
            Convert.ToInt16(Radio73.SelectedValue.ToString()),
            Convert.ToInt16(Radio90.SelectedValue.ToString()),
           Convert.ToInt16(Radio91.SelectedValue.ToString()), Convert.ToInt16(Radio17.SelectedValue.ToString()),
           Convert.ToInt16(Radio89.SelectedValue.ToString()), Convert.ToInt16(Radio92.SelectedValue.ToString()),
           Convert.ToInt16(RadioBtnList1.SelectedValue.ToString()), Convert.ToInt16(RadioBtnList2.SelectedValue.ToString()),
           Convert.ToInt16(RadioBtnList3.SelectedValue.ToString())
              );


        for (int i = 0; i < chkhaf_work.Items.Count; i++)
        {
            if (chkhaf_work.Items[i].Selected)
            {

                check = 1;
                AdminSetup dv = new AdminSetup();
                iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work.Items[i].Value, div_code, check);
            }
            else
            {
                check = 0;
                AdminSetup dv = new AdminSetup();
                iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work.Items[i].Value, div_code, check);
            }
        }

        for (int i = 0; i < chkhaf_work_mgr.Items.Count; i++)
        {
            if (chkhaf_work_mgr.Items[i].Selected)
            {

                check = 1;

                AdminSetup dv = new AdminSetup();
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_Mgr set Hlfdy_flag='" + check + "' " +
                         " where WorkType_Code_M='" + chkhaf_work_mgr.Items[i].Value + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

                // iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work_mgr.Items[i].Value, div_code, check);
            }
            else
            {
                check = 0;
                AdminSetup dv = new AdminSetup();
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_Mgr set Hlfdy_flag='" + check + "' " +
                         " where WorkType_Code_M='" + chkhaf_work_mgr.Items[i].Value + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

                // iReturn = dv.RecordUpdate_Forhalfday(chkhaf_work_mgr.Items[i].Value, div_code, check);
            }

        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    private void Fillsalesforce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, "admin");

      
        //dsSalesForce.Tables[0].Rows[0].Delete();
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
            grdgps.Visible = true;
            grdgps.DataSource = dsSalesForce;
            grdgps.DataBind();
        }
     
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
      
        int iReturn = -1;

        foreach (GridViewRow gridRow in grdgps.Rows)
        {
            CheckBox chkId = (CheckBox)gridRow.Cells[0].FindControl("chkId");
            bool bCheck = chkId.Checked;
            Label lblSF_Code = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string sf_Code = lblSF_Code.Text.ToString();
            CheckBox chkfencing = (CheckBox)gridRow.Cells[3].FindControl("chkfencing");

            CheckBox GeoFencingche = (CheckBox)gridRow.Cells[4].FindControl("chkfencingche");
            CheckBox GeoFencingstock = (CheckBox)gridRow.Cells[5].FindControl("chkfencingstock");

            AdminSetup ad = new AdminSetup();
            if (chkId.Checked)
            {

                geo_code = 0;
            }
            else
            {
                geo_code = 1;
            }

            if (chkfencing.Checked)
            {
                geo_fencing = 1;
            }
            else
            {
                geo_fencing = 0;
            }
            if (GeoFencingche.Checked)
            {
                Fencingche = 1;
            }
            else
            {
                Fencingche = 0;
            }

            if (GeoFencingstock.Checked)
            {
                Fencingstock = 1;
            }
            else
            {
                Fencingstock = 0;
            }

            iReturn = ad.RecordUpdate_geosf_code(sf_Code, geo_code, geo_fencing, Fencingche, Fencingstock);


          
        }

        if (iReturn != -1)
        {
            //  menu1.Status = "Chemists De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
           
   
        }
        //pnlpopup.Style.Add("display", "none");
        //pnlpopup.Style.Add("visibility", "hidden");
    }
    protected void linkgps_Click(object sender, EventArgs e)
    {
        pnlpopup.Style.Add("display", "block");
        pnlpopup.Style.Add("visibility", "visible");
    }

   
}
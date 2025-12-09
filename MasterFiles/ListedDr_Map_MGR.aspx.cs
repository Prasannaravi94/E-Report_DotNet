using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;


public partial class MasterFiles_ListedDr_Map_MGR : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsProdDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsDocSubCat = null;
    string state_code = string.Empty;
    DataSet dsCatgType = null;
    string Listed_DR_Code = string.Empty;
    string doctype = string.Empty;
    string chkCampaign = string.Empty;
    string Doc_SubCatCode = string.Empty;
    int iIndex = -1;
    string sCmd = string.Empty;
    int iReturn = -1;
    int time;
    DataSet dsSalesForce = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
         sf_code = Session["sf_code"].ToString();
        //if (Session["sf_type"].ToString() == "1")
        //{
        //    sf_code = Session["sf_code"].ToString();
        //}
        //else
        //{
        //    sf_code = Request.QueryString["sf_code"].ToString();
        //}


        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            ////// menu1.FindControl("btnBack").Visible = false;

            DataSet dsPrd = new DataSet();
            ListedDR dr = new ListedDR();

            dsPrd = dr.getPrd_Priority(div_code);

            if (dsPrd.Tables[0].Rows.Count > 0)
            {
                hndpri_yesno.Value = dsPrd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                hndprio.Value = dsPrd.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

            }


            if (Session["sf_type"].ToString() == "2")
            {
                // sfCode = Session["sf_code"].ToString();
                DataList1.BackColor = System.Drawing.Color.White;

                UserControl_MGR_Menu Usc_MR =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                //    // usc_MR.FindControl("btnBack").Visible = false;

                fill_baselevel();
               FillDoc();

            }
            else
            {
                //sf_code = Session["sf_code"].ToString();
                //if (Session["sf_code_Temp"] != null)
                //{
                //    sf_code = Session["sf_code_Temp"].ToString();
                //}
                //DataList1.BackColor = System.Drawing.Color.White;
                //UserControl_MenuUserControl Usc_Menu =
                //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                //Divid.Controls.Add(Usc_Menu);

                //Usc_Menu.Title = this.Page.Title;
                //Session["backurl"] = "LstDoctorList.aspx";
                ////  Usc_// menu.FindControl("btnBack").Visible = false;
                //FillDoc();

                //  getWorkName();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu Usc_MR1 =
                        (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MR1);
                Usc_MR1.Title = this.Page.Title;

            }
            else
            {
               // UserControl_MenuUserControl Usc_Menu =
               //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
               // Divid.Controls.Add(Usc_Menu);
               // Session["backurl"] = "LstDoctorList.aspx";

            }
        }
    }

    private void FillDoc()
    {
        lblSelect.Visible = true;
        lblSelect.Text = "Select Listed Doctor Name";
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDr_for_Mapp(ddlMR.SelectedValue, div_code);
        ViewState["DrCode"] = dsListedDR;
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {


            ddldr.DataTextField = "ListedDr_Name";
            ddldr.DataValueField = "ListedDrCode";
            ddldr.DataSource = dsListedDR;
            ddldr.DataBind();

        }
        else
        {
            ddldr.DataSource = dsListedDR;
            ddldr.DataBind();

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        //chkprd.Visible = true;
        lblSelect.Visible = true;

        if (hndpri_yesno.Value == "1")
        {

            DataList1.Visible = true;
            DataList2.Visible = false;
        }
        else
        {
            DataList2.Visible = true;
            DataList1.Visible = false;
        }
        FillPrd();
    }

    private void FillPrd()
    {
        //btnGo.Enabled = false;
        lblSelect.Text = "Select the Product";
        btnSubmit.Visible = true;
        Product spec = new Product();
        DataSet dsChkSp = new DataSet();
        dsChkSp = spec.getPrd_For_Mapp(div_code, ddlMR.SelectedValue);
        if (hndpri_yesno.Value == "1")
        {
            if (dsChkSp.Tables[0].Rows.Count > 0)
            {
                //btnSave.Visible = true;
                DataList1.Visible = true;
                DataList2.Visible = false;
                DataList1.DataSource = dsChkSp;
                DataList1.DataBind();
            }
            else
            {
                DataList1.DataSource = dsChkSp;
                DataList1.DataBind();
            }
        }
        else
        {
            if (dsChkSp.Tables[0].Rows.Count > 0)
            {
                //btnSave.Visible = true;
                DataList1.Visible = false;
                DataList2.Visible = true;
                DataList2.DataSource = dsChkSp;
                DataList2.DataBind();
            }
            else
            {
                DataList2.DataSource = dsChkSp;
                DataList2.DataBind();
            }
        }



        if (hndpri_yesno.Value == "1")
        {

            foreach (DataListItem cb in DataList1.Items)
            {
                DropDownList ddlPriority = (DropDownList)cb.FindControl("ddlPriority");

                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(int));
                dt.Columns.Add("Text", typeof(string));
                //for (int i = 0; i < dsProdDR.Tables[0].Rows.Count; i++)
                for (int i = 1; i <= Convert.ToInt16(hndprio.Value); i++)
                {
                    dt.Rows.Add(i, i);
                }
                ddlPriority.DataValueField = "Value";
                ddlPriority.DataTextField = "Text";
                ddlPriority.DataSource = dt;
                ddlPriority.DataBind();
                ddlPriority.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }
        else
        {

        }

        string str_CateCode = "";
        Product prd = new Product();
        dsProdDR = prd.getprdfor_Mappdr(ddldr.SelectedValue);

        if (dsProdDR.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < dsProdDR.Tables[0].Rows.Count; i++)
            {
                str_CateCode = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                if (hndpri_yesno.Value == "1")
                {


                    foreach (DataListItem grid in DataList1.Items)
                    {
                        Label chk = (Label)grid.FindControl("lblPrdCode");
                        DropDownList ddlPriority = (DropDownList)grid.FindControl("ddlPriority");

                        if (hndpri_yesno.Value != "1")
                        {
                            ddlPriority.Visible = false;
                        }

                        string[] Salesforce;
                        if (str_CateCode != "")
                        {
                            iIndex = -1;
                            Salesforce = str_CateCode.Split(',');
                            foreach (string sf in Salesforce)
                            {

                                CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");
                                Label hf = (Label)grid.FindControl("lblPrdCode");

                                if (sf == hf.Text)
                                {
                                    ddlPriority.SelectedValue = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                                    chkCatName.Checked = true;
                                    chkCatName.Attributes.Add("style", "Color: Red; font-weight:Bold; font-size:16px; ");
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataListItem grid in DataList2.Items)
                    {
                        Label chk = (Label)grid.FindControl("lblPrdCode");

                        //DropDownList ddlPriority = (DropDownList)grid.FindControl("ddlPriority");

                        string[] Salesforce;
                        if (str_CateCode != "")
                        {
                            iIndex = -1;
                            Salesforce = str_CateCode.Split(',');
                            foreach (string sf in Salesforce)
                            {

                                CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");
                                Label hf = (Label)grid.FindControl("lblPrdCode");

                                if (sf == hf.Text)
                                {

                                    //ddlPriority.SelectedValue = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                                    chkCatName.Checked = true;
                                    chkCatName.Attributes.Add("style", "Color: Red; font-weight:Bold; font-size:16px; ");
                                }
                            }
                        }
                    }
                }
            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(time);

        //string strPrd = "";
        //string srtpd = "";


        //foreach (DataListItem grid in DataList1.Items)
        //{
        //    Label chk = (Label)grid.FindControl("lblPrdCode");
        //    CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");

        //    if (chkCatName.Checked == true)
        //    {
        //        strPrd += chk.Text + ",";
        //    }
        //}


        //if (strPrd != "")
        //{

        //    strPrd = strPrd.Remove(strPrd.Length - 1);

        //    //string[] Prd = strPrd_name.Split(',');
        //    string[] Prd_code = strPrd.Split(',');

        //    foreach (string Prd_Cod in Prd_code)
        //    {
        //        ListedDR lst = new ListedDR();
        //        int iReturn = lst.RecordAdd_ProductMap_New(ddldr.SelectedValue, Prd_Cod, ddldr.SelectedItem.Text, sf_code, div_code);
        //        if (iReturn > 0)
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
        //            DataList1.Visible = false;
        //            btnSubmit.Visible = false;
        //            lblSelect.Text = "Select Listed Doctor Name";
        //        }
        //    }
        //}
        //foreach (DataListItem grid in DataList1.Items)
        //{
        //    Label chk = (Label)grid.FindControl("lblPrdCode");
        //    CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");

        //    if (!chkCatName.Checked)
        //    {
        //        if (chkCatName.Checked == false)
        //        {
        //            srtpd += chk.Text + ",";
        //        }
        //    }
        //}


        //if (srtpd != "")
        //{
        //    srtpd = srtpd.Remove(srtpd.Length - 1);

        //    string[] Prd_cod = srtpd.Split(',');

        //    foreach (string Prd_Co in Prd_cod)
        //    {
        //        ListedDR lstdr = new ListedDR();
        //        int iReturn = lstdr.Delete_ProductMap(ddldr.SelectedValue, Prd_Co, sf_code, div_code);
        //        if (iReturn == -1)
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
        //            DataList1.Visible = false;
        //            btnSubmit.Visible = false;
        //            lblSelect.Text = "Select Listed Doctor Name";
        //        }
        //    }


        //}




        if (hndpri_yesno.Value == "1")
        {
            string Priority = string.Empty;
            foreach (DataListItem grid in DataList1.Items)
            {
                DropDownList prior = (DropDownList)grid.FindControl("ddlPriority");
                Priority = prior.SelectedValue;

                //if (Priority != "0")
                //{
                //    value += Priority + ',';
                //}

                //if (value.Contains(Priority))
                //{

                //}

                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("lblPrdCode");
                chkCampaign = "";


                if (chk.Checked)
                {
                    // str = lblcode.Text;
                    //chkCampaign = chkCampaign + chk.Text + ",";
                    chkCampaign = chkCampaign + hf.Text + ",";

                    string[] strProductSplit = chkCampaign.Split(',');
                    foreach (string strprod in strProductSplit)
                    {
                        if (strprod != "")
                        {
                            if (Priority == "0")
                            {
                                Priority = null;
                            }
                            ListedDR lst = new ListedDR();
                            int iReturn = lst.RecordAdd_ProductMap_New(ddldr.SelectedValue, strprod, ddldr.SelectedItem.Text, ddlMR.SelectedValue, div_code, Priority);
                            if (iReturn > 0)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Mapped Successfully');</script>");
                                DataList1.Visible = false;
                                DataList2.Visible = false;
                                btnSubmit.Visible = false;
                                lblSelect.Text = "Select Listed Doctor Name";

                            }
                        }
                    }

                }
                else
                {
                    dsListedDR = (DataSet)ViewState["DrCode"];
                    foreach (DataRow drFF in dsListedDR.Tables[0].Rows)
                    {
                        if (drFF["ListedDrCode"].ToString() == ddldr.SelectedValue)
                        {


                            chkCampaign = chkCampaign + hf.Text + ",";

                            string[] strProductSplit = chkCampaign.Split(',');
                            foreach (string strprod in strProductSplit)
                            {
                                if (strprod != "")
                                {
                                    ListedDR lstdr = new ListedDR();
                                    int iReturn = lstdr.Delete_ProductMap(ddldr.SelectedValue, strprod, ddlMR.SelectedValue, div_code);
                                }
                            }
                        }
                    }
                }

            }
        }

        else
        {
            string Priority = string.Empty;
            foreach (DataListItem grid in DataList2.Items)
            {
                //DropDownList prior = (DropDownList)grid.FindControl("ddlPriority");
                //Priority = prior.SelectedValue;

                //if (Priority != "0")
                //{
                //    value += Priority + ',';
                //}

                //if (value.Contains(Priority))
                //{

                //}

                CheckBox chk = (CheckBox)grid.FindControl("chkCatName");
                Label hf = (Label)grid.FindControl("lblPrdCode");
                chkCampaign = "";


                if (chk.Checked)
                {
                    // str = lblcode.Text;
                    //chkCampaign = chkCampaign + chk.Text + ",";
                    chkCampaign = chkCampaign + hf.Text + ",";

                    string[] strProductSplit = chkCampaign.Split(',');
                    foreach (string strprod in strProductSplit)
                    {
                        if (strprod != "")
                        {
                            if (Priority == "0")
                            {
                                Priority = null;
                            }
                            ListedDR lst = new ListedDR();
                            int iReturn = lst.RecordAdd_ProductMap_New(ddldr.SelectedValue, strprod, ddldr.SelectedItem.Text, ddlMR.SelectedValue, div_code, Priority);
                            if (iReturn > 0)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Mapped Successfully');</script>");
                                DataList1.Visible = false;
                                DataList2.Visible = false;
                                btnSubmit.Visible = false;
                                lblSelect.Text = "Select Listed Doctor Name";

                            }
                        }
                    }

                }
                else
                {
                    dsListedDR = (DataSet)ViewState["DrCode"];
                    foreach (DataRow drFF in dsListedDR.Tables[0].Rows)
                    {
                        if (drFF["ListedDrCode"].ToString() == ddldr.SelectedValue)
                        {


                            chkCampaign = chkCampaign + hf.Text + ",";

                            string[] strProductSplit = chkCampaign.Split(',');
                            foreach (string strprod in strProductSplit)
                            {
                                if (strprod != "")
                                {
                                    ListedDR lstdr = new ListedDR();
                                    int iReturn = lstdr.Delete_ProductMap(ddldr.SelectedValue, strprod, ddlMR.SelectedValue, div_code);
                                }
                            }
                        }
                    }
                }

            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddldr.SelectedValue = "0";
        FillDoc();
        DataList2.Visible = false;
        DataList1.Visible = false;
        btnSubmit.Visible = false;
        lblSelect.Text = "Select Listed Doctor Name";
    }

    private void fill_baselevel()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
         
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
           // ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    protected void ddlMR_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDoc();
        DataList1.Visible = false;
        DataList2.Visible = false;
        btnSubmit.Visible = false;
    }
    //protected void btnGoo_Click(object sender, EventArgs e)
    //{
    //    lblDr.Visible = true;
    //    txtNew.Visible = true;
    //    ddldr.Visible = true;
    //    btnGo.Visible = true;
    //    btnclr.Visible = true;
    //    FillDoc();
    //}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
using System.Security.Cryptography;
using System.Drawing.Imaging;

public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_New : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    DataSet dsSalesForce = null;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string sCmd = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    string doc_code = string.Empty;
    string Territory_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    string sf_code = string.Empty;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strDeact = string.Empty;
    string strView = string.Empty;
    string ClsSName = string.Empty;
    string QuaSName = string.Empty;
    string CatSName = string.Empty;
    string SpecSName = string.Empty;

    string Sf_Code_1 = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

       // Session["sf_code"] = "";

        if (Session["SF_Code_N"] != null && Session["SF_Code_N"].ToString() != "")
        {
            Sf_Code_1 = Session["SF_Code_N"].ToString();
            //Session["S_Code"] = Sf_Code_1;
        }
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
            Session["SF_Code_N"] = sfCode;                     

        }
        if (!Page.IsPostBack)
        {
          //  menu1.Title = this.Page.Title;
          //  menu1.FindControl("btnBack").Visible = false;
          //  getddlSF_Code();

            Session["GetCmdArgChar"] = "All";

            Session["GetcmdArgChar_Chemist"] = "All";

          //  FillDoc_Alpha();

            tblSearch.Visible = false;

            tblSr_Chemist.Visible = false;


            FillYear();
            FillMR();
            Get_CrmMgr_Status();
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = true;
            c1.Title = this.Page.Title;

            getddlSF_Code();

            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = true;
            c1.Title = this.Page.Title;
            Session["backurl"] = "Doctor_Service_CRM_New.aspx";

            //string SfCode = Request.QueryString["Sf_Code"];

            //if (SfCode == null)
            //{
            //    //Sf_Code_1 = Session["SF_Code_N"].ToString();
            //    Response.Redirect("");
            //}
            //else
            //{
                
            //}

            
           // Fill_MGR_Rpt();
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = true;
            c1.Title = this.Page.Title;
        }

      

    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet dsSalesforce;

        //if (Session["SF_Code_N"] != null && Session["SF_Code_N"].ToString() != "")
        //{
        //    sfCode = Session["SF_Code_N"].ToString();
        //}
        //else
        //{
        //    sfCode = Session["sf_code"].ToString();
        //}

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sfCode);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesforce = sf.SalesForceList_New_GetMr(div_code, sfCode);
            //dsSalesforce = sf.SalesForceList_New(div_code, sfCode);

        }
        else
        {
            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMR(div_code, sfCode, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesforce = dsmgrsf;
        }
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();
        }

    }

   

    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsTerritory;
            ddlFieldForce.DataBind();
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlFieldForce.SelectedIndex = 1;
                sfCode = ddlFieldForce.SelectedValue.ToString();
                Session["sf_code"] = sfCode;
            }
        }
    }

    //Populate the Year dropdown
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            DataSet  dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division

            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    int Year = k+ 1;
                    string F_Year = k + "-" + Year;
                    ddlFinancial.Items.Add(F_Year);
                }
            }

            string CurYear = DateTime.Now.Year.ToString() + "-" + (Convert.ToInt32(DateTime.Now.Year.ToString()) + 1);
            ddlFinancial.Items.FindByText(CurYear).Selected = true;

            //ddlFinancial.Items.Contains();
            //ddlFinancial.SelectedIndex = 0;
            
        }
        catch (Exception ex)
        {
            //ErrorLog err = new ErrorLog();
           // iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillYear()");
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void Get_CrmMgr_Status()
    {
        SecSale ss = new SecSale();
        DataSet dsCrm = ss.Get_CrmMgr_Status(div_code);
        if (dsCrm.Tables[0].Rows.Count > 0)
        {
            string Status = dsCrm.Tables[0].Rows[0]["Crm_Mgr"].ToString();
            Session["CRM_MgrStatus"] = Status;            
        }
    }

    private void FillDoc()
    {
        sfCode = Session["S_Code"].ToString();

        SecSale LstDoc = new SecSale();
        for (int i = 1; i < ddlFieldForce.Items.Count; i++)
        {
            sfCode = Session["S_Code"].ToString(); 

            if (ddlFieldForce.Items[i].Value == sfCode)
            {
                ddlFieldForce.SelectedIndex = i;
            }
        }

        string Mode = ddlMode.SelectedItem.Text;

        if (Mode == "Doctor")
        {
            dsDoc = LstDoc.Get_Listed_Dr_CRM(sfCode, div_code);

            gvChemist.Visible = false;

            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        else if (Mode == "Chemist/Pharmacy")
        {
            dsDoc = LstDoc.Get_Chemist_List_CRM(sfCode, div_code);

            grdDoctor.Visible = false;

            if (dsDoc.Tables[0].Rows.Count > 0)
            {   

                gvChemist.Visible = true;
                dlAlpha.Visible = true;
                gvChemist.DataSource = dsDoc;
                gvChemist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                gvChemist.DataSource = dsDoc;
                gvChemist.DataBind();
            }

        }
    }

    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        ListedDR LstDoc = new ListedDR();
        sCmd = Session["GetCmdArgChar"].ToString();
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        sfCode = ddlFieldForce.SelectedValue;

        if (sCmd == "All")
        {
            dtGrid = LstDoc.getListedDoctorList_DataTable(sfCode);
        }
        else if (sCmd != "")
        {
            dtGrid = LstDoc.getDoctorlistAlphabet_Datatable(sfCode, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = LstDoc.getListedDrforName_Datatable(sfCode, txtsearch.Text);
        }
        else if (ddlSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                dtGrid = LstDoc.getListedDoctorList_DataTable(sfCode);
            }
            else if (search == 2)
            {
                dtGrid = LstDoc.getListedDrforSpl_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 3)
            {
                dtGrid = LstDoc.getListedDrforCat_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 4)
            {
                dtGrid = LstDoc.getListedDrforQual_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 5)
            {
                dtGrid = LstDoc.getListedDrforClass_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
            else if (search == 6)
            {
                dtGrid = LstDoc.getListedDrforTerr_Datatable(sfCode, ddlSrc2.SelectedValue);
            }
        }

        return dtGrid;
    }

    protected void grdDoctor_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdDoctor.DataSource = sortedView;
        grdDoctor.DataBind();
    }

    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        txtsearch.Text = string.Empty;
        grdDoctor.PageIndex = 0;

        if (search == 7)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;

            FillDoc();
        }
        else
        {

            txtsearch.Visible = false;
            ddlSrc2.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillDoc();

        }
        if (search == 2)
        {
            FillSpl();
            FillDoc();
        }
        if (search == 3)
        {
            FillCat();
            FillDoc();
        }
        if (search == 4)
        {
            FillQualification();
            FillDoc();
        }
        if (search == 5)
        {
            FillClass();
            FillDoc();
        }
        if (search == 6)
        {
            FillTerritory();
            FillDoc();
        }
    }

    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();

        sfCode = ddlFieldForce.SelectedValue;

        dsListedDR = lstDR.FetchCategory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Cat_SName";
            ddlSrc2.DataValueField = "Doc_Cat_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillSpl()
    {
        ListedDR lstDR = new ListedDR();

        sfCode = ddlFieldForce.SelectedValue;

        dsListedDR = lstDR.FetchSpeciality(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_Special_SName";
            ddlSrc2.DataValueField = "Doc_Special_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillQualification()
    {
        ListedDR lstDR = new ListedDR();

        sfCode = ddlFieldForce.SelectedValue;

        dsListedDR = lstDR.FetchQualification(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_QuaName";
            ddlSrc2.DataValueField = "Doc_QuaCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillClass()
    {
        ListedDR lstDR = new ListedDR();
        sfCode = ddlFieldForce.SelectedValue;
        dsListedDR = lstDR.FetchClass(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Doc_ClsSName";
            ddlSrc2.DataValueField = "Doc_ClsCode";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        sfCode = ddlFieldForce.SelectedValue;
        dsListedDR = lstDR.FetchTerritory(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }
    
    protected void grdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");       
        }
    }

    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            SecSale ss = new SecSale();




            string S_Cde = Session["sf_code"].ToString();
            string Crm_Mgr = Session["CRM_MgrStatus"].ToString();

            S_Cde = ddlFieldForce.SelectedValue;

            string Status_Dr = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDocCode = (e.Row.FindControl("lblDocCode") as Label);
                Table tblStatus = (Table)e.Row.FindControl("tblCloseService");
                TableRow tr_Status = new TableRow();
                string Lst_DrCode = lblDocCode.Text;
                string sf_code = Session["S_Code"].ToString();
                Label lblDocCode_1 = (Label)e.Row.FindControl("lblDocCode");

                Label lblimg = (e.Row.FindControl("lblimg") as Label);

                  DataSet dsCrm = ss.Get_CrmMgr_Status(div_code);
                  if (dsCrm.Tables[0].Rows.Count > 0)
                  {
                       Status_Dr = dsCrm.Tables[0].Rows[0]["Crm_Mgr"].ToString();
                  }

                string DrCode = lblDocCode_1.Text;

                //string Dr_Code = HttpUtility.UrlEncode(Encrypt(DrCode));
                //string F_year = HttpUtility.UrlEncode(Encrypt(ddlFinancial.SelectedItem.ToString()));
                //string S_Code = HttpUtility.UrlEncode(Encrypt(S_Cde));
                //string C_Mgr = HttpUtility.UrlEncode(Encrypt(Crm_Mgr));
                //string ModeType = HttpUtility.UrlEncode(Encrypt("Doctor"));

                string Dr_Code = DrCode;
                string F_year =ddlFinancial.SelectedItem.ToString();
                string S_Code = S_Cde;
                string C_Mgr = Crm_Mgr;
                string ModeType ="Doctor";
                string F_Code = ddlFieldForce.SelectedValue;


                if (Status_Dr != "AB")
                {
                    string strlnk = "<a id=hlDrAdd href='Doctor_Service_Entry_New.aspx?ListedDrCode=" + DrCode + "&FnlYear=" + F_year + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
         "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
         "font-family: Verdana; text-align: center'  title=Add Service >Add Service</a>";

                    e.Row.Cells[8].Text = strlnk;
                }
                else
                {
                   // e.Row.Cells[8].Text = "";
                    lblimg.Visible = true;                    
                }

                if (Session["sf_type"].ToString() == "1")
                {
                    //e.Row.Cells[9].Text = "Edit/Update";
                    // hlDrEdit.InnerText = "Edit/Update";

                    string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ListedDrCode=" + DrCode + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
                         "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                         "font-family: Verdana; text-align: center'  title=Edit/Update >Edit/Update</a>";

                    e.Row.Cells[9].Text = str;

                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    e.Row.Cells[9].Text = "Edit/Approve";

                    string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ListedDrCode=" + DrCode + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
                             "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                             "font-family: Verdana; text-align: center'  title=Edit/Approve >Edit/Approve</a>";

                    e.Row.Cells[9].Text = str;
                }
                else
                {
                    e.Row.Cells[9].Text = "Edit/Approve";

                    string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ListedDrCode=" + DrCode + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
                             "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                             "font-family: Verdana; text-align: center'  title=Edit/Approve >Edit/Approve</a>";

                    e.Row.Cells[9].Text = str;
                }
                             
                DataSet dsClose = ss.GetDoctor_Service_CloseStatus(div_code, Lst_DrCode, sf_code);

                //HtmlAnchor hlDrEdit = (HtmlAnchor)e.Row.FindControl("hlDrEdit");



                if (dsClose.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drClose in dsClose.Tables[0].Rows)
                    {
                        string Sl_no = drClose["Sl_No"].ToString();
                        string Close_Service = drClose["Close_Service_Dr"].ToString();
                        int TargetAMt = 0;

                        if (drClose["Total_Business_Expect"].ToString() == "")
                        {
                            TargetAMt = 0;
                        }
                        else
                        {
                            TargetAMt = Convert.ToInt32(drClose["Total_Business_Expect"].ToString());
                        }

                        string Approve = drClose["Ser_Type"].ToString();

                        if (Approve == "0" || Approve == "")
                        {
                            e.Row.Cells[9].Enabled = false;
                        }
                        else if (Approve == "1")
                        {
                            e.Row.Cells[9].Enabled = true;
                        }

                        DataSet dsBusTotal = ss.GetTotal_Business(div_code, Lst_DrCode, sf_code, Sl_no);

                        if (dsBusTotal.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drTot in dsBusTotal.Tables[0].Rows)
                            {
                                decimal BusTotal = 0;

                                if (drTot["Total"].ToString() == "")
                                {
                                    BusTotal = 0;
                                }
                                else
                                {
                                    BusTotal = Convert.ToDecimal(drTot["Total"].ToString());
                                }

                                TableCell trCell = new TableCell();

                                if (BusTotal >= TargetAMt)
                                {
                                    trCell.BackColor = System.Drawing.Color.Green;
                                }
                                else if (BusTotal < TargetAMt && BusTotal != 0)
                                {
                                    trCell.BackColor = System.Drawing.Color.Yellow;
                                }
                                else if (BusTotal == 0)
                                {
                                    trCell.BackColor = System.Drawing.Color.Red;
                                }

                                trCell.Width = 20;
                                trCell.Height = 20;
                                tr_Status.Cells.Add(trCell);
                            }
                        }
                    }
                }

                tblStatus.Rows.Add(tr_Status);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDoctor.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
        ListedDR LstDoc = new ListedDR();

        if (sCmd == "All")
        {
            FillDoc();
        }
        else if (sCmd != "")
        {
            FillDoc(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        else if (ddlSrc2.SelectedIndex != -1)
        {
            Search();
        }

    }

    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        grdDoctor.PageIndex = 0;
        Search();
    }

    private void Search()
    {
        ListedDR LstDoc = new ListedDR();
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        sfCode = ddlFieldForce.SelectedValue;

        if (search == 1)
        {
            FillDoc();
        }
        if (search == 2)
        {
            dsDoc = LstDoc.getListedDrforSpl(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 3)
        {

            dsDoc = LstDoc.getListedDrforCat(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 4)
        {
            dsDoc = LstDoc.getListedDrforQual(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 5)
        {
            dsDoc = LstDoc.getListedDrforClass(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 6)
        {

            dsDoc = LstDoc.getListedDrforTerr(sfCode, ddlSrc2.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
        }
        if (search == 7)
        {

            dsDoc = LstDoc.getListedDrforName(sfCode, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                dlAlpha.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

        }
    }

    private void FillDoc_Alpha()
    {
        ListedDR Lstdr = new ListedDR();
        dsListedDR = Lstdr.getDoctorlist_Alphabet(sfCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsListedDR;
            dlAlpha.DataBind();
        }
    }
    private void FillDoc(string sAlpha)
    {
        ListedDR Lstdr = new ListedDR();

        string Mode = ddlMode.SelectedItem.Text;

        if (Mode == "Doctor")
        {
            dsListedDR = Lstdr.getDoctorlist_Alphabet(sfCode, sAlpha);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsListedDR;
                grdDoctor.DataBind();
                gvChemist.Visible = false;
            }
        }
        else if (Mode == "Chemist/Pharmacy")
        {
            SecSale ss = new SecSale();
            dsListedDR = ss.getChemistList_Alphabet(sfCode, sAlpha,div_code);
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                gvChemist.Visible = true;
                gvChemist.DataSource = dsListedDR;
                gvChemist.DataBind();
                grdDoctor.Visible = false;
            }
        }
       
    }

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdDoctor.PageIndex = 0;
            FillDoc();
        }
        else
        {
            grdDoctor.PageIndex = 0;
            FillDoc(sCmd);
        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        try
        {
           // Session["S_Code"] = Session["sf_code"].ToString();  
           
            sfCode = ddlFieldForce.SelectedValue;
            Session["S_Code"] = sfCode;

            string SF_Cd = ddlFieldForce.SelectedValue;        
           
            FillDoc();


            string Mode = ddlMode.SelectedItem.Text;

            if (Mode == "Doctor")
            {
                grdDoctor.Visible = true;
                tblSearch.Visible = true;
                gvChemist.Visible = false;
                tblSr_Chemist.Visible = false;
            }
            else if (Mode == "Chemist/Pharmacy")
            {
                gvChemist.Visible = true;
                tblSr_Chemist.Visible = true;
                grdDoctor.Visible = false;
                tblSearch.Visible = false;
            }
           
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFieldForce.SelectedIndex = -1;
        ddlFinancial.SelectedIndex = -1;
        grdDoctor.Visible = false;
        tblSearch.Visible = false;
        gvChemist.Visible = false;
        tblSr_Chemist.Visible = false;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Listed_Dr_Service_Status> Get_Dr_Service_Status()
    {
        SecSale lst = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["S_Code"].ToString();

        SecSale ss = new SecSale();

        DataSet dsDoc = ss.Get_Listed_Dr_CRM(sf_code,div_code);

        string LstCode = "";

        List<Listed_Dr_Service_Status> objDrDetail = new List<Listed_Dr_Service_Status>();

        foreach (DataRow dr in dsDoc.Tables[0].Rows)
        {
            string Lst_DrCode = dr["ListedDrCode"].ToString();

            DataSet dsClose = ss.GetDoctor_Service_CloseStatus(div_code, Lst_DrCode, sf_code);

            if (dsClose.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drClose in dsClose.Tables[0].Rows)
                {
                    Listed_Dr_Service_Status objStatus = new Listed_Dr_Service_Status();

                    objStatus.DrCnt =Convert.ToString(dsClose.Tables[0].Rows.Count);
                    objStatus.Dr_Code = drClose["Doctor_Code"].ToString();
                    objStatus.Close_Service = drClose["Close_Service_Dr"].ToString();
                    objDrDetail.Add(objStatus);
                }
            }
        }
        return objDrDetail;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<DocAppoveStatus>GetDoctor_ServiceStatus(string DrCode)
    {
        SecSale lst = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["S_Code"].ToString();

        SecSale ss = new SecSale();

        //DataSet dsDoc = ss.Get_Listed_Dr_CRM(sf_code);

        //string LstCode = "";

        List<DocAppoveStatus> objDrDetail = new List<DocAppoveStatus>();

      ////  foreach (DataRow dr in dsDoc.Tables[0].Rows)
       // {

        //ddlFieldForce
        
         string Lst_DrCode = DrCode;

            DataSet dsClose = ss.GetCRM_Servcie_ApprovalDet(div_code, Lst_DrCode, sf_code);

            if (dsClose.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drClose in dsClose.Tables[0].Rows)
                {
                    DocAppoveStatus objStatus = new DocAppoveStatus();

                    objStatus.Doctor_Code = drClose["Doctor_Code"].ToString();
                    objStatus.ListedDr_Name = drClose["ListedDr_Name"].ToString();
                    objStatus.ServiceName = drClose["ServiceName"].ToString();
                    objStatus.Ser_Type = drClose["Ser_Type"].ToString();
                    objStatus.Reporting_To = drClose["Reporting_To"].ToString();
                    objDrDetail.Add(objStatus);
                }
            }
       // }
        return objDrDetail;
    }

    protected void gvChemist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        SecSale ss = new SecSale();
        string S_Cde = Session["sf_code"].ToString();
        string Crm_Mgr = Session["CRM_MgrStatus"].ToString();
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblChemistCode = (e.Row.FindControl("lblChemistCode") as Label);
            Table tblStatus = (Table)e.Row.FindControl("tblCloseService");
            TableRow tr_Status = new TableRow();
            string Lst_ChemistCode = lblChemistCode.Text;

            //string sf_code = Session["S_Code"].ToString();

            Label lblChemistCode_1 = (Label)e.Row.FindControl("lblChemistCode");

            Label lblimg = (e.Row.FindControl("lblimg") as Label);

            string ChemistCode = lblChemistCode_1.Text;

            string Chm_Code = ChemistCode;
            string F_year = ddlFinancial.SelectedItem.ToString();
            string S_Code = S_Cde;
            string C_Mgr = Crm_Mgr;
            string ModeType = "Chemist";
            string F_Code = ddlFieldForce.SelectedValue;

            string Status_Dr = string.Empty;

            DataSet dsCrm = ss.Get_CrmMgr_Status(div_code);
            if (dsCrm.Tables[0].Rows.Count > 0)
            {
                Status_Dr = dsCrm.Tables[0].Rows[0]["Crm_Mgr"].ToString();
            }


            if (Status_Dr != "AB")
            {
                string strlnk = "<a id=hlDrAdd href='Doctor_Service_Entry_New.aspx?ListedDrCode=" + Chm_Code + "&FnlYear=" + F_year + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
                   "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                   "font-family: Verdana; text-align: center'  title=Add Service >Add Service</a>";


                e.Row.Cells[7].Text = strlnk;
            }
            else
            {
                // e.Row.Cells[8].Text = "";
                lblimg.Visible = true;
            }

            if (Session["sf_type"].ToString() == "1")
            {
                //e.Row.Cells[9].Text = "Edit/Update";

                // hlDrEdit.InnerText = "Edit/Update";

                //string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ChemistCode=" + ChemistCode + "&Mode=Chemist' onclick='return ShowProgress();'" +
                //     "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                //     "font-family: Verdana; text-align: center'  title=Edit/Update >Edit/Update</a>";

                string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ListedDrCode=" + Chm_Code + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
                  "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                  "font-family: Verdana; text-align: center'  title=Edit/Update >Edit/Update</a>";

                e.Row.Cells[8].Text = str;

            }
            else if (Session["sf_type"].ToString() == "2")
            {
                e.Row.Cells[8].Text = "Edit/Approve";

                //string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ChemistCode=" + ChemistCode + "&Mode=Chemist' onclick='return ShowProgress();'" +
                //         "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                //         "font-family: Verdana; text-align: center'  title=Edit/Approve >Edit/Approve</a>";            

                string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ListedDrCode=" + Chm_Code + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
             "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
             "font-family: Verdana; text-align: center'  title=Edit/Approve >Edit/Approve</a>"; 

                e.Row.Cells[8].Text = str;
            }
            else
            {
                e.Row.Cells[8].Text = "Edit/Approve";

                string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ListedDrCode=" + Chm_Code + "&Mode=" + ModeType + "&SCode=" + S_Code + "&CrmStatus=" + C_Mgr + "&F_Code=" + F_Code + "&TypeCd=" + div_code + "' onclick='return ShowProgress();'" +
                "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                "font-family: Verdana; text-align: center'  title=Edit/Approve >Edit/Approve</a>"; 

                //string str = "<a id=hlDrEdit href='Doctor_Service_CRM_Edit.aspx?ChemistCode=" + ChemistCode + "&Mode=Chemist' onclick='return ShowProgress();'" +
                //         "class='Service_CSS' style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                //         "font-family: Verdana; text-align: center'  title=Edit/Approve >Edit/Approve</a>";

                e.Row.Cells[8].Text = str;
            }
          
            DataSet dsClose = ss.GetChemist_Service_CloseStatus(div_code, Lst_ChemistCode, sf_code);

            //HtmlAnchor hlDrEdit = (HtmlAnchor)e.Row.FindControl("hlDrEdit");

            if (dsClose.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drClose in dsClose.Tables[0].Rows)
                {
                    string Sl_no = drClose["Sl_No"].ToString();
                    string Close_Service = drClose["Close_Service_Dr"].ToString();
                    int TargetAMt = 0;

                    if (drClose["Total_Business_Expect"].ToString() == "")
                    {
                        TargetAMt = 0;
                    }
                    else
                    {
                        TargetAMt = Convert.ToInt32(drClose["Total_Business_Expect"].ToString());
                    }

                    string Approve = drClose["Ser_Type"].ToString();

                    if (Approve == "0" || Approve == "")
                    {
                        e.Row.Cells[8].Enabled = false;
                    }
                    else if (Approve == "1")
                    {
                        e.Row.Cells[8].Enabled = true;
                    }

                    DataSet dsBusTotal = ss.GetTotal_Business(div_code, Lst_ChemistCode, sf_code, Sl_no);

                    //if (dsBusTotal.Tables[0].Rows.Count > 0)
                    //{
                    //    foreach (DataRow drTot in dsBusTotal.Tables[0].Rows)
                    //    {
                    //        decimal BusTotal = 0;

                    //        if (drTot["Total"].ToString() == "")
                    //        {
                    //            BusTotal = 0;
                    //        }
                    //        else
                    //        {
                    //            BusTotal = Convert.ToDecimal(drTot["Total"].ToString());
                    //        }

                    //        TableCell trCell = new TableCell();

                    //        if (BusTotal >= TargetAMt)
                    //        {
                    //            trCell.BackColor = System.Drawing.Color.Green;
                    //        }
                    //        else if (BusTotal < TargetAMt && BusTotal != 0)
                    //        {
                    //            trCell.BackColor = System.Drawing.Color.Yellow;
                    //        }
                    //        else if (BusTotal == 0)
                    //        {
                    //            trCell.BackColor = System.Drawing.Color.Red;
                    //        }

                    //        trCell.Width = 20;
                    //        trCell.Height = 20;
                    //        tr_Status.Cells.Add(trCell);
                    //    }
                    //}
                }
            }

            tblStatus.Rows.Add(tr_Status);
        }
    }

    private void Search_Chemist()
    {
        SecSale ss = new SecSale();
        search = Convert.ToInt32(ddlSrch_Chm.SelectedValue);

        if (search == 1)
        {
            FillDoc();
        }       
        if (search == 2)
        {

            dsDoc = ss.Search_ChemistName(sfCode, txtsearch_Chm.Text, div_code);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = false;
                dlAlpha.Visible = false;
                gvChemist.Visible = true;
                dlAlpha_Chm.Visible = true;
                gvChemist.DataSource = dsDoc;
                gvChemist.DataBind();
            }
            else
            {
                dlAlpha.Visible = false;
                dlAlpha_Chm.Visible = false;
                gvChemist.DataSource = dsDoc;
                gvChemist.DataBind();
            }

        }
    }

    protected void Btnsrc_Chm_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar_Chemist"] = string.Empty;
        gvChemist.PageIndex = 0;
        Search_Chemist();
    }

    protected void gvChemist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChemist.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar_Chemist"].ToString();

       // string Strc = Session["GetCmdArgChar"].ToString();

        sf_code = ddlFieldForce.SelectedValue;

        ListedDR LstDoc = new ListedDR();

        SecSale ss = new SecSale();

        if (sCmd == "All")
        {
            FillDoc();
        }
        else if (sCmd != "")
        {
            FillDoc(sCmd);
        }
        else if (txtsearch_Chm.Text != "")
        {
            dsDoc = ss.Search_ChemistName(sfCode, txtsearch_Chm.Text, div_code);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                gvChemist.Visible = true;
                dlAlpha_Chm.Visible = true;
                gvChemist.DataSource = dsDoc;
                gvChemist.DataBind();

                grdDoctor.Visible = false;
                dlAlpha.Visible = false;
            }
            else
            {
                dlAlpha_Chm.Visible = false;
                gvChemist.DataSource = dsDoc;
                gvChemist.DataBind();
            }
        }
        else if (ddlSrc2_Chm.SelectedIndex != -1)
        {
            Search_Chemist();
        }

    }

    protected void ddlSrch_Chm_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch_Chm.SelectedValue);
        txtsearch_Chm.Text = string.Empty;
        gvChemist.PageIndex = 0;

        if (search == 2)
        {
            txtsearch_Chm.Visible = true;
            Btnsrc_Chm.Visible = true;
            ddlSrc2_Chm.Visible = false;
        }
        else
        {

            txtsearch_Chm.Visible = false;
            ddlSrc2_Chm.Visible = true;
            Btnsrc_Chm.Visible = true;
        }
        if (search == 1)
        {
            txtsearch_Chm.Visible = false;
            ddlSrc2_Chm.Visible = false;
            Btnsrc_Chm.Visible = false;
            FillDoc();

        }
       
    }

    protected void gvChemist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    public static string Encrypt(string inputText)
    {
        string encryptionkey = "SAUW193BX628TD57";
        byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
        using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
        {
            using (MemoryStream mstrm = new MemoryStream())
            {
                using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                {
                    cryptstm.Write(plainText, 0, plainText.Length);
                    cryptstm.Close();
                    return Convert.ToBase64String(mstrm.ToArray());
                }
            }
        }
    }

}

public class Listed_Dr_Service_Status
{
    public string DrCnt { get; set; }
    public string Dr_Code { get; set; }
    public string Close_Service { get; set; }
}

public class DocAppoveStatus
{
    public string Doctor_Code { get; set; }
    public string ListedDr_Name { get; set; }
    public string ServiceName { get; set; }
    public string Ser_Type { get; set; }
    public string Reporting_To { get; set; }
}
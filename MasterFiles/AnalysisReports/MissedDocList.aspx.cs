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
using Bus_EReport;
using System.Net;

public partial class MIS_Reports_MissedDocList : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsListedDR = null;
    DataSet dsOneVisit = null;
    DataSet dsTwoVisit = null;
    DataSet dsThreeVisit = null;
    DataSet dsmore = null;
    DataSet dsDoc = null;
    DataSet dsTerritory = null;
    DataSet dsdate = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string vMode = string.Empty;
    string sf_name = string.Empty;
    string sf_hq = string.Empty;
    string sf_desig = string.Empty;
    string cnt = string.Empty;
    int search = 0;
    string sCmd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        vMode = Request.QueryString["vMode"].ToString();
        cnt = Request.QueryString["cnt"].ToString();
        if (vMode != "")
        {
            if (sMode == "4")
            {
                lblsubhead.Text = "Category: ";
                Doctor dc = new Doctor();
                DataSet dsdc = dc.getCatgName(vMode, div_code);
                if (dsdc.Tables[0].Rows.Count > 0)
                    lblsubhead.Text = lblsubhead.Text + Convert.ToString(dsdc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                lblsubhead.Visible = true;

            }
            if (sMode == "5")
            {
                lblsubhead.Text = "Speciality: ";
                Doctor dc = new Doctor();
                DataSet dsdc = dc.getSpecName(vMode, div_code);
                if (dsdc.Tables[0].Rows.Count > 0)
                    lblsubhead.Text = lblsubhead.Text + Convert.ToString(dsdc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                lblsubhead.Visible = true;

            }
            if (sMode == "6")
            {
                lblsubhead.Text = "Class: ";
                Doctor dc = new Doctor();
                DataSet dsdc = dc.getClassName(vMode, div_code);
                if (dsdc.Tables[0].Rows.Count > 0)
                    lblsubhead.Text = lblsubhead.Text + Convert.ToString(dsdc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                lblsubhead.Visible = true;

            }
        }
        if (!Page.IsPostBack)
        {
            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
            sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            sf_desig = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
            sf_hq = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());

            string sMonth = getMonthName(Convert.ToInt16(FMonth)) + " " + FYear.ToString();
            if (!Page.IsPostBack)
            {
                lblHead.Text = lblHead.Text + sMonth + "<span style='font-weight: bold;color:Black;'> " + " ( " + sf_name + " - " + sf_desig + " - " + sf_hq + " )" + "</span>";
                getWorkName();
            }
            FillDoctor();
            FillOneVisit();
            FillTwoVisit();
            FillThreeVisit();
            FillMoreVisit();

        }
    }
    private void getWorkName()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            string str = "Doctor " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            ddlSrch.Items.Add(new System.Web.UI.WebControls.ListItem(str, "6", true));
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
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        ListedDR LstDoc = new ListedDR();
        Doctor doc = new Doctor();
        // sCmd = Session["GetCmdArgChar"].ToString();
        DateTime dtCurrent1;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent1 = Convert.ToDateTime(sCurrentDate);
        //  dtGrid = doc.Missed_sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, sMode);
        search = Convert.ToInt32(ddlSrch.SelectedValue);

        if (ddlSrch.SelectedIndex != -1)
        {
            if (search == 1)
            {
                //dtGrid = doc.Missed_sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, sMode);
                dtGrid = doc.Missed_List_MR_Sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1);
            }
            else if (search == 2)
            {
                dtGrid = doc.Missed_List_MR_Spec_Sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, ddlSrc2.SelectedValue);
               // dtGrid = LstDoc.getMiss_spl_sort(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1);
            }
            else if (search == 3)
            {
                dtGrid = doc.Missed_List_MR_Cat_Sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, ddlSrc2.SelectedValue);
               // dtGrid = LstDoc.getmiss_Cat_sort(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1);
            }
            else if (search == 4)
            {
                dtGrid = doc.Missed_List_MR_Qual_Sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, ddlSrc2.SelectedValue);
              //  dtGrid = LstDoc.getmiss_Qual_sort(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1);
            }
            else if (search == 5)
            {
                dtGrid = doc.Missed_List_MR_Cls_Sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, ddlSrc2.SelectedValue);
               // dtGrid = LstDoc.getmiss_Class_sort(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1);
            }
            else if (search == 6)
            {
                dtGrid = doc.Missed_List_MR_Terr_Sort(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1, ddlSrc2.SelectedValue);
               // dtGrid = LstDoc.getmiss_Terr_sort(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent1);
            }
        }

        return dtGrid;
    }



    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
    private void FillDoctor()
    {
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        Doctor dc = new Doctor();
        if (vMode != "")
        {
           // dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode, vMode);
        }
        else
        {
            dsDoctor = dc.Missed_List_MR(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
          //  dsDoctor = dc.Missed_Doc_cnt(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode);
            //dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode, vMode);
        }

        lblmisCnt.Text = "(" +cnt+ ")";
        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();

        }

    }

    private void FillOneVisit()
    {
        DCR dc = new DCR();
        dsOneVisit = dc.visit_cnt_1(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsOneVisit.Tables[0].Rows.Count > 0)
            lbloneCnt.Text = " (" + dsOneVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() +")";
        dsOneVisit = dc.One_Visit_Dr_SVL(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsOneVisit.Tables[0].Rows.Count > 0)
        {
            grdOneVisit.Visible = true;
            grdOneVisit.DataSource = dsOneVisit;
            grdOneVisit.DataBind();
        }
        else
        {
            grdOneVisit.DataSource = dsOneVisit;
            grdOneVisit.DataBind();

        }
    }
    private void FillTwoVisit()
    {
        DCR dc = new DCR();
        dsTwoVisit = dc.visit_cnt_2(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsTwoVisit.Tables[0].Rows.Count > 0)
            lbltwoCnt.Text = " (" + dsTwoVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + ")";
        dsTwoVisit = dc.Two_Visit_Dr_SVL(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsTwoVisit.Tables[0].Rows.Count > 0)
        {
            grdTwoVisit.Visible = true;
            grdTwoVisit.DataSource = dsTwoVisit;
            grdTwoVisit.DataBind();
        }
        else
        {
            grdTwoVisit.DataSource = dsTwoVisit;
            grdTwoVisit.DataBind();
        }
    }
    private void FillThreeVisit()
    {
        DCR dc = new DCR();
        dsThreeVisit = dc.visit_cnt_3(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsThreeVisit.Tables[0].Rows.Count > 0)
            lblthreecnt.Text = " (" + dsThreeVisit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + ")";
  
        dsThreeVisit = dc.Three_Visit_Dr_SVL(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsThreeVisit.Tables[0].Rows.Count > 0)
        {
            grdThreeVisit.Visible = true;
            grdThreeVisit.DataSource = dsThreeVisit;
            grdThreeVisit.DataBind();
        }
        else
        {
            grdThreeVisit.DataSource = dsThreeVisit;
            grdThreeVisit.DataBind();
        }
    }
    private void FillMoreVisit()
    {
        DCR dc = new DCR();
        dsmore = dc.visit_cnt_more(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsmore.Tables[0].Rows.Count > 0)
            lblmoreCnt.Text = " (" + dsmore.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + ")";

        dsmore = dc.More_Visit_Dr_SVL(sf_code, div_code, Convert.ToInt32(FMonth), Convert.ToInt32(FYear));
        if (dsmore.Tables[0].Rows.Count > 0)
        {
            grdMoreVisit.Visible = true;
            grdMoreVisit.DataSource = dsmore;
            grdMoreVisit.DataBind();
        }
        else
        {
            grdMoreVisit.DataSource = dsmore;
            grdMoreVisit.DataBind();
        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //  e.Row.Cells[7].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                LinkButton LnkHeaderText = e.Row.Cells[9].Controls[0] as LinkButton;
                LnkHeaderText.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbldrcode = (Label)e.Row.FindControl("lblDRCode");
            Label lblvisit = (Label)e.Row.FindControl("lblVisit");
            string sActive_date = string.Empty;
            DCR dcrdr = new DCR();
            dsdate = dcrdr.getPrevious_Visit(sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), lbldrcode.Text);
            if (dsdate.Tables[0].Rows.Count > 0)
                
                lblvisit.Text = "" + dsdate.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


            if (lblvisit.Text == "")
            {
                lblvisit.Text = " - ";
            }
        }
    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {

        Session["GetcmdArgChar"] = string.Empty;
        grdDoctor.PageIndex = 0;
        Search();
        //FillOneVisit();
        //FillTwoVisit();
        //FillThreeVisit();
        //FillMoreVisit();
    }
    private void Search()
    {
        ListedDR LstDoc = new ListedDR();
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;
        Doctor dd = new Doctor();
        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        if (search == 1)
        {

            FillDoctor();
            FillOneVisit();
            FillTwoVisit();
            FillThreeVisit();
            FillMoreVisit();
        }
        if (search == 2)
        {
            dsDoc = dd.Missed_List_MR_Spec(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, ddlSrc2.SelectedValue);
           // dsDoc = LstDoc.getMiss_spl(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;

                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {

                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

            dsOneVisit = LstDoc.Spec_OneVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsOneVisit.Tables[0].Rows.Count > 0)
            {
                grdOneVisit.Visible = true;
                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            else
            {

                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            dsTwoVisit = LstDoc.Spec_TwoVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsTwoVisit.Tables[0].Rows.Count > 0)
            {
                grdTwoVisit.Visible = true;
                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            else
            {

                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            dsThreeVisit = LstDoc.Spec_ThreeVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsThreeVisit.Tables[0].Rows.Count > 0)
            {
                grdThreeVisit.Visible = true;
                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            else
            {

                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            dsmore = LstDoc.Spec_MoreVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsmore.Tables[0].Rows.Count > 0)
            {
                grdMoreVisit.Visible = true;
                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
            else
            {

                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }

        }
        if (search == 3)
        {
            dsDoc = dd.Missed_List_MR_Cat(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, ddlSrc2.SelectedValue);
           // dsDoc = LstDoc.getmiss_Cat(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {

                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

            dsOneVisit = LstDoc.Cat_OneVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsOneVisit.Tables[0].Rows.Count > 0)
            {
                grdOneVisit.Visible = true;
                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            else
            {

                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            dsTwoVisit = LstDoc.Cat_TwoVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsTwoVisit.Tables[0].Rows.Count > 0)
            {
                grdTwoVisit.Visible = true;
                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            else
            {

                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            dsThreeVisit = LstDoc.Cat_ThreeVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsThreeVisit.Tables[0].Rows.Count > 0)
            {
                grdThreeVisit.Visible = true;
                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            else
            {

                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            dsmore = LstDoc.Cat_MoreVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsmore.Tables[0].Rows.Count > 0)
            {
                grdMoreVisit.Visible = true;
                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
            else
            {

                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
        }
        if (search == 4)
        {
            dsDoc = dd.Missed_List_MR_Qual(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, ddlSrc2.SelectedValue);
           // dsDoc = LstDoc.getmiss_Qual(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            dsOneVisit = LstDoc.Qua_OneVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsOneVisit.Tables[0].Rows.Count > 0)
            {
                grdOneVisit.Visible = true;
                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            else
            {

                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            dsTwoVisit = LstDoc.Qua_TwoVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsTwoVisit.Tables[0].Rows.Count > 0)
            {
                grdTwoVisit.Visible = true;
                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            else
            {

                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            dsThreeVisit = LstDoc.Qua_ThreeVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsThreeVisit.Tables[0].Rows.Count > 0)
            {
                grdThreeVisit.Visible = true;
                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            else
            {

                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            dsmore = LstDoc.Qua_MoreVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsmore.Tables[0].Rows.Count > 0)
            {
                grdMoreVisit.Visible = true;
                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
            else
            {

                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
        }
        if (search == 5)
        {
            dsDoc = dd.Missed_List_MR_Cls(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, ddlSrc2.SelectedValue);
           // dsDoc = LstDoc.getmiss_Class(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            dsOneVisit = LstDoc.Cls_OneVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsOneVisit.Tables[0].Rows.Count > 0)
            {
                grdOneVisit.Visible = true;
                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            else
            {

                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            dsTwoVisit = LstDoc.Cls_TwoVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsTwoVisit.Tables[0].Rows.Count > 0)
            {
                grdTwoVisit.Visible = true;
                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            else
            {

                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            dsThreeVisit = LstDoc.Cls_ThreeVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsThreeVisit.Tables[0].Rows.Count > 0)
            {
                grdThreeVisit.Visible = true;
                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            else
            {

                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            dsmore = LstDoc.Cls_MoreVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsmore.Tables[0].Rows.Count > 0)
            {
                grdMoreVisit.Visible = true;
                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
            else
            {

                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
        }
        if (search == 6)
        {
            dsDoc = dd.Missed_List_MR_Terr(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, ddlSrc2.SelectedValue);
            //dsDoc = LstDoc.getmiss_Terr(sf_code, ddlSrc2.SelectedValue, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {

                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            dsOneVisit = LstDoc.Terr_OneVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsOneVisit.Tables[0].Rows.Count > 0)
            {
                grdOneVisit.Visible = true;
                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            else
            {

                grdOneVisit.DataSource = dsOneVisit;
                grdOneVisit.DataBind();
            }
            dsTwoVisit = LstDoc.Terr_TwoVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsTwoVisit.Tables[0].Rows.Count > 0)
            {
                grdTwoVisit.Visible = true;
                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            else
            {

                grdTwoVisit.DataSource = dsTwoVisit;
                grdTwoVisit.DataBind();
            }
            dsThreeVisit = LstDoc.Terr_ThreeVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsThreeVisit.Tables[0].Rows.Count > 0)
            {
                grdThreeVisit.Visible = true;
                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            else
            {

                grdThreeVisit.DataSource = dsThreeVisit;
                grdThreeVisit.DataBind();
            }
            dsmore = LstDoc.Terr_MoreVisit_SVL(sf_code, div_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), ddlSrc2.SelectedValue);
            if (dsmore.Tables[0].Rows.Count > 0)
            {
                grdMoreVisit.Visible = true;
                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
            else
            {

                grdMoreVisit.DataSource = dsmore;
                grdMoreVisit.DataBind();
            }
        }
        if (search == 7)
        {

            dsDoc = LstDoc.getListedDrforName_SVL(sf_code, div_code, txtsearch.Text);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                grdDoctor.Visible = true;
                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }
            else
            {

                grdDoctor.DataSource = dsDoc;
                grdDoctor.DataBind();
            }

        }
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
            FillDoctor();
            FillOneVisit();
            FillTwoVisit();
            FillThreeVisit();
            FillMoreVisit();

        }
        if (search == 2)
        {
            FillSpl();
        }
        if (search == 3)
        {
            FillCat();
        }
        if (search == 4)
        {
            FillQualification();
        }
        if (search == 5)
        {
            FillClass();
        }
        if (search == 6)
        {
            FillTerritory();
        }
    }
    private void FillCat()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
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
        dsListedDR = lstDR.FetchSpeciality(sf_code);
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
        dsListedDR = lstDR.FetchQualification(sf_code);
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
        dsListedDR = lstDR.FetchClass(sf_code);
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
        dsListedDR = lstDR.FetchTerritory(sf_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddlSrc2.DataTextField = "Territory_Name";
            ddlSrc2.DataValueField = "Territory_Code";
            ddlSrc2.DataSource = dsListedDR;
            ddlSrc2.DataBind();
        }

    }

}
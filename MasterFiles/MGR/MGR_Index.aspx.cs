using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_MGR_MGR_Index : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsAdmin = null;
    DataSet dsDoc1 = null;
    DataSet dsSalesForce = null;
    DataSet dsTP = new DataSet();
    DataSet dsAdm = null;
    DataSet dsDcr = new DataSet();
    DataSet dsAdmNB = null;
    DataSet dsAdminSetup = null;
    DataSet dsadmin = null;
    DataSet dsSecSales = null;
    string sfCode = string.Empty;
    //string sf_code = string.Empty;
    string div_code = string.Empty;
    string strdcrtxt = string.Empty;
    string strtptxt = string.Empty;
    string strMultiDiv = string.Empty;
    string strleavetxt = string.Empty;

    DataSet dsTp = null;
    DataSet dsDesig = null;
    DataSet dsTp2 = null;
    DataSet dsExp = null;
    string from_month = string.Empty;
    int time;
    int tp_month4;
    string from_year = string.Empty;
    int from_year2;
    int next_month4;
    int to_year2;
    string to_year = string.Empty;
    DataSet dsLogin = null;
    string strEx = string.Empty;
    string strDocadd = string.Empty;

    string strDeact = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();
            
        }
        if (!Page.IsPostBack)
        {
            DataSet dsdiv = new DataSet();
            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sfCode);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                    ddlDivision.Visible = true;
                    lblDivision.Visible = true;

                    getDivision();
                }
                else
                {

                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;

                    //  BindUserList();
                }
            }
            ddlDivision.SelectedValue = div_code;
            //if (div_code == "3")
            //{
            //    FillDoc_unique();
            //}
            //else
            //{
            //    FillDoc();
            //}
            //FillDcr();
            FillDoc_Deact();
            FillTourPlan();
           //FillLeave();
            FillDoc_AddDeactivate();
            FillExp1();
            FillSecSales();
            //if (div_code != "2")
            //{
            //    grdListedDR.Columns[1].Visible = true;
            //    grdListedDR.Columns[6].Visible = false;
            //    grdListedDR.Columns[7].Visible = true;

            //    grdListedDR1.Columns[1].Visible = true;
            //    grdListedDR1.Columns[6].Visible = false;
            //    grdListedDR1.Columns[7].Visible = true;
            //    FillDoc_unique();

            //}
            //else
            //{
                grdListedDR.Columns[1].Visible = false;
                grdListedDR.Columns[7].Visible = false;
                grdListedDR.Columns[6].Visible = true;

                grdListedDR1.Columns[1].Visible = false;
                grdListedDR1.Columns[7].Visible = false;
                grdListedDR1.Columns[6].Visible = true;
                FillDoc();
           // }
            Session["backurl"] = "~/MasterFiles/MGR/MGR_Index.aspx";
            //if ((dsTP.Tables[0].Rows.Count > 0) || (dsDcr.Tables[0].Rows.Count > 0) || (dsAdminSetup.Tables[0].Rows.Count > 0))
            //{
            //    btnHome.Visible = false;
            //}
          
         //   menu1.Title = this.Page.Title;
           // // menu1.FindControl("btnBack").Visible = false;
               AdminSetup dv = new AdminSetup();
            dsadmin = dv.getHomePage_Restrict(div_code);
            if (dsadmin.Tables[0].Rows.Count > 0)
            {
                //string strdcr = dsadmin.Tables[0].Rows[0]["DCR_Home"].ToString();
                //if (strdcr == "1" && (dsDcr.Tables[0].Rows.Count > 0))
                //{
                //    lblhomepage.Visible = true;
                //    btnHome.Visible = false;

                //    strdcrtxt = "DCR ";
                //}
                string strTp = dsadmin.Tables[0].Rows[0]["TP_Home"].ToString();
                if (strTp == "1" && (dsTP.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                    btnHome.Visible = false;
                    strtptxt = "/ TP";
                }
                //string strLeave = dsadmin.Tables[0].Rows[0]["Leave_Home"].ToString();
                //if (strLeave == "1" && (dsAdminSetup.Tables[0].Rows.Count > 0))
                //{
                //    lblhomepage.Visible = true;
                //    btnHome.Visible = false;
                //    strleavetxt = " / Leave";

                //}
                string strExpen = dsadmin.Tables[0].Rows[0]["Expense_Home"].ToString();
                if (strExpen == "1" && (dsExp.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                    btnHome.Visible = false;
                    strEx = " / Expense";
                }
                string strdocadd = dsadmin.Tables[0].Rows[0]["Listeddr_Add_Home"].ToString();
                if (strdocadd == "1" && (dsDoc.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                   btnHome.Visible = false;
                   strDocadd = " / Listddr Add";
                }
                string strdocdeac = dsadmin.Tables[0].Rows[0]["Listeddr_Deact_Home"].ToString();
                if (strdocdeac == "1" && (dsDoc1.Tables[0].Rows.Count > 0))
                {
                    lblhomepage.Visible = true;
                    btnHome.Visible = false;
                    strDeact = " / Listddr Deact";
                }


                //   menu1.Title = this.Page.Title;
                // // menu1.FindControl("btnBack").Visible = false;

            }

            lbltext.Text = strdcrtxt + strtptxt + strleavetxt + strEx + strDocadd + strDeact;
        

         
        }
        

    }
    private void FillExp1()
    {
        GridExpense.DataSource = null;
        GridExpense.DataBind();

        Expense Exp = new Expense();
        dsExp = Exp.getExp_approve1(sfCode, div_code);
        if (dsExp.Tables[0].Rows.Count > 0)
        {

            GridExpense.Visible = true;
            GridExpense.DataSource = dsExp;
            GridExpense.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            GridExpense.DataSource = dsExp;
            GridExpense.DataBind();
        }
    }
    private void FillDoc()
    {
        string iVal = "2";
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc1 = new ListedDR();
        dsDoc = LstDoc1.getListedDr_MGR_Mode_One(sfCode, iVal, div_code);

        if (dsDoc.Tables[0].Rows.Count > 0)
        {
           
            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }
    private void FillDoc_unique()
    {
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc1 = new ListedDR();
        dsDoc = LstDoc1.getListedDr_MGR_Mode_One(sfCode, 2, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {

            grdListedDR.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }
    private void FillDcr()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();
        DCR dr = new DCR();
        if (div_code.Contains(','))
            div_code = div_code.Substring(0, div_code.Length - 1);
        dsDcr = dr.get_DCR_Pending_Approval(sfCode, div_code);
        if (dsDcr.Tables[0].Rows.Count > 0)
        {
            grdDCR.Visible = true;
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
    }
    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }
    private void FillDoc_Deact()
    {
        grdListedDR1.DataSource = null;
        grdListedDR1.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc1 = LstDoc.getListedDr_MGRNew(sfCode, 3, div_code);
        if (dsDoc1.Tables[0].Rows.Count > 0)
        {
          
            grdListedDR1.Visible = true;
            grdListedDR1.DataSource = dsDoc1;
            grdListedDR1.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdListedDR1.DataSource = dsDoc1;
            grdListedDR1.DataBind();
        }
    }

    private void FillDoc_AddDeactivate()
    {
        grdadddeactivate.DataSource = null;
        grdadddeactivate.DataBind();

        ListedDR lstAdd = new ListedDR();
        dsDoc = lstAdd.getListedDr_adddeact_One(sfCode, 2, 3, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdadddeactivate.Visible = true;
            grdadddeactivate.DataSource = dsDoc;
            grdadddeactivate.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdadddeactivate.DataSource = dsDoc;
            grdadddeactivate.DataBind();
        }

    }
    private void FillLeave()
    {
        grdLeave.DataSource = null;
        grdLeave.DataBind();

        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.getLeave_approve(sfCode, 2, div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            grdLeave.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
    }
    private void FillTourPlan()
    {
        //TourPlan tp = new TourPlan();
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Pending_Approval(sfCode, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            //if (strGetMR.Substring(0, 2) != "MR")
            //{
                grdTP_Calander.Visible = true;
                grdTP_Calander.DataSource = dsTP;
                grdTP_Calander.DataBind();

        //    }
        //    else
        //    {
        //        btnHome.Visible = true;
        //        grdTP.Visible = true;
        //        grdTP.DataSource = dsTP;
        //        grdTP.DataBind();
        //    }
        }
        else
        {
            btnHome.Visible = true;
            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }


    }

    //Populate the Secondary Sales grid which are waiting for approval
    private void FillSecSales()
    {
        grdSecSales.DataSource = null;
        grdSecSales.DataBind();
        SecSale ss = new SecSale();
        //Get the approval required list

        if (sfCode.Contains("MR"))
        {
            DataSet dsMGR = ss.GetSecSale_MGR(sfCode);

            if (dsMGR.Tables[0].Rows.Count > 0)
            {
                string ReportingTo = dsMGR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                sfCode = ReportingTo;
            }
        }

        dsSecSales = ss.get_SecSales_Pending_Approval(sfCode, 1, div_code);
        if (dsSecSales.Tables[0].Rows.Count > 0)
        {
            grdSecSales.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
        else
        {
            btnHome.Visible = true;
            grdSecSales.DataSource = dsSecSales;
            grdSecSales.DataBind();
        }
    }

    protected void grdSecSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //7
            //e.Row.Cells[7].Text = "sdf";
            //lblMonth.Text = getMonthName(lblMonth.Text);

        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ActTerrtotal = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            lblMonth.Text = getMonthName(lblMonth.Text);
            // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
            ActTerrtotal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sf_code"));
            if (ActTerrtotal.Contains("MR"))
            {
                if(div_code =="17")
                {
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[9].Visible = false;
                }
                else
                {
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;

                }

                
            }
            else
            {
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
        }

    }
    private string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "January";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "3")
        {
            sReturn = "March";
        }
        else if (sMonth == "4")
        {
            sReturn = "April";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "June";
        }
        else if (sMonth == "7")
        {
            sReturn = "July";
        }
        else if (sMonth == "8")
        {
            sReturn = "August";
        }
        else if (sMonth == "9")
        {
            sReturn = "September";
        }
        else if (sMonth == "10")
        {
            sReturn = "October";
        }
        else if (sMonth == "11")
        {
            sReturn = "November";
        }
        else if (sMonth == "12")
        {
            sReturn = "December";
        }

        return sReturn;
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("MGR_Home.aspx");
        if (Session["sf_type"].ToString() == "1") // MR Login
        {

            Response.Redirect("Default_MR.aspx");

        }
        else if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            //TP_New tp = new TP_New();
            //DataSet dsTP = new DataSet();

            //dsTP = tp.get_lastTPdate_forTpValida(sfCode, div_code);

            //DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
            //string sMonthName1 = dtDate1.ToString();

            //string tp_month = dtDate1.ToString("MMM");

            //string tp_month1 = dtDate1.ToString();

            //string[] tp_month3 = tp_month1.Split('/');

            //tp_month4 = Convert.ToInt32(tp_month3[1]);
            //from_year = tp_month3[2];
            //from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


            //string next_month1 = DateTime.Now.AddMonths(1).ToString();

            //string[] next_month2 = next_month1.Split('/');

            //next_month4 = Convert.ToInt32(next_month2[1]);
            //to_year = next_month2[2];
            //to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

            //string Current_month = DateTime.Now.Month.ToString("MMM");
            //string next_month = DateTime.Now.AddMonths(1).ToString("MMM");

            //AdminSetup adm = new AdminSetup();
            //dsTp = adm.chk_tpbasedsystem_MGR(div_code);

            ////if (dsTp.Tables[0].Rows.Count > 0)
            ////{
            //if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            //{
            //    AdminSetup ad = new AdminSetup();
            //    dsDesig = ad.chkRange_tpbased_MGR(div_code, Session["Designation_Short_Name"].ToString());
            //    if (dsDesig.Tables[0].Rows.Count > 0)
            //    {

            //        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
            //        {
            //            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            //            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

            //            DateTime dt = DateTime.Now;
            //            int day = dt.Day;
            //            int curr_month = dt.Month;
            //            int curr_year = dt.Year;

            //            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
            //            {

            //                var now = DateTime.Now;
            //                var startOfMonth = new DateTime(now.Year, now.Month, 1);
            //                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


            //                if (startdate <= day && enddate >= day)
            //                {
            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();


            //                    if (tp_month4 == curr_month && from_year2 == curr_year)
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sfCode, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {

            //                                isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, next_month4, to_year2);

            //                                if (isRepSt == false)
            //                                {
            //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                    //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sfCode, div_code, Convert.ToDateTime(lastDay_curr_mon));

            //                                    iReturn = tp.update_tpdate_tpvalidation(sfCode, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {

            //                        dsReject = tp_new.IsTpChk_Rejection(sfCode, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, next_month4, to_year2);

            //                            if (isRepSt == false)
            //                            {
            //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sfCode, div_code, Convert.ToDateTime(lastDay_curr_mon));

            //                                iReturn = tp.update_tpdate_tpvalidation(sfCode, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
            //                                }
            //                            }
            //                        }

            //                    }
            //                }
            //                else if (enddate <= DaysInMonth && enddate <= day)
            //                {
            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();

            //                    if (tp_month4 == curr_month && from_year2 == curr_year)
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sfCode, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {
            //                                isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, next_month4, to_year2);//chk next month tp

            //                                if (isRepSt == false)
            //                                {
            //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sfCode, div_code, Convert.ToDateTime(current_month));

            //                                    iReturn = tp.update_tpdate_tpvalidation(sfCode, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sfCode, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, next_month4, to_year2);//chk next month tp

            //                            if (isRepSt == false)
            //                            {
            //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sfCode, div_code, Convert.ToDateTime(current_month));

            //                                iReturn = tp.update_tpdate_tpvalidation(sfCode, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
            //                                }
            //                            }
            //                        }
            //                    }

            //                }
            //                else
            //                {

            //                    var today = DateTime.Today;
            //                    var month = new DateTime(today.Year, today.Month, 1);
            //                    var first = month.AddMonths(-1);
            //                    var last_month_date = month.AddDays(-1);

            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();

            //                    dsReject = tp_new.IsTpChk_Rejection(sfCode, div_code);//chk rejection


            //                    if (dsReject.Tables[0].Rows.Count > 0)
            //                    {
            //                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                    }
            //                    else
            //                    {

            //                        isRepSt = tp_new.IsTpChk_NextMonth(sfCode, div_code, curr_month, curr_year);//chk current month tp

            //                        if (isRepSt == false)
            //                        {

            //                            iReturn = tp.insertDelete_tpdate_tpvalidation(sfCode, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

            //                            if (tp_month4 == curr_month && from_year2 == curr_year)
            //                            {
            //                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {
            //                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
            //                                iReturn = tp.update_tpdate_tpvalidation(sfCode, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                                }
            //                            }
            //                        }
            //                    }

            //                }


            //            }

            //        }


            //    }


            //}



            //   Response.Redirect("~/MGR_Home.aspx");
            //Response.Redirect("~/MGR_Dashboard.aspx");
            Server.Transfer("~/MGR_Home.aspx");

        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDoc();
        FillDcr();
        FillDoc_Deact();
        FillTourPlan();
        FillLeave();
        FillDoc_AddDeactivate();
        FillExp1();
        FillSecSales();
    }
    
}
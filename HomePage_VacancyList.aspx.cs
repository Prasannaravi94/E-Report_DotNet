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
using System.Data.SqlClient;

public partial class HomePage_VacancyList : System.Web.UI.Page
{ 
    string sf_type = string.Empty; 
    string div_code = string.Empty;
    string sf_name = string.Empty;
    string sf_code = string.Empty;
    string hq = string.Empty;
    string desig = string.Empty;
    DataTable dt = new DataTable();
    DataSet dsFF = null;
    DataSet dsState = null;
    DataSet dstpstatus = null;
    string entry_date;
    string confirm_date;

    string constr;
    static string divcode = string.Empty;
    string Process_type = string.Empty;
    string sfcode = string.Empty;
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    DataSet dsDesig = new DataSet();
    string subdiv = string.Empty;
    string[] Distinct;
    string[] strsplit;
    string sSub = string.Empty;
    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string mode = string.Empty;
    static string sSfName = string.Empty;
    static string FMName = string.Empty;
    static string spec = string.Empty;
    static string SubDiv_Code = string.Empty;
    static string MGR_Code = string.Empty;
    string Trans_month_year = string.Empty;
    SqlConnection con = new SqlConnection();
    SqlDataAdapter adapt;
    DataSet dsLogin = null;
    DataSet dsSalesForce = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsImage = new DataSet();
    DataSet dsBirth = new DataSet();
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdm = null;
    DataSet dsadmn = null;
    DataSet dsAdmNB = null;
    DataSet dsLogin1 = null;
    DataSet dspwd = new DataSet();
    DataSet dsDoc = null;
    DataSet dsFeed = new DataSet();
    DataSet dsVacant = new DataSet();
    DataSet dssample = null;
    DataSet dsinput = null;
    DataSet dsvac = null;
    string pwdDt = string.Empty;
    string month = string.Empty;
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    int time;
    int tp_month4;
    string from_year = string.Empty;
    int from_year2;
    int next_month4;
    int to_year2;
    string to_year = string.Empty;
    DataSet dsTp = null;
    private DataSet dsinput1;
    DataSet dsquiz = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();   
        sf_code = Session["Sf_code"].ToString();
        sf_name = Session["Sf_name"].ToString();
        sf_type = Session["sf_type"].ToString();
        hq = Session["sf_hq"].ToString();
        desig = Session["Designation_Short_Name"].ToString();
        FillSalesForce();
         

        lblHead.Text = "Vacant Status List - " + "<span style='color:red'> " + sf_name + "  </span>";

        lblFieldForce.Text = "Field Force Name : " + "<span style='color:green'> " + sf_name + " - " + hq + " - " + desig +  "  </span>";

    }
    private void FillSalesForce()
    { 
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("sp_SalesForceVacMrAgainstMgr", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", div_code);
        cmd.Parameters.AddWithValue("@sf_code", sf_code);



        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        //ds.Tables[0].Columns.Remove("");
        //ds.Tables[0].Columns.Remove("");

        dt = ds.Tables[0];
        con.Close();
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdvac.DataSource = ds;
            grdvac.DataBind();
        }
    }
    protected void grdvac_row_databound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lbldesig");


            e.Row.Attributes.Add("style", "background-Color:" + "#" + Convert.ToString(lbl.Text));


        }
    } 
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Export.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptVacantStatusView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    private void BindImage1()
    {
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        // SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sf_code + ',' + "%' or SF_CODE like '%" + ',' + sf_code + ',' + "%'  ) AND Effective_To >= getDate() ", con);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  DataSet dsImage = new DataSet();
        da.Fill(dsImage);
        con.Close();

    }


    //private void BindImage1()
    //{
    //    div_code = Session["div_code"].ToString();

    //    if (div_code.Contains(','))
    //    {
    //        div_code = div_code.Remove(div_code.Length - 1);
    //    }
    //    SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    //  DataSet dsImage = new DataSet();
    //    da.Fill(dsImage);
    //    con.Close();

    //}
    private void BindImage_FieldForce()
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }


        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where sf_code='" + sf_code + "' and Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsImage_FF);
        con.Close();
    }

    protected void btnnxt_Click(object sender, EventArgs e)
    {
        if(Session["sf_type"].ToString()=="2")
        {
            //UserLogin astp = new UserLogin();
            //int iRet = astp.Login_details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
            AdminSetup adm_Nb = new AdminSetup();
            dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
            ListedDR lstDr = new ListedDR();
            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            string sMonthName = dtDate.ToString("MMM");
            TP_New tp = new TP_New();
            DataSet dsTP = new DataSet();
            // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);



            DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
            string sMonthName1 = dtDate1.ToString();

            string tp_month = dtDate1.ToString("MMM");

            string tp_month1 = dtDate1.ToString();

            string[] tp_month3 = tp_month1.Split('/');

            tp_month4 = Convert.ToInt32(tp_month3[1]);
            from_year = tp_month3[2];
            from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


            string next_month1 = DateTime.Now.AddMonths(1).ToString();

            string[] next_month2 = next_month1.Split('/');

            next_month4 = Convert.ToInt32(next_month2[1]);
            to_year = next_month2[2];
            to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

            string Current_month = DateTime.Now.Month.ToString("MMM");
            string next_month = DateTime.Now.AddMonths(1).ToString("MMM");

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last_month_date = month.AddDays(-1);



            var threemth_back = month.AddMonths(-3);
            DateTime three_month_ago = Convert.ToDateTime(threemth_back);
            DateTime three_mnth_ago_lst_date = new DateTime(three_month_ago.Year, three_month_ago.Month, 1).AddMonths(1).AddDays(-1);

            var twomth_back = month.AddMonths(-2);
            DateTime two_month_ago = Convert.ToDateTime(twomth_back);
            DateTime two_mnth_ago_lst_date = new DateTime(two_month_ago.Year, two_month_ago.Month, 1).AddMonths(1).AddDays(-1);

            int two_mnt = two_mnth_ago_lst_date.Month;
            int tw_year = two_mnth_ago_lst_date.Year;

            int one_mnt = last_month_date.Month;
            int onemth_year = last_month_date.Year;


            dsTp = adm.chk_tpbasedsystem_MGR(div_code);
            if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            {
                AdminSetup ad = new AdminSetup();
                SalesForce sff = new SalesForce();
                DataSet dsde = new DataSet();
                dsde = sff.getDesignation_BulkEdit(sf_code, div_code);
                if (dsde.Tables[0].Rows.Count > 0)
                {
                    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsde.Tables[0].Rows[0]["Designation_Code"].ToString()));
                }
                if (dsDesig.Tables[0].Rows.Count > 0)
                {

                    if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                    {
                        int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        DateTime dt = DateTime.Now;
                        int day = dt.Day;
                        int curr_month = dt.Month;
                        int curr_year = dt.Year;

                        if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                        {

                            var now = DateTime.Now;
                            var startOfMonth = new DateTime(now.Year, now.Month, 1);
                            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                            var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                            if (startdate <= day && enddate >= day)//range
                            {
                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;

                                DataSet dsReject = new DataSet();


                                ///start here for check last two months check
                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                        }

                                    }


                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                    }
                                    else
                                    {

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

                                        }
                                        else
                                        {


                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (tp_month4 == curr_month && from_year2 == curr_year)
                                {

                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp


                                    if (isRepSt == false)
                                    {
                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                    }
                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                            }
                                        }
                                    }

                                }

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                            }
                                        }
                                    }

                                }
                            }//end of range
                            else if (enddate <= DaysInMonth && enddate <= day)
                            {
                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();


                                ///start here for check last two months check
                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                        }

                                    }

                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }
                                        }
                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {

                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                    }
                                    else
                                    {

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {

                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                //Newly Added by Vasanthi-Begin
                                else if (tp_month4 == curr_month && from_year2 == curr_year)
                                {


                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                    if (isRepSt == false)
                                    {
                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                        string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                        if (iReturn > 0)
                                        {

                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                        }
                                    }
                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                            string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                            }
                                        }
                                    }
                                }
                                //Newly Added by Vasanthi-End
                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                            string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                            }
                                        }
                                    }
                                }


                            }
                            else
                            {



                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();

                                dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                if (dsReject.Tables[0].Rows.Count > 0)
                                {
                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                }

                                else
                                {

                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                if (tp_month4 == curr_month && from_year2 == curr_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                                else
                                                {
                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                            if (tp_month4 == curr_month && from_year2 == curr_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                            }
                                            else
                                            {
                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                        }
                                    }
                                }
                                //end here
                            }
                        }
                    }
                }
            }
            //end start

            BindImage1();
            BindImage_FieldForce();
            //  Response.Redirect("Sale_Dashboard.aspx");
            AdminSetup admquiz = new AdminSetup();
            dsquiz = admquiz.Get_Quiz_Process(Session["div_code"].ToString(), Session["sf_code"].ToString());
            if (dsquiz.Tables[0].Rows.Count > 0)
            {
                if (dsquiz.Tables[0].Rows[0]["res"].ToString() == "1" || dsquiz.Tables[0].Rows[0]["res"].ToString() == null || dsquiz.Tables[0].Rows[0]["res"].ToString() == "")
                {
                    Response.Redirect("Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + " &res=" + dsquiz.Tables[0].Rows[0]["res"].ToString() + "");
                }
            }
            if (Count != 0)
            {
                //System.Threading.Thread.Sleep(time);
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            //else if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    Response.Redirect("HomePage_Image.aspx");
            //}
            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //{
            //    Server.Transfer("HomePage_FieldForcewise.aspx");

            //}
            else if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("Quote_Design.aspx");

            }
            else if (dsAdmNB.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("NoticeBoard_design.aspx");

            }
            else if (dsAdm.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("FlashNews_Design.aspx");
            }

            else if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("Birthday_Wish.aspx");
            }

            else
            {
                Server.Transfer("~/Default_MGR.aspx");
            }
        }

        else if(Session["sf_type"].ToString()=="1")
        {

        }
    }
}
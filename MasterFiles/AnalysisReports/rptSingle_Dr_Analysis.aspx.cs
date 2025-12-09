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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using DBase_EReport;
public partial class MasterFiles_AnalysisReports_rptSingle_Dr_Analysis : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string drcode = string.Empty;
    string sf_type = string.Empty;
    string division_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowdt = new System.Data.DataTable();
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string[] Distinct;
    int totcount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            div_code = Request.QueryString["div_code"].ToString();

            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            TYear = Request.QueryString["To_year"].ToString();
            drcode = Request.QueryString["drcode"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            if (Request.QueryString["Dashboard"] != null)//Dashboard
            {
                pnlbutton.Visible = false;
            }

            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Single Dr Analysis From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            Filldoc();
            CreateDynamicTable();
            ProductSample();
            ListeddrProd();
            Input();
            GetFeedback();
            Chemist();
            GetRCPA();
            CRM();
            Business();
        }
    }
    private void CreateDynamicTable()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        //
        string mnt = Convert.ToString(months);
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        int j = 0;
        DataTable SfCodes = sf1.getMRJointWork_camp(div_code, sf_code, 0);
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("sf_code");
        for (int i = 0; i < SfCodes.Rows.Count; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"]);
        }
        dsmgrsf.Tables.Add(SfCodes);
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Single_Doctor_Visit";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.Parameters.AddWithValue("@dr_code", drcode);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dtrowdt = dsts.Tables[1].Copy();

        /*
        result = dsts.Tables[1].AsEnumerable()
       .GroupBy(row => row.Field<string>("VST"))
       .Select(g => g.CopyToDataTable()).ToList();
        */
        dsts.Tables[0].Columns.RemoveAt(8);
        //dsts.Tables[0].Columns.RemoveAt(5);
        //dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[1].Columns.RemoveAt(0);
        if (dsts.Tables[1].Rows.Count > 1)
        {
            dsts.Tables[1].Rows.RemoveAt(1);
        }
        else
        {
            dsts.Tables[1].Rows.RemoveAt(0);
        }
        GrdFixation.DataSource = dsts.Tables[1];
        GrdFixation.DataBind();
    }
    private void Filldoc()
    {
        ListedDR lstdr = new ListedDR();
        dsDoc = lstdr.getListedDr_Single(sf_code, drcode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            lblName.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            lblAddress.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            lblmob.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            lblEm.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            lblHospital.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            lblCat.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            lblSpc.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            lblcls.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            lblqua.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            lblcampaign.Text = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
        }
    }
    private void ProductSample()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;

        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;

        //    tbl.Rows.Add(tr_header);

        TableRow tr_catg1 = new TableRow();
        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {
                TableCell tc_month = new TableCell();
                // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                Literal lit_month = new Literal();
                // SalesForce sf = new SalesForce();
                lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                //tc_month.Style.Add("font-family", "Calibri");
                tc_month.Style.Add("font-size", "10pt");
                //tc_month.Attributes.Add("Class", "tr_det_head");              
                //tc_month.Style.Add("border-color", "Black");
                //tc_month.BorderWidth = 1;
                tc_month.ColumnSpan = 2;
                tc_month.HorizontalAlign = HorizontalAlign.Center;
                tc_month.Width = 300;
                tc_month.Controls.Add(lit_month);
                tr_header.Cells.Add(tc_month);
                // tr_catg1.Cells.Add(tc_month);
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
        }


        tblProduct.Rows.Add(tr_header);
        TableRow tr_lst_det = new TableRow();
        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {
                TableCell tc_lst_month = new TableCell();
                HyperLink lit_lst_month = new HyperLink();
                lit_lst_month.Text = "Detailed";
                tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#bad4f5");
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.BorderWidth = 1;
                tc_lst_month.Height = 30;

                //tc_lst_month.Style.Add("font-family", "Calibri");
                tc_lst_month.Style.Add("font-size", "10pt");
                //tc_lst_month.Style.Add("Color", "White");
                tc_lst_month.Style.Add("border-color", "#DCE2E8");
                tc_lst_month.Controls.Add(lit_lst_month);
                tr_lst_det.Cells.Add(tc_lst_month);




                TableCell tc_msd_month = new TableCell();
                HyperLink lit_msd_month = new HyperLink();
                lit_msd_month.Text = "Sampled";             

                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");
                tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#bad4f5");
                tc_msd_month.BorderWidth = 1;
                tc_msd_month.Height = 30;
                //tc_msd_month.Style.Add("Color", "White");
                tc_msd_month.Style.Add("border-color", "#DCE2E8");
                //tc_msd_month.Style.Add("font-family", "Calibri");
                tc_msd_month.Style.Add("font-size", "10pt");
                tc_msd_month.Controls.Add(lit_msd_month);
                tr_lst_det.Cells.Add(tc_msd_month);



                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
                tblProduct.Rows.Add(tr_lst_det);
            }
            int cmonthdoc = Convert.ToInt32(FMonth);
            int cyeardoc = Convert.ToInt32(FYear);

            DataSet dsdoc = new DataSet();
            DataSet dsmet = new DataSet();
            SalesForce dcrdoc = new SalesForce();
            DCR dcr1 = new DCR();
            string Final_Product = "";

            TableRow tr_det = new TableRow();

            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    strproduct = "";
                    Final_Product = "";
                    strSample = "";
                    DateTime dtCurrent;
                    if (cmonthdoc == 12)
                    {
                        sCurrentDate = "01-01-" + (cyeardoc + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonthdoc + 1) + "-01-" + cyeardoc;
                        //sCurrentDate = cmonth  + "-01-" + cyear;
                    }

                    dtCurrent = Convert.ToDateTime(sCurrentDate);
                    Doctor doc = new Doctor();
                    dsdoc = doc.getProduct_Sam(div_code, drcode, sf_code, cmonthdoc, cyeardoc);
                    TableCell tc_det_confirm = new TableCell();
                    Literal lit_det_confirm = new Literal();
                    Literal lit_det_FF = new Literal();
                    //for (int i = 0; i < dsdoc.Tables[0].Rows.Count; i++)
                    //{

                    //    strproduct += dsdoc.Tables[0].Rows[i]["Product_Detail"].ToString().Replace("#", ",").Trim() + ",";
                    //}
                    ////    lit_det_confirm.Text = strproduct;
                    //string[] strwith = strproduct.Split(',');
                    //Distinct = strproduct.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ////  Distinct = Final_Code.Replace(" ", "").Split(',');
                    //string[] distinctArray = RemoveDuplicates(Distinct);

                    //foreach (string st in distinctArray)
                    //{
                    //    if (st.Contains("~0") || st.Contains("~$"))
                    //    {
                    //        Final_Product += st + "<br/>";
                    //    }
                    //    else
                    //    {
                    //        strSample += st + "<br/>";
                    //    }
                    //}
                    for (int i = 0; i < dsdoc.Tables[0].Rows.Count; i++)
                    {
                        if (dsdoc.Tables[0].Rows[i]["Product_Detail_Name"].ToString() != "" && dsdoc.Tables[0].Rows[i]["sample"].ToString() == "0")
                        {
                            Final_Product += dsdoc.Tables[0].Rows[i]["Product_Detail_Name"].ToString() + "<br/>";
                        }
                        else if (dsdoc.Tables[0].Rows[i]["Product_Detail_Name"].ToString() != "" && dsdoc.Tables[0].Rows[i]["sample"].ToString() != "0")
                        {
                            strSample += dsdoc.Tables[0].Rows[i]["Product_Detail_Name"].ToString() + " - " + dsdoc.Tables[0].Rows[i]["sample"].ToString() + "<br/>";
                        }
                    }

                   
                    tc_det_confirm.BorderWidth = 1;
                    tc_det_confirm.HorizontalAlign = HorizontalAlign.Left;
                    lit_det_confirm.Text = Final_Product; 
                    tc_det_confirm.Controls.Add(lit_det_confirm);
                    tr_det.Cells.Add(tc_det_confirm);

                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();

                   
                    tc_det_FF.BorderWidth = 1;
                    lit_det_FF.Text = strSample;
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tc_det_FF.HorizontalAlign = HorizontalAlign.Left;

                    tr_det.Cells.Add(tc_det_FF);




                    cmonthdoc = cmonthdoc + 1;
                    if (cmonthdoc == 13)
                    {
                        cmonthdoc = 1;
                        cyeardoc = cyeardoc + 1;
                    }
                }
            }

            tblProduct.Rows.Add(tr_det);



        }
    }
    private void ListeddrProd()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;
    
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        ListedDR lst = new ListedDR();
        dsSal = lst.get_Prod_map(drcode, sf_code);
        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;
        if (dsSal != null)
        {
            //    tbl.Rows.Add(tr_header);
            TableCell tc_FF = new TableCell();
         
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 300;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Product Tagged</b></center>";
            //tc_FF.Style.Add("border-color", "Black");
            //tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt");
            tc_FF.Controls.Add(lit_FF);

            tr_header.Cells.Add(tc_FF);
            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                    Literal lit_month = new Literal();
                    // SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                    //tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    //tc_month.Attributes.Add("Class", "tr_det_head");                  
                    //tc_month.Style.Add("border-color", "Black");
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Width = 300;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }


            tblLstProd.Rows.Add(tr_header);
          

            foreach (DataRow drFF in dsSal.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
                lit_det_user.Text = drFF["Product_Name"].ToString();
                tc_det_user.HorizontalAlign = HorizontalAlign.Left;
              
                tc_det_user.BorderWidth = 1;
                tc_det_user.Controls.Add(lit_det_user);
                tr_det.Cells.Add(tc_det_user);
                int cmonthdoc = Convert.ToInt32(FMonth);
                int cyeardoc = Convert.ToInt32(FYear);
                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {




                        TableCell tc_msd_month = new TableCell();
                        Literal lit_msd_month = new Literal();
                        dsDoctor = lst.get_Prod_Analysis(drcode, cmonthdoc, cyeardoc, drFF["Product_Code_SlNo"].ToString(), sf_code);
                        if(dsDoctor.Tables[0].Rows.Count>0)
                            lit_msd_month.Text = dsDoctor.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        
              

                        tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                        //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");
                   
                        tc_msd_month.BorderWidth = 1;
                   
                        //tc_msd_month.Style.Add("border-color", "Black");
                        //tc_msd_month.Style.Add("font-family", "Calibri");
                        tc_msd_month.Style.Add("font-size", "10pt");
                        tc_msd_month.Controls.Add(lit_msd_month);
                        tr_det.Cells.Add(tc_msd_month);



                        cmonthdoc = cmonthdoc + 1;
                        if (cmonthdoc == 13)
                        {
                            cmonthdoc = 1;
                            cyeardoc = cyeardoc + 1;
                        }
                      
                    }

                  
                }



                tblLstProd.Rows.Add(tr_det);

            }
        }
    }

    private void GetFeedback()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;
  
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int monthF = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonthF = Convert.ToInt32(FMonth);
        int cyearF = Convert.ToInt32(FYear);
        ListedDR lst = new ListedDR();
        dsSal = lst.get_Prod_map(drcode, sf_code);
        ViewState["months"] = monthF;
        ViewState["cmonth"] = cmonthF;
        ViewState["cyear"] = cyearF;
  
          
            TableRow tr_catg1 = new TableRow();
            if (monthF >= 0)
            {

                for (int j = 1; j <= monthF + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                    Literal lit_month = new Literal();
                    // SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonthF.ToString()).Substring(0, 3) + "-" + cyearF;

                    //tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    //tc_month.Attributes.Add("Class", "tr_det_head");
                
                    //tc_month.Style.Add("border-color", "Black");
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Width = 300;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonthF = cmonthF + 1;
                    if (cmonthF == 13)
                    {
                        cmonthF = 1;
                        cyearF = cyearF + 1;
                    }
                }
            }


            tblFeed.Rows.Add(tr_header);
            int cmonthdoc = Convert.ToInt32(FMonth);
            int cyeardoc = Convert.ToInt32(FYear);

                TableRow tr_det = new TableRow();               
                if (monthF >= 0)
                {

                    for (int j = 1; j <= monthF + 1; j++)
                    {

                        TableCell tc_msd_month = new TableCell();
                        Literal lit_msd_month = new Literal();
                        dsDoctor = lst.getDoc_Feedback(div_code, drcode, sf_code, cmonthdoc, cyeardoc);
                        if (dsDoctor.Tables[0].Rows.Count > 0)
                            lit_msd_month.Text = dsDoctor.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                     

                        tc_msd_month.HorizontalAlign = HorizontalAlign.Left;
                        //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");

                        tc_msd_month.BorderWidth = 1;

                        //tc_msd_month.Style.Add("border-color", "Black");
                        //tc_msd_month.Style.Add("font-family", "Calibri");
                        tc_msd_month.Style.Add("font-size", "10pt");
                        tc_msd_month.Controls.Add(lit_msd_month);
                        tr_det.Cells.Add(tc_msd_month);



                        cmonthdoc = cmonthdoc + 1;
                        if (cmonthdoc == 13)
                        {
                            cmonthdoc = 1;
                            cyeardoc = cyeardoc + 1;
                        }

                    }

                    tblFeed.Rows.Add(tr_det);
                }



              
            
       
    }
    public string[] RemoveDuplicates(string[] myList)
    {
        System.Collections.ArrayList newList = new System.Collections.ArrayList();

        foreach (string str in myList)
            if (!newList.Contains(str))
                newList.Add(str);
        return (string[])newList.ToArray(typeof(string));
    }
    private void Input()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;
     
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;

        //    tbl.Rows.Add(tr_header);

        TableRow tr_catg1 = new TableRow();
        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {
                TableCell tc_month = new TableCell();
                // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                Literal lit_month = new Literal();
                // SalesForce sf = new SalesForce();
                lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                //tc_month.Style.Add("font-family", "Calibri");
                tc_month.Style.Add("font-size", "10pt");
                //tc_month.Attributes.Add("Class", "tr_det_head");
                
                //tc_month.Style.Add("border-color", "Black");
                tc_month.BorderWidth = 1;
                tc_month.ColumnSpan = 2;
                tc_month.HorizontalAlign = HorizontalAlign.Center;
                tc_month.Width = 300;
                tc_month.Controls.Add(lit_month);
                tr_header.Cells.Add(tc_month);
                // tr_catg1.Cells.Add(tc_month);
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
        }


        tblInput.Rows.Add(tr_header);
        TableRow tr_lst_det = new TableRow();
        if (months >= 0)
        {

            for (int j = 1; j <= months + 1; j++)
            {
                TableCell tc_lst_month = new TableCell();
                HyperLink lit_lst_month = new HyperLink();
                lit_lst_month.Text = "Name";

        

                tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#BAD4F5");
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.BorderWidth = 1;
                tc_lst_month.Height = 30;

                //tc_lst_month.Style.Add("font-family", "Calibri");
                tc_lst_month.Style.Add("font-size", "10pt");
                //tc_lst_month.Style.Add("Color", "White");
                tc_lst_month.Style.Add("border-color", "#DCE2E8");
                tc_lst_month.Controls.Add(lit_lst_month);
                tr_lst_det.Cells.Add(tc_lst_month);




                TableCell tc_msd_month = new TableCell();
                HyperLink lit_msd_month = new HyperLink();
                lit_msd_month.Text = "Qty";
            

                tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");
                tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#BAD4F5");
                tc_msd_month.BorderWidth = 1;
                tc_msd_month.Height = 30;
                //tc_msd_month.Style.Add("Color", "White");
                tc_msd_month.Style.Add("border-color", "#DCE2E8");
                //tc_msd_month.Style.Add("font-family", "Calibri");
                tc_msd_month.Style.Add("font-size", "10pt");
                tc_msd_month.Controls.Add(lit_msd_month);
                tr_lst_det.Cells.Add(tc_msd_month);



                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
                tblInput.Rows.Add(tr_lst_det);
            }
            int cmonthdoc = Convert.ToInt32(FMonth);
            int cyeardoc = Convert.ToInt32(FYear);

            DataSet dsdoc = new DataSet();
            DataSet dsmet = new DataSet();
            SalesForce dcrdoc = new SalesForce();
            DCR dcr1 = new DCR();
            string Final_Product = "";

            TableRow tr_det = new TableRow();

            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    strproduct = "";
                    Final_Product = "";
                    strSample = "";
                    DateTime dtCurrent;
                    if (cmonthdoc == 12)
                    {
                        sCurrentDate = "01-01-" + (cyeardoc + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonthdoc + 1) + "-01-" + cyeardoc;
                        //sCurrentDate = cmonth  + "-01-" + cyear;
                    }

                    dtCurrent = Convert.ToDateTime(sCurrentDate);
                    ListedDR doc1 = new ListedDR();
                    dsdoc = doc1.getDoc_Input(div_code, drcode, sf_code, cmonthdoc, cyeardoc);
                    TableCell tc_det_confirm = new TableCell();
                    Literal lit_det_confirm = new Literal();
                    Literal lit_det_FF = new Literal();
                    for (int i = 0; i < dsdoc.Tables[0].Rows.Count; i++)
                    {
                        if (dsdoc.Tables[0].Rows[i]["Gift_Name"].ToString() != "" && dsdoc.Tables[0].Rows[i]["Gift_Qty"].ToString() != "0")
                        {
                            strproduct += dsdoc.Tables[0].Rows[i]["Gift_Name"].ToString() + "<br/>";
                            strSample += dsdoc.Tables[0].Rows[i]["Gift_Qty"].ToString() + "<br/>";
                        }
                      
                      
                      
                    }
                    //    lit_det_confirm.Text = strproduct;
                    string[] strwith = strproduct.Split(',');
                    Distinct = strproduct.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //  Distinct = Final_Code.Replace(" ", "").Split(',');
                    string[] distinctArray = RemoveDuplicates(Distinct);

                    //foreach (string st in distinctArray)
                    //{
                        
                    //        Final_Product += st + "<br/>";
                       
                    //}


                    tc_det_confirm.BorderWidth = 1;
                    tc_det_confirm.HorizontalAlign = HorizontalAlign.Left;
                    lit_det_confirm.Text = strproduct;
                    tc_det_confirm.Controls.Add(lit_det_confirm);
                    tr_det.Cells.Add(tc_det_confirm);

                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();

                    tc_det_FF.BorderWidth = 1;
                    lit_det_FF.Text = strSample;
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tc_det_FF.HorizontalAlign = HorizontalAlign.Left;

                    tr_det.Cells.Add(tc_det_FF);




                    cmonthdoc = cmonthdoc + 1;
                    if (cmonthdoc == 13)
                    {
                        cmonthdoc = 1;
                        cyeardoc = cyeardoc + 1;
                    }
                }
            }

            tblInput.Rows.Add(tr_det);



        }
    }
    private void Chemist()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;
     
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        ListedDR lst = new ListedDR();
        dsSal = lst.get_Chem_map(drcode, sf_code);
        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;
        if (dsSal != null)
        {
            //    tbl.Rows.Add(tr_header);
            TableCell tc_FF = new TableCell();
         
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 300;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Chemist Name</b></center>";
            //tc_FF.Style.Add("border-color", "Black");
            //tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt");
            tc_FF.Controls.Add(lit_FF);

            tr_header.Cells.Add(tc_FF);
            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                    Literal lit_month = new Literal();
                    // SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                    //tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    //tc_month.Attributes.Add("Class", "tr_det_head");                   
                    //tc_month.Style.Add("border-color", "Black");
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Width = 300;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }


            tblChemist.Rows.Add(tr_header);

           
            foreach (DataRow drFF in dsSal.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
                lit_det_user.Text = drFF["chemists_name"].ToString();
                tc_det_user.HorizontalAlign = HorizontalAlign.Left;
                
                tc_det_user.BorderWidth = 1;
                tc_det_user.Controls.Add(lit_det_user);
                tr_det.Cells.Add(tc_det_user);
                int cmonthchem = Convert.ToInt32(FMonth);
                int cyearchem = Convert.ToInt32(FYear);

                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {




                        TableCell tc_msd_month = new TableCell();
                        Literal lit_msd_month = new Literal();
                        dsDoctor = lst.get_Chem_Analysis(drcode, cmonthchem, cyearchem, drFF["Chemists_Code"].ToString(), sf_code);
                        if (dsDoctor.Tables[0].Rows.Count > 0)
                            lit_msd_month.Text = dsDoctor.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                       

                        tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                        //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");

                        tc_msd_month.BorderWidth = 1;

                        //tc_msd_month.Style.Add("border-color", "Black");
                        //tc_msd_month.Style.Add("font-family", "Calibri");
                        tc_msd_month.Style.Add("font-size", "10pt");
                        tc_msd_month.Controls.Add(lit_msd_month);
                        tr_det.Cells.Add(tc_msd_month);



                        cmonthchem = cmonthchem + 1;
                        if (cmonthchem == 13)
                        {
                            cmonthchem = 1;
                            cyearchem = cyearchem + 1;
                        }

                    }


                }



                tblChemist.Rows.Add(tr_det);

            }
        }
    }

    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;


            //Creating a gridview row object
            //GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell objtablecell = new TableCell();
            #endregion
            //
            #region Merge cells




            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            SalesForce sf = new SalesForce();
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);




            //iLstColCnt.Add(3);
            for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
            {
                int iCnt = 0;

                TableCell objtablecell = new TableCell();
                AddMergedCells(objgridviewrow, objtablecell, months1 + 1, dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString(), "#4cb6c4", false);

                int j = 1;
                months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                cmonth1 = Convert.ToInt32(FMonth);
                cyear1 = Convert.ToInt32(FYear);
                if (months1 >= 0)
                {

                    for (j = 1; j <= months1 + 1; j++)
                    {


                        TableCell objtablecell2 = new TableCell();
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#4cb6c4", true);
                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }
                }

                //iLstVstmnt.Add(cmonth1);
                //iLstVstyr.Add(cyear1);

            }


            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);


            //
            #endregion
            //
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("color", "white");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        cell.Attributes.Add("title", "Tooltip text for " + cell.Text);
        //    }
        //}


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = 0;
            int m = 1;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;
                //
                #region Calculations
                //
                List<int> sDate = new List<int>();
                int iDateList;
                for (int s = 0; s < dtrowdt.Rows.Count; s++)
                {
                    //if (e.Row.Cells[1].Text == dtrowdt.Rows[s][0].ToString())
                    //{
                    for (i = 0, m = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Text = dtrowdt.Rows[s][m].ToString().Replace("[", "").Replace("]", "");
                        e.Row.Cells[i].Wrap = false;
                        iDateList = 0;
                        sDate.Clear();
                        string[] strSplit = e.Row.Cells[i].Text.Split(',');
                        foreach (string str in strSplit)
                        {
                            if (str != "")
                            {
                                iDateList = Convert.ToInt32(str);
                                sDate.Add(iDateList);
                            }
                        }
                        string sDateList = "";
                        sDate.Sort();
                        foreach (int item in sDate)
                        {
                            sDateList += item.ToString() + ",";
                        }
                       
                        e.Row.Cells[i].Text = sDateList;
                      
                        m++;
                    }
                    i++;

                    break;
                    //}
                }
                #endregion
                //
              
            }

        }
       
    }
    private void GetRCPA()
    {
        Product sf = new Product();

        DataSet dsprod = new DataSet();
        dsprod = sf.Get_Rcpa_Products(div_code, drcode);

        if (dsprod.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();

            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
            tr_header.ForeColor = System.Drawing.Color.White;
            tr_header.Height = 45;

            TableCell tc_Prod = new TableCell();         
            tc_Prod.BorderWidth = 1;
            tc_Prod.Width = 200;
            //tc_Prod.Style.Add("border-color", "Black");
            tc_Prod.Style.Add("font-size", "10pt");
            //tc_Prod.Attributes.Add("Class", "tr_det_head");
            Literal lit_Prod = new Literal();
            lit_Prod.Text = "<center>Our Product</center>";
            tc_Prod.Controls.Add(lit_Prod);          
            tr_header.Cells.Add(tc_Prod);

            //tr_header.Attributes.Add("Class", "tblCellFont");
            TableCell tc_Qty = new TableCell();
           
            tc_Qty.BorderWidth = 1;
            tc_Qty.Width = 150;
            Literal lit_Qty = new Literal();
            lit_Qty.Text = "<center>Qty</center>";
            //tc_Qty.Style.Add("border-color", "Black");
            tc_Qty.Style.Add("font-size", "10pt");
            //tc_Qty.Attributes.Add("Class", "tr_det_head");
            tc_Qty.Controls.Add(lit_Qty);
            tr_header.Cells.Add(tc_Qty);


            TableCell tc_Rate = new TableCell();          
            tc_Rate.BorderWidth = 1;
            tc_Rate.Width = 120;
            Literal lit_Rate = new Literal();
            lit_Rate.Text = "<center>Rate</center>";          
            tc_Rate.Style.Add("font-size", "10pt");
            //tc_Rate.Style.Add("border-color", "Black");
            //tc_Rate.Attributes.Add("Class", "tr_det_head");
            tc_Rate.Controls.Add(lit_Rate);
            tr_header.Cells.Add(tc_Rate);
         
            TableCell tc_com = new TableCell();           
            tc_com.BorderWidth = 1;          
            //tc_DR_Total.Width = 50;
            Literal lit_com = new Literal();
            lit_com.Text = "<center>Competitor Name</center>";
            tc_com.Style.Add("font-size", "10pt");
            //tc_com.Style.Add("border-color", "Black");
            //tc_com.Attributes.Add("Class", "tr_det_head");
            tc_com.Controls.Add(lit_com);
            //  tr_lst_det.Cells.Add(tc_DR_Total);

            tr_header.Cells.Add(tc_com);

            TableCell tc_Brand = new TableCell();            
            tc_Brand.BorderWidth = 1;
            tc_Brand.Style.Add("font-size", "10pt");
            //tc_Brand.Style.Add("border-color", "Black");
            Literal lit_Brand = new Literal();
            lit_Brand.Text = "<center>Competitor Brand</center>";
            //tc_Brand.Attributes.Add("Class", "tr_det_head");
            tc_Brand.Controls.Add(lit_Brand);
            //  tr_lst_det.Cells.Add(tc_DR_Total);

            tr_header.Cells.Add(tc_Brand);

            TableCell tc_ComQty = new TableCell();
          
            tc_ComQty.BorderWidth = 1;
            tc_ComQty.Style.Add("font-size", "10pt");
            //tc_ComQty.Style.Add("border-color", "Black");
            //tc_DR_Total.Width = 50;
            Literal lit_ComQty = new Literal();
            lit_ComQty.Text = "<center>Qty</center>";            
            tc_ComQty.Controls.Add(lit_ComQty);
            //  tr_lst_det.Cells.Add(tc_DR_Total);

            tr_header.Cells.Add(tc_ComQty);

            TableCell tc_ComRate = new TableCell();
           
            tc_ComRate.BorderWidth = 1;
            tc_ComRate.Style.Add("font-size", "10pt");
            //tc_ComRate.Style.Add("border-color", "Black");
            Literal lit_ComRate = new Literal();
            lit_ComRate.Text = "<center>Rate</center>";          
            tc_ComRate.Controls.Add(lit_ComRate);
            //  tr_lst_det.Cells.Add(tc_DR_Total);

            tr_header.Cells.Add(tc_ComRate);

            tblRCPA.Rows.Add(tr_header);


            TableRow tr_det = new TableRow();
            foreach (DataRow drFF in dsprod.Tables[0].Rows)
            {
                 dsSal = sf.Get_Rcpa_Products_Det(div_code, drcode, drFF["ourbrnd"].ToString(), sf_code);
                 if (dsSal.Tables[0].Rows.Count > 0)
                 {
                     totcount = dsSal.Tables[0].Rows.Count;
                 }
                 string[] strwith = drFF["ourBrndNm"].ToString().Split('(');

                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
                lit_det_user.Text = strwith[0].ToString();
                tc_det_user.HorizontalAlign = HorizontalAlign.Left;
             
                tc_det_user.BorderWidth = 1;
                tc_det_user.RowSpan = totcount +1;
                tc_det_user.Controls.Add(lit_det_user);

                tr_det.Cells.Add(tc_det_user);

                TableCell tc_Our_Qty = new TableCell();
                Literal lit_Our_Qty = new Literal();
                lit_Our_Qty.Text = strwith[1].Replace(")", "").ToString();
                tc_Our_Qty.HorizontalAlign = HorizontalAlign.Left;
            
                tc_Our_Qty.BorderWidth = 1;
                tc_Our_Qty.RowSpan = totcount + 1;
                tc_Our_Qty.Controls.Add(lit_Our_Qty);

                tr_det.Cells.Add(tc_Our_Qty);


                tblRCPA.Rows.Add(tr_det);
                TableCell tc_Our_Rate = new TableCell();
                Literal lit_Our_Rate = new Literal();
                lit_Our_Rate.Text = drFF["CmptrPOB"].ToString();
                tc_Our_Rate.HorizontalAlign = HorizontalAlign.Left;
               
                tc_Our_Rate.BorderWidth = 1;
                tc_Our_Rate.RowSpan = totcount + 1;
                tc_Our_Rate.Controls.Add(lit_Our_Rate);

                tr_det.Cells.Add(tc_Our_Rate);
                tblRCPA.Rows.Add(tr_det);
                dsSal = sf.Get_Rcpa_Products_Det(div_code, drcode, drFF["ourbrnd"].ToString(), sf_code);
                if(dsSal.Tables[0].Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dsSal.Tables[0].Rows)
                    {
                     
                        TableRow trProduct = new TableRow();

                        TableCell tc_Com_Name = new TableCell();
                        Literal lit_Com_Name = new Literal();
                        lit_Com_Name.Text = dr["CmptrName"].ToString();
                        tc_Com_Name.HorizontalAlign = HorizontalAlign.Left;
                        
                        tc_Com_Name.BorderWidth = 1;

                        tc_Com_Name.Controls.Add(lit_Com_Name);

                        trProduct.Cells.Add(tc_Com_Name);

                        TableCell tc_Com_Br = new TableCell();
                        Literal lit_Com_Br = new Literal();
                        lit_Com_Br.Text = dr["CmptrBrnd"].ToString();
                        tc_Com_Br.HorizontalAlign = HorizontalAlign.Left;
                    
                        tc_Com_Br.BorderWidth = 1;

                        tc_Com_Br.Controls.Add(lit_Com_Br);

                        trProduct.Cells.Add(tc_Com_Br);

                        TableCell tc_Com_Qty = new TableCell();
                        Literal lit_Com_Qty = new Literal();
                        lit_Com_Qty.Text = dr["CmptrQty"].ToString();
                        tc_Com_Qty.HorizontalAlign = HorizontalAlign.Left;
                     
                        tc_Com_Qty.BorderWidth = 1;

                        tc_Com_Qty.Controls.Add(lit_Com_Qty);

                        trProduct.Cells.Add(tc_Com_Qty);


                        TableCell tc_Com_Rate = new TableCell();
                        Literal lit_Com_Rate = new Literal();
                        lit_Com_Rate.Text = dr["CmptrPriz"].ToString();
                        tc_Com_Rate.HorizontalAlign = HorizontalAlign.Left;
                      
                        tc_Com_Rate.BorderWidth = 1;

                        tc_Com_Rate.Controls.Add(lit_Com_Rate);

                        trProduct.Cells.Add(tc_Com_Rate);

                        tblRCPA.Rows.Add(trProduct);
                       
                    }

                }
              
            }
            
        }
    }
    private void CRM()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;
       
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        ListedDR lst = new ListedDR();
        dsSal = lst.get_Prod_map(drcode, sf_code);
        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;
        if (dsSal != null)
        {
            //    tbl.Rows.Add(tr_header);
            TableCell tc_FF = new TableCell();
          
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 300;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>CRM Given</b></center>";
            //tc_FF.Style.Add("border-color", "Black");
            //tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt");
            tc_FF.Controls.Add(lit_FF);

            tr_header.Cells.Add(tc_FF);
            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                    Literal lit_month = new Literal();
                    // SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;

                    //tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    //tc_month.Attributes.Add("Class", "tr_det_head");
                 
                    //tc_month.Style.Add("border-color", "Black");
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Width = 300;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }


            tblcrm.Rows.Add(tr_header);


            foreach (DataRow drFF in dsSal.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                TableCell tc_det_user = new TableCell();
                Literal lit_det_user = new Literal();
              //  lit_det_user.Text = drFF["Product_Name"].ToString();
                tc_det_user.HorizontalAlign = HorizontalAlign.Left;
             
                tc_det_user.BorderWidth = 1;
                tc_det_user.Controls.Add(lit_det_user);
                tr_det.Cells.Add(tc_det_user);
                int cmonthdoc = Convert.ToInt32(FMonth);
                int cyeardoc = Convert.ToInt32(FYear);
                if (months >= 0)
                {

                    for (int j = 1; j <= months + 1; j++)
                    {




                        TableCell tc_msd_month = new TableCell();
                        Literal lit_msd_month = new Literal();
                        //dsDoctor = lst.get_Prod_Analysis(drcode, cmonthdoc, cyeardoc, drFF["Product_Code_SlNo"].ToString(), sf_code);
                        //if (dsDoctor.Tables[0].Rows.Count > 0)
                        //    lit_msd_month.Text = dsDoctor.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                  

                        tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                        //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");

                        tc_msd_month.BorderWidth = 1;

                        //tc_msd_month.Style.Add("border-color", "Black");
                        //tc_msd_month.Style.Add("font-family", "Calibri");
                        tc_msd_month.Style.Add("font-size", "10pt");
                        tc_msd_month.Controls.Add(lit_msd_month);
                        tr_det.Cells.Add(tc_msd_month);



                        cmonthdoc = cmonthdoc + 1;
                        if (cmonthdoc == 13)
                        {
                            cmonthdoc = 1;
                            cyeardoc = cyeardoc + 1;
                        }

                    }


                }



                tblcrm.Rows.Add(tr_det);

            }
        }
    }
    private void Business()
    {
        TourPlan tp = new TourPlan();
        SalesForce sf = new SalesForce();
        TableRow tr_header = new TableRow();
        string strproduct = string.Empty;
        string strSample = string.Empty;
 
        tr_header.BorderWidth = 1;
        tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#414D55");
        tr_header.ForeColor = System.Drawing.Color.White;
        tr_header.Height = 45;

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        ListedDR lst = new ListedDR();
        dsSal = lst.get_Prod_map(drcode, sf_code);
        ViewState["months"] = months;
        ViewState["cmonth"] = cmonth;
        ViewState["cyear"] = cyear;
        if (dsSal != null)
        {
            //    tbl.Rows.Add(tr_header);
            TableCell tc_FF = new TableCell();
      
            tc_FF.BorderWidth = 1;
            tc_FF.Width = 300;
            Literal lit_FF = new Literal();
            lit_FF.Text = "<center><b>Business Given</b></center>";
            //tc_FF.Style.Add("border-color", "Black");
            //tc_FF.Style.Add("font-family", "Calibri");
            tc_FF.Style.Add("font-size", "10pt");
            tc_FF.Controls.Add(lit_FF);

            tr_header.Cells.Add(tc_FF);
            TableRow tr_catg1 = new TableRow();
            if (months >= 0)
            {

                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;

                    Literal lit_month = new Literal();
                    // SalesForce sf = new SalesForce();
                    lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;
           
                    //tc_month.Style.Add("font-family", "Calibri");
                    tc_month.Style.Add("font-size", "10pt");
                    //tc_month.Attributes.Add("Class", "tr_det_head");                
                    //tc_month.Style.Add("border-color", "Black");
                    tc_month.BorderWidth = 1;

                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_month.Width = 300;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    // tr_catg1.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }


            tblBus.Rows.Add(tr_header);
            Product prod = new Product();
            string value = string.Empty;
            dsSal = prod.Get_Business(div_code, drcode, sf_code);
            if (dsSal.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drFF in dsSal.Tables[0].Rows)
                {
                    TableRow tr_det = new TableRow();
                    TableCell tc_det_user = new TableCell();
                    Literal lit_det_user = new Literal();
                    lit_det_user.Text = drFF["product_code"].ToString();
                    tc_det_user.HorizontalAlign = HorizontalAlign.Left;
                  
                    tc_det_user.BorderWidth = 1;
                    tc_det_user.Controls.Add(lit_det_user);
                    tr_det.Cells.Add(tc_det_user);
                    int cmonthdoc = Convert.ToInt32(FMonth);
                    int cyeardoc = Convert.ToInt32(FYear);
                    Product prod1 = new Product();
                    if (months >= 0)
                    {

                        for (int j = 1; j <= months + 1; j++)
                        {

                            value = "";


                            TableCell tc_msd_month = new TableCell();
                            Literal lit_msd_month = new Literal();
                            dsDoctor = prod1.Get_Business_Det(div_code,drcode, cmonthdoc, cyeardoc, drFF["product_code"].ToString(), sf_code);
                            if (dsDoctor.Tables[0].Rows.Count > 0)

                                value = dsDoctor.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                lit_msd_month.Text = value;                            

                            tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
                            //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#4cb6c4");

                            tc_msd_month.BorderWidth = 1;

                            //tc_msd_month.Style.Add("border-color", "Black");
                            //tc_msd_month.Style.Add("font-family", "Calibri");
                            tc_msd_month.Style.Add("font-size", "10pt");
                            tc_msd_month.Controls.Add(lit_msd_month);
                            tr_det.Cells.Add(tc_msd_month);



                            cmonthdoc = cmonthdoc + 1;
                            if (cmonthdoc == 13)
                            {
                                cmonthdoc = 1;
                                cyeardoc = cyeardoc + 1;
                            }
                            tblBus.Rows.Add(tr_det);
                        }

                       
                    }



                   

                }
            }
        }
    }
}
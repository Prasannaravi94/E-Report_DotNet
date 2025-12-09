using System;
using System.Web.UI.HtmlControls;
using System.Collections;
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
using DBase_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Web.Configuration;
public partial class MasterFiles_Quesionaire_rpt_Questionnaire_View_Zoom : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;

    string sCurrentDate = string.Empty;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    string speciality = string.Empty;
    DataSet dsDes = new DataSet();
    string test = string.Empty;
    string spec_name = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    string sType = string.Empty;
    string sQusType = string.Empty;
    string smode = string.Empty;
    SqlConnection con;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
    string sQuestion = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            smode = Request.QueryString["smode"].ToString();
            if (Request.QueryString["sf"].ToString() == "ALL")
            {
                FMonth = Request.QueryString["cMnth"].ToString();
                FYear = Request.QueryString["cYr"].ToString();
                TMonth = Request.QueryString["TMnth"].ToString();
                TYear = Request.QueryString["TYr"].ToString();

            }
            else
            {
                FMonth = Request.QueryString["cMnth"].ToString();
                FYear = Request.QueryString["cYr"].ToString();
                TMonth = Request.QueryString["cMnth"].ToString();
                TYear = Request.QueryString["cYr"].ToString();
            }

            sType = Request.QueryString["Prod"].ToString();
            sQusType = Request.QueryString["type"].ToString();
            sfname = Request.QueryString["sf_name"].ToString();


            //  lblRegionName.Text = sfname;
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
            string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

            if (smode == "1")
            {
                lblHead.Text = "Questionnaire View  Product Based - " + strFMonthName + " " + FYear + "  To  " + strTMonthName + " " + TYear;
            }
            else if (smode == "2")
            {
                lblHead.Text = "Questionnaire View Competitor Brand Based - " + strFMonthName + " " + FYear + "  To  " + strTMonthName + " " + TYear;
            }
            LblForceName.Text = "Field Force Name : " + sfname;
            if (!Page.IsPostBack)
            {
                FillQues();

                FillQ();


                //FillSF();      

                FillPrd();
            }
        }
    }
    private void FillQues()
    {
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        //cmd = new SqlCommand("select Question_Type_Id,Question_Type_Name,Question_Type_SName from Mas_QuestionType", con);
        if (smode == "1")
        {

            cmd = new SqlCommand("select Question_Id,Question_Text from Trans_Questionnaire_Head where Question_Type_Mode='PB' and Question_Type_SName='" + sQusType.Trim() + "' order by Question_Id", con);
        }
        else if (smode == "2")
        {
            cmd = new SqlCommand("select Question_Id,Question_Text from Trans_Questionnaire_Head where Question_Type_Mode='CB' and Question_Type_SName='" + sQusType.Trim() + "' order by Question_Id", con);
        }
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        lstQues.DataSource = ds;
        lstQues.DataValueField = ds.Tables[0].Columns["Question_Id"].ColumnName;
        lstQues.DataTextField = ds.Tables[0].Columns["Question_Text"].ColumnName;
        lstQues.DataBind();
        //System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem();
        //li.Text = "--ALL--";
        //li.Value = "0";
        //lstQues.Items.Insert(0, li);
    }
    private void FillPrd()
    {
        FMonth = Request.QueryString["cMnth"].ToString();
        FYear = Request.QueryString["cYr"].ToString();
        int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth);
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
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

        DataTable dtQues = new DataTable();
        dtQues.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtQues.Columns["INX"].AutoIncrementStep = 1;
        dtQues.Columns["INX"].AutoIncrementSeed = 1;
        dtQues.Columns.Add("Ques_ID", typeof(int));

        if (dsDes.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDes.Tables[0].Rows.Count; i++)
            {
                dtQues.Rows.Add(null, dsDes.Tables[0].Rows[i]["Question_Id"].ToString());
            }
        }
        DataTable dtProd = new DataTable();
        dtProd.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtProd.Columns["INX"].AutoIncrementSeed = 1;
        dtProd.Columns["INX"].AutoIncrementStep = 1;
        dtProd.Columns.Add("CODE", typeof(int));        
        string Prod = Request.QueryString["Prod"].ToString();
        Prod = Prod.Remove(Prod.LastIndexOf(','));
        string[] ttlProd = Prod.Split(',');

        foreach (string sProd in ttlProd)
        {
            if (sProd != "")
                dtProd.Rows.Add(null, Convert.ToInt32(sProd));
        }
        
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
           string sProc_Name = "";
           if (smode == "1")
           {
               sProc_Name = "Questionnaire_View_Product_Zoom";
           }
           else if (smode == "2")
           {
               sProc_Name = "Questionnaire_View_Comp_Zoom";
           }

           SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@type", sQusType);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Prod", dtProd);
        cmd.Parameters.AddWithValue("@Ques", dtQues);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        //dsts.Tables[0].Columns.RemoveAt(5);
        //dsts.Tables[0].Columns.RemoveAt(3);
        //dsts.Tables[0].Columns.RemoveAt(1);
        if (smode == "1")
        {
            dsts.Tables[0].Columns.Remove("Trans_code");
            dsts.Tables[0].Columns.Remove("ProdCode");
            dsts.Tables[0].Columns.Remove("Prod_Code");
         //   dsts.Tables[0].Columns.Remove("SN");
        }
        else if (smode == "2")
        {
            dsts.Tables[0].Columns.Remove("Comp_code");
            dsts.Tables[0].Columns.Remove("ProdCode");
            dsts.Tables[0].Columns.Remove("Prod_Code");
            dsts.Tables[0].Columns.Remove("comp");
        }
        Grdprd.DataSource = dsts;
        Grdprd.DataBind();

    }
  

    protected void Grdprd_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell2 = new TableCell();
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "S.No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Product Name", "#0097AC", true);
            if (smode == "1")
            {
                if (sQusType.Trim() == "DO")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Doctor Name", "#0097AC", true);
                }
                else if (sQusType.Trim() == "CH")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Chemist Name", "#0097AC", true);
                }
                else if (sQusType.Trim() == "HO")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Hospital Name", "#0097AC", true);
                }
                else if (sQusType.Trim() == "ST")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Stockist Name", "#0097AC", true);
                }
            }
            else
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Competitor Name", "#0097AC", true);
            }
            FMonth = Request.QueryString["cMnth"].ToString();
            FYear = Request.QueryString["cYr"].ToString();
            //TMonth = Request.QueryString["TMnth"].ToString();
            //TYear = Request.QueryString["TYr"].ToString();
            int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);
            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);

            //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

            SalesForce sf = new SalesForce();

           
            FillQ();

            for (int i = 0; i <= months; i++)
            {
                iLstMonth.Add(cmonth);
                iLstYear.Add(cyear);
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, dsDes.Tables[0].Rows.Count, sTxt.Trim(), "#0097AC", true);

               


                if (dsDes.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < dsDes.Tables[0].Rows.Count; j++)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDes.Tables[0].Rows[j]["Question_Text"].ToString(), "#0097AC", true);
                    }

                }


                cmonth = cmonth + 1;

                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //
            #endregion
            //
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Height = 20;
        objtablecell.Font.Size = 9;
        objtablecell.Font.Bold = true;

        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
  
        objgridviewrow.Cells.Add(objtablecell);
    }



    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //if (test == e.Row.Cells[2].Text)
            //{
            //   // e.Row.Cells[2].BorderColor = System.Drawing.Color.White;
            //    e.Row.Cells[2].Text = "";
            //    //lblsf_name.Text = "";
            //   // e.Row.Cells[2].BorderColor = System.Drawing.Color.White;


            //}
            //else
            //{
            //    //e.Row.Cells[2].BorderColor = System.Drawing.Color.White;
            //    test = e.Row.Cells[2].Text;
            //    //e.Row.Cells[2].BorderColor = System.Drawing.Color.White;
            //    //lblsf_name.Text = test;
            //}
            int iInx = e.Row.RowIndex;
            for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
            {


                if (e.Row.Cells[i].Text != "")
                {
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("$", ",");
                    //HyperLink hLink = new HyperLink();
                    //hLink.Text = e.Row.Cells[i].Text;
                    //string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                    //string sSf_name = dtrowClr.Rows[iInx][2].ToString();




                }
                if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
                e.Row.Cells[0].Wrap = false;
                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
             
            }
         
        }
        #region merge columns
        int RowSpan = 2;
        for (int i = Grdprd.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = Grdprd.Rows[i];
            GridViewRow prevRow = Grdprd.Rows[i + 1];
            if (currRow.Cells[1].Text == prevRow.Cells[1].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                currRow.Cells[1].RowSpan = RowSpan;
                prevRow.Cells[1].Visible = false;
               


                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
       

       e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        #endregion
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillQ();
        //FillSF();      

        FillPrd();
    }

    private void FillQ()
    {
        smode = Request.QueryString["smode"].ToString();

        sType = Request.QueryString["Prod"].ToString();
        sQusType = Request.QueryString["type"].ToString();
        sQuestion = "";
        foreach (System.Web.UI.WebControls.ListItem item in lstQues.Items)
        {
            if (item.Selected)
            {

                sQuestion += item.Value + ",";

            }
        }
        if (sQuestion != "")
        {
            sQuestion = sQuestion.Remove(sQuestion.Length - 1);
            if (smode == "1")
            {
                strQry = "select Question_Id,Question_Text from Trans_Questionnaire_Head where Question_Type_Mode='PB' and Question_Type_SName='" + sQusType + "' and Question_Id in (" + sQuestion + ")  order by Question_Id";
            }
            else if (smode == "2")
            {
                strQry = "select Question_Id,Question_Text from Trans_Questionnaire_Head where Question_Type_Mode='CB' and Question_Type_SName='" + sQusType + "'  and Question_Id in (" + sQuestion + ") order by Question_Id";
            }
            dsDes = db_ER.Exec_DataSet(strQry);
        }
        else
        {
            if (smode == "1")
            {
                strQry = "select Question_Id,Question_Text from Trans_Questionnaire_Head where Question_Type_Mode='PB' and Question_Type_SName='" + sQusType + "'  order by Question_Id";
            }
            else if (smode == "2")
            {
                strQry = "select Question_Id,Question_Text from Trans_Questionnaire_Head where Question_Type_Mode='CB' and Question_Type_SName='" + sQusType + "'   order by Question_Id";
            }
            dsDes = db_ER.Exec_DataSet(strQry);
        }
    }
}
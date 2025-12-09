//
#region Assembly
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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
#endregion
//
//
public partial class MasterFiles_AnalysisReports_Visit_Details_Cat_Cls_Spclty_Dtwise : System.Web.UI.Page
{
    //
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    List<int> iLstMonthStart = new List<int>();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    List<string> sLstCat_Spclty_Cls = new List<string>();
    List<int> iLstCat_Spclty_Cls = new List<int>();
    string sSpeciality;
    string sDays = string.Empty;
    string day = string.Empty;
    string checkVacant = string.Empty;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
       
        //sSpeciality = Request.QueryString["cbVal"].ToString();
        //day = Request.QueryString["cbVal"].ToString();
        day = Request.QueryString["cbValue2"].ToString();
        
        // sDays = Request.QueryString["chk"].ToString();
        //day = Request.QueryString["cdate"].ToString();
        // checkVacant = Request.QueryString["checkVacant"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        string sHeader = "";
       string sMode = Request.QueryString["cMode"].ToString();
        string strToMonth = sf.getMonthName(TMonth);
        if (sMode == "1")
            sHeader = "Category Wise ";
        else if (sMode == "2")
            sHeader = "Speciality Wise ";
        else if (sMode == "3")
            sHeader = "Class Wise ";

        lblHead.Text = sHeader + "Visit Details Between - " + strFrmMonth + " " + FYear  ;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillSalesForce();
    }

    #endregion
    //
    #region FillSalesForce
    private void FillSalesForce()
    {
        int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //dtMnYr.Rows.Add(null, Convert.ToInt32(Request.QueryString["FMonth"].ToString()), Convert.ToInt32(Request.QueryString["Fyear"].ToString()));
        DataTable dtDays = new DataTable();
        dtDays.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtDays.Columns["INX"].AutoIncrementSeed = 1;
        dtDays.Columns["INX"].AutoIncrementStep = 1;
        dtDays.Columns.Add("Dayss", typeof(string));


        string dd = day.Trim();
        dd = day.Remove(day.Length - 1);

        string[] dayss = { dd };

        dayss = dd.Split(',');

        foreach (string d in dayss)
        {
            dtDays.Rows.Add(null, d.ToString().Trim());
        }
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
        DataTable dtSpclty = new DataTable();
        dtSpclty.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSpclty.Columns["INX"].AutoIncrementSeed = 1;
        dtSpclty.Columns["INX"].AutoIncrementStep = 1;
        dtSpclty.Columns.Add("CODE", typeof(int));

        string spclty = Request.QueryString["cbVal"].ToString();
        spclty = spclty.Remove(spclty.LastIndexOf(','));
        string[] ttlSpc = spclty.Split(',');

        foreach (string sSpclty in ttlSpc)
        {
            if (sSpclty != "")
                dtSpclty.Rows.Add(null, Convert.ToInt32(sSpclty));
        }
        string sProcName = "", sTblName = "";
        //sProcName = "visit_details_Cat_Dtwise";
        //sTblName = "@CatTbl";

        if (Request.QueryString["cMode"].ToString() == "1")
        {
          
                sProcName = "visit_details_Cat_Dtwise";
                sTblName = "@CatTbl";
           
        }
        else if (Request.QueryString["cMode"].ToString() == "2")
        {
            
                sProcName = "visit_details_Splty_Dtwise";
                sTblName = "@SpcltyTbl";
           
            
        }
        else if (Request.QueryString["cMode"].ToString() == "3")
        {
           
                sProcName = "visit_details_Class";
                sTblName = "@ClsTbl";
           
        }

        if (sProcName != "")
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            SqlCommand cmd = new SqlCommand(sProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@Fmonth", FMonth);
            cmd.Parameters.AddWithValue("@Fyear", FYear);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue(sTblName, dtSpclty);
            cmd.Parameters.AddWithValue("@dtDays", dtDays);
            cmd.CommandTimeout = 8000;

             SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            //
            GrdDoctor.DataSource = dsts;
            GrdDoctor.DataBind();
        }
    }
    #endregion
    // 
    #region GridView Header
    protected void GVMissedCall_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            //
            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell1 = new TableCell();
            //
            GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "S.No", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#DDEECC", true);

            string spclty = Request.QueryString["cbTxt"].ToString();
            spclty = spclty.Remove(spclty.LastIndexOf(','));
            string[] ttlSpc = spclty.Split(',');
            string sCode = Request.QueryString["cbVal"].ToString();
            sCode = sCode.Remove(sCode.LastIndexOf(','));
            string[] ttlCode = sCode.Split(',');

            //int months = (Convert.ToInt32(Request.QueryString["Tyear"].ToString()) - Convert.ToInt32(Request.QueryString["Fyear"].ToString())) * 12 
            //    + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Fyear"].ToString());
            SalesForce sf = new SalesForce();
            bool flag = true;

           
                string dd = day.Trim();
                dd = day.Remove(day.Length - 1);

                string[] dayss = { dd };

                dayss = dd.Trim().Split(',');

                foreach (string d in dayss)
                {
                   


                    int icolspan = (ttlSpc.Length * 2) + 1;
                    iLstMonthStart.Add(icolspan);
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                    int count = day.Count(x => x == ',');
                    string sTxt = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
             

                    AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, d + "-" + sTxt, "#DDEECC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 2, 0, "Total Lstd Drs", "#DDEECC", true);

                    for (int i = 0; i < ttlSpc.Length; i++)
                    {
                        if (flag)
                        {
                            sLstCat_Spclty_Cls.Add(ttlSpc[i].ToString());
                            iLstCat_Spclty_Cls.Add(Convert.ToInt32(ttlCode[i].ToString()));
                        }
                        AddMergedCells(objgridviewrow1, objtablecell1, 0, 2, ttlSpc[i].ToString(), "#DDEECC", true);
                    }
                    //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total", "#33FF66", true);
                    flag = false;
                    for (int i = 0; i < ttlSpc.Length; i++)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "List", "#DDEECC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met", "#DDEECC", false);
                        // AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Seen", "#DDEECC", false);
                        // AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed", "#DDEECC", false);
                    }
                }
           
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
          
            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell,int rowspan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowspan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //    
    #region grid doctor rowdatabound
    protected void GrdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
         
            int iLstTmp = 0, iMtTmp = 0, iSnTmp = 0, iMsdTmp = 0;
            int iLst = 0, iMt = 0, iSn = 0, iMsd = 0;
            int k = e.Row.Cells.Count - 5, tmp = 0, indx = e.Row.RowIndex;

            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][5].ToString()));
            LinkButton lnk_btn = new LinkButton();
            int iTtl = 1, l = 0;
            for (int j = 5, inxMnth = 0, inxYr = 0, inxCode = 0; j < e.Row.Cells.Count; j += 2)
            {
               
                System.Drawing.Color clr;
                if (tmp % 2 == 0)
                {
                    clr = System.Drawing.Color.LightPink;
                    tmp++;
                }
                else
                {
                    clr = System.Drawing.Color.LightGray;
                    tmp++;
                }
                e.Row.Cells[j].BackColor = clr;
                e.Row.Cells[j + 1].BackColor = clr;
              
                iTtl += 2;
                inxCode++;
                if (inxCode == iLstCat_Spclty_Cls.Count)
                {
                    inxCode = 0;
                }
                if (iLstMonthStart.Count >= l)
                {
                    if (iLstMonthStart[l] == iTtl)
                    {
                        iTtl = 1;
                        l++;
                        if (iLstMonthStart.Count != l)
                        {
                            inxMnth++;
                            inxYr++;
                            j++;
                        }
                    }
                }
            }
           
           
            for (int i = 4; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "-";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            //
        }
    }
    //
    private string AssignToolTip(string sMnth, string sYr, string sCode, string sType)
    {
        SalesForce sf = new SalesForce();
        string sTxt = sf.getMonthName(sMnth.ToString()).Substring(0, 3) + "/" + sYr.Substring(2, 2);
        return sTxt += " (" + sCode + " - " + sType + ")";
    }
    //
    #endregion
    //
}
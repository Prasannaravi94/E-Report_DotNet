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

public partial class MIS_Reports_rptMangerVisit_VacantMR : System.Web.UI.Page
{
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
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<string> iLstdes = new List<string>();
    List<DataTable> result = new List<System.Data.DataTable>();
    DataSet dsSalesforce = new DataSet();
    DataSet dsSalesforce2 = new DataSet();
    DataSet dsts = new DataSet();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());

        lblHead.Text = lblHead.Text + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " +"<span style='color:#D624AD;'>" + strFieledForceName +"</span>";
        lblvacant.Text = "<span style='color:red'>" + "*** " + "</span>" + "Whether the Managers are covered the " + "<span style='color:#FF5733;font-weight:true'>" + "Vacant HQ's " + "</span>" + " or not ...";
        //FillReport();
        FillVacantMr();

    }

    private void FillVacantMr()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.getDrcnt_VcantMr(sf_code, div_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            GrdFixation.DataSource = dsSalesforce;
            dtrowClr = dsSalesforce.Tables[0].Copy();
            GrdFixation.DataBind();
            
        }

       // dsDoctor = sf.getDrcnt_VcantMr(FMonth, FYear, div_code, sf_code);
    }

    

    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;

        string mnt = Convert.ToString(months);
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //

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
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = ""; 

        sProc_Name = "MangerVisit_VacantMR";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dsts);
        dtrowClr = dsts.Tables[dtMnYr.Rows.Count].Copy();
        //  dtrowdt = dsts.Tables[1].Copy();

        // result = dsts.Tables[dtMnYr.Rows.Count-1].AsEnumerable()
        //.GroupBy(row => row.Field<string>("month"))
        //.Select(g => g.CopyToDataTable()).ToList();

        dsts.Tables[dtMnYr.Rows.Count].Columns.RemoveAt(6);
        dsts.Tables[dtMnYr.Rows.Count].Columns.RemoveAt(9);
        dsts.Tables[dtMnYr.Rows.Count].Columns.RemoveAt(1);
        GrdFixation.DataSource = dsts.Tables[dtMnYr.Rows.Count];
        GrdFixation.DataBind();
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
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell = new TableCell();

            #endregion
            //
            #region Merge cells


            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Sf_code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Field Force Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
  

            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;

            string strQry = "";


            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level from Mas_SF_Designation " +
                     " where Division_Code='" + div_code + "' and type='2' order by Designation_Short_Name ";

            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (months1 >= 0)
            {
                int iR = 0;
                for (int j = 1; j <= months1 + 1; j++)
                {
                    int iColSpan = 0;
                    //  AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                    TableCell objtablecell2 = new TableCell();
                    foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                    {
                        int iCnt = 0;
                        iColSpan = ((dsDoctor.Tables[0].Rows.Count));
                        AddMergedCells(objgridviewrow2, objtablecell2, iCnt, dtRow["Designation_Short_Name"].ToString(), "#0097AC", false);
                        if (iR == 0)
                            iLstdes.Add(dtRow["Designation_Short_Name"].ToString());
                    }
                    iR++;
                    iLstVstmnt.Add(cmonth1);
                    iLstVstyr.Add(cyear1);
                    AddMergedCells(objgridviewrow, objtablecell, iColSpan, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1 + " (No.of Doctors Visited)", "#0097AC", true);
                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }

            }

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);

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

       
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("border-color", "black");
        objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("width", "300px");
        objtablecell.Wrap = false;
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;


            int indx = e.Row.RowIndex;

           
                string sfcode = dtrowClr.Rows[indx][1].ToString();
                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                SalesForce sf1 = new SalesForce();
                DataSet dsmgrsf = new DataSet();

              

                if (months1 >= 0)
                {
                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        SalesForce sal = new SalesForce();
                        dsSalesforce2 = sal.getDrcnt_VcantMr_Desig(Convert.ToInt16(cmonth1), Convert.ToInt16(cyear1), div_code, sfcode);

                        //if (dsSalesforce2.Tables[0].Rows.Count > 0)
                        //{
                        //    DataTable dt = sf1.getMRJointWork_camp(div_code, sfcode, 0);
                        //    dsmgrsf.Tables.Add(dt);
                        //    dsDoctor = dsmgrsf;

                           
                            //if (dsDoctor.Tables[0].Rows.Count > 0)
                            //{
                            //  //var dataVal = dt.AsEnumerable().Where(co => co.Field<string>("Sf_Code").Substring(0,3)=="MGR").Select(row => new { Desg = row.Field<string>("Designation_Short_Name"), }).Distinct().OrderBy(x => x.Desg);

                            //    foreach (DataRow drFF in dsDoctor.Tables[0].Rows)
                            //    {
                            //        string desi = dsSalesforce2.Tables[0].Columns.ToString();

                            //        if (drFF["sf_Designation_Short_Name"].ToString() == desi)
                            //        {

                            //        }
                            //    }
                            //}

                            for (int k = 1; k < dsSalesforce2.Tables[0].Columns.Count; k++)
                            {
                                TableCell tbl = new TableCell();
                                tbl.Text = dsSalesforce2.Tables[0].Rows[0][k].ToString();
                                if (tbl.Text == "0")
                                {
                                    tbl.Text = "";
                                }
                                e.Row.Cells.Add(tbl);
                            }
                            cmonth1 = cmonth1 + 1;
                            if (cmonth1 == 13)
                            {
                                cmonth1 = 1;
                                cyear1 = cyear1 + 1;
                            }

                           
                        }
                       
                    }
                }

        e.Row.Cells[2].Wrap = false;
        e.Row.Cells[3].Wrap = false;
        e.Row.Cells[4].Wrap = false;               
        }


    }

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
using System.Drawing;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_Survey_Rpt_Survey_Process_View : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsMailCompose = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string Survey_Id = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;

    List<int> iLstVstCnt = new List<int>();
    DataSet dschem = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    int imissed_dr = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            Survey_Id = Request.QueryString["Survey_Id"].ToString();
            strFieledForceName = Request.QueryString["Name"].ToString();

            SalesForce sf = new SalesForce();
            lblHead.Text = " Survey - View ";
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillReport();
        }
    }

    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Survey_Process_view";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        cmd.Parameters.AddWithValue("@Survey_Id", Survey_Id);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("Sf_Code");
        dsts.Tables[0].Columns.Remove("clr");
        //dsts.Tables[0].Columns.Remove("FirstLevel");
        //dsts.Tables[0].Columns.Remove("SecondLevel");
        dsts.Tables[0].Columns.Remove("Sf_Code1");
        dsts.Tables[0].Columns.Remove("Empty");

        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }

    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            DB_EReporting db = new DB_EReporting();
            string strQry = "";

            strQry = " select 'Q'+cast((ROW_NUMBER() OVER (ORDER BY a.Question_Id))as varchar(20)) Ques_Id,a.Question_Id,b.Question_Name from Mas_Question_Survey_Creation_Detail a,Mas_Question_Creation b   " +
                " where a.Question_Id=b.Question_Id and a.Survey_ID='" + Request.QueryString["Survey_Id"].ToString() + "' and a.division_code='" + Session["div_code"].ToString() + "' and a.division_code=b.division_code order by Ques_Id ";
            dsDoctor = db.Exec_DataSet(strQry);

            #region Merge cells
            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp.Code", "#0097AC", true);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
            {
                AddMergedCells(objgridviewrow, objtablecell, 5, dtRow["Question_Name"].ToString(), "#0097AC", true);

                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Drs", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chm", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Stk", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Hos", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Prd", "#0097AC", false);
            }
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
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
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Style.Add("border", "1px solid Red");
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            if (e.Row.Cells[1].Text == "ZZZZZ") //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
            {
                if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    
                    int Ques = 0;
                    int Survey_Md = 0;
                    string Survey_Mode = string.Empty;

                    for (int n = 6, m = 0; n <= e.Row.Cells.Count - 1; n++)
                    {
                        if (Survey_Md == 0)
                        {
                            Survey_Mode = "D";
                        }
                        else if (Survey_Md == 1)
                        {
                            Survey_Mode = "C";
                        }
                        else if (Survey_Md == 2)
                        {
                            Survey_Mode = "S";
                        }
                        else if (Survey_Md == 3)
                        {
                            Survey_Mode = "H";
                        }
                        else if (Survey_Md == 4)
                        {
                            Survey_Mode = "P";
                        }
                        string Ques_Id = dsDoctor.Tables[0].Rows[Ques].ItemArray.GetValue(1).ToString();
                        if (e.Row.Cells[n].Text != "&nbsp;" && e.Row.Cells[n].Text != "0" && e.Row.Cells[n].Text != "-")
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[n].Text;
                            //hLink.Attributes.Add("class", "btnLstDr");
                            hLink.Attributes.Add("onClick", "callServerButtonEvent('" + Survey_Mode + "','" + Ques_Id + "')");
                            hLink.ToolTip = "Click here";
                            hLink.Attributes.Add("style", "cursor:pointer");
                            hLink.Font.Underline = true;
                            hLink.ForeColor = System.Drawing.Color.Red;
                            e.Row.Cells[n].Controls.Add(hLink);
                            e.Row.Cells[n].Attributes.Add("align", "center");

                            //j = j + 1;
                        }

                        if (Survey_Md == 4)
                        {
                            Survey_Md = 0;
                            Ques = Ques + 1;
                        }
                        else
                        {
                            Survey_Md = Survey_Md + 1;
                        }
                        e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[n].Height = 30;
                        e.Row.Cells[n].Attributes.Add("style", "font-weight:bold;");
                        e.Row.Cells[n].Style.Add("font-size", "12pt");

                        e.Row.Cells[n].Style.Add("color", "Red");
                        e.Row.Cells[n].Style.Add("border-color", "black");
                        //e.Row.Cells[n].Style.Add("font-size", "12pt");

                        if (e.Row.Cells[n].Text == "0" || e.Row.Cells[n].Text == "" || e.Row.Cells[n].Text == "&nbsp;")
                        {
                            e.Row.Cells[n].Text = "-";
                        }
                    }
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].ColumnSpan = 6;
                    e.Row.Cells[5].Text = "Grand Total";
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[5].Height = 30;
                    e.Row.Cells[5].Attributes.Add("style", "font-weight:bold;");
                    e.Row.Cells[5].Style.Add("font-size", "12pt");
                    e.Row.Cells[5].Style.Add("color", "Red");
                    e.Row.Cells[5].Style.Add("border-color", "black");

                }
            }

            else
            {
                int Ques = 0;
                int Survey_Md = 0;
                string Survey_Mode = string.Empty;
                for (int i = 6, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (Survey_Md == 0)
                    {
                        Survey_Mode = "D";
                    }
                    else if (Survey_Md == 1)
                    {
                        Survey_Mode = "C";
                    }
                    else if (Survey_Md == 2)
                    {
                        Survey_Mode = "S";
                    }
                    else if (Survey_Md == 3)
                    {
                        Survey_Mode = "H";
                    }
                    else if (Survey_Md == 4)
                    {
                        Survey_Mode = "P";
                    }

                    if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "&nbsp;")
                    {
                        string Ques_Id = dsDoctor.Tables[0].Rows[Ques].ItemArray.GetValue(1).ToString();
                        string Ques_Name = dsDoctor.Tables[0].Rows[Ques].ItemArray.GetValue(2).ToString();

                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        hLink.Attributes.Add("class", "btnDrSn");
                        hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "',  '" + Survey_Mode + "','" + Request.QueryString["Survey_Id"].ToString() + "','" + Ques_Id + "','" + div_code + "','" + Ques_Name + "','" + Convert.ToString(dtrowClr.Rows[indx][3].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][4].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][6].ToString()) + "' )");
                        hLink.ToolTip = "Click here";
                        //hLink.NavigateUrl = "#";
                        hLink.Font.Underline = true;
                        hLink.Attributes.Add("style", "cursor:pointer");
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                    }

                    if (Survey_Md == 4)
                    {
                        Survey_Md = 0;
                        Ques = Ques + 1;
                    }
                    else
                    {
                        Survey_Md = Survey_Md + 1;
                    }

                    if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "-" || e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "-";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");
                }
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
            #endregion
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
        }
    }


    protected void btnExcelGrid_Click(object sender, EventArgs e)
    {
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Survey_Process_view_Excel", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(Session["div_code"].ToString()));
        cmd.Parameters.AddWithValue("@sf_code", Request.QueryString["sf_code"].ToString());
        cmd.Parameters.AddWithValue("@Survey_Id", Request.QueryString["Survey_Id"].ToString());
        cmd.Parameters.AddWithValue("@DCHP_Mode", lblSurvey_Mode.Text);
        cmd.Parameters.AddWithValue("@Ques_id", lblQues_Id.Text);

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        //ds.Tables[0].Columns.Remove("sf_code");
        DataTable dt = ds.Tables[0];
        int countCol = dt.Columns.Count;
  
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        var ws = wbook.Worksheets.Add(dt, " Survey Answer ");
        ws.Row(1).InsertRowsAbove(1);
        ws.Cell(1, 1).Value = " Survey Answer ";
        ws.Cell(1, 1).Style.Font.Bold = true;
        ws.Cell(1, 1).Style.Font.FontSize = 15;
        //  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        ws.Range("A1:M1").Row(1).Merge();

        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\" Survey Answer View.xlsx\"");
        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }
        httpResponse.End();
    }

}



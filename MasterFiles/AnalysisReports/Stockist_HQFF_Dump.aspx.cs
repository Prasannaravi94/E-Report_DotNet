using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;

using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using System.IO;
using System.Data.Sql;
using DBase_EReport;
using ClosedXML;
public partial class MasterFiles_AnalysisReports_Stockist_HQFF_Dump : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (sf_type == "3" || sf_type == "")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            Filldiv();
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            ddlDivision.Visible = false;
            lblDivision.Visible = false;

        }
        else if (Session["sf_type"].ToString() == "")
        {
            UserControl_pnlMenu c1 =
           (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = Page.Title;
        }
        else if (Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
                (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = Page.Title;
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = Page.Title;
            ddlDivision.Visible = false;
            lblDivision.Visible = false;

        }



    }


    private void Filldiv()
    {

        Division dv = new Division();
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
        {
            string[] strDivSplit = div_code.Split(',');
            ddlDivision.Items.Add("ALL");
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    DataSet dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }

            ddlDivision.SelectedIndex = 0;
        }
        else
        {
            DataSet dsDivision = dv.getDivisionHO(div_code);
            //DataSet dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();

                ddlDivision.SelectedIndex = 0;
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        //  SqlCommand cmd = new SqlCommand("Stockist_HQwise_Dump", con);

        SqlCommand cmd = new SqlCommand("Stockist_HQwise_Dump_1", con); //added done 12-5-2025
        
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", ddlDivision.SelectedValue.ToString());

        cmd.CommandTimeout = 1000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        ds.Tables[0].Columns.Remove("Stockist_Code");
        ds.Tables[0].Columns.Remove("Sf_Code");

        dt = ds.Tables[0];
        con.Close();


        //ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        ////  wbook.Worksheets.Add(dt, "Chemist");

        //var ws = wbook.Worksheets.Add(dt, "Stockist");
        //ws.Row(1).InsertRowsAbove(1);
        //ws.Cell(1, 1).Value = "Stockist Dump (  " + ddlDivision.SelectedItem.Text.Trim() + " )";
        //ws.Cell(1, 1).Style.Font.Bold = true;
        //ws.Cell(1, 1).Style.Font.FontSize = 15;
        ////  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        //ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        //ws.Range("A1:K1").Row(1).Merge();
        //// Prepare the response
        //HttpResponse httpResponse = Response;
        //httpResponse.Clear();
        //httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        ////Provide you file name here
        //httpResponse.AddHeader("content-disposition", "attachment;filename=\"Stockist_Dump.xlsx\"");

        //// Flush the workbook to the Response.OutputStream
        //using (MemoryStream memoryStream = new MemoryStream())
        //{
        //    wbook.SaveAs(memoryStream);
        //    memoryStream.WriteTo(httpResponse.OutputStream);
        //    memoryStream.Close();
        //}

        //httpResponse.End();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Stockist_Dump.xls"));
        Response.ContentType = "application/ms-excel";
        //DataTable dt = BindDatatable();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();

    }


}
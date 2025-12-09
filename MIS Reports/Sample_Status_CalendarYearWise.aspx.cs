using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MIS_Reports_Sample_Status_CalendarYearWise : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dschm = null;
    DataSet dsstk = null;
    DataSet dsDoc5 = null;
    DataSet dsDoc6 = null;
    string tot_chm6 = string.Empty;
    DataSet dsstk1 = null;
    DataSet dsstk2 = null;
    string tot_chm = string.Empty;
    string tot_stk = string.Empty;
    string tot_stk1 = string.Empty;
    string tot_stk2 = string.Empty;
    string tot_chm1 = string.Empty;
    string total_doc_reg = string.Empty;
    string tot_dr_draft = string.Empty;
    DataTable dt = new DataTable();
    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsDoc2 = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string tot_draft = string.Empty;
    string tot_dr = string.Empty;
    string total_doc = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_dr1 = string.Empty;
    string total_doc1 = string.Empty;
    string tot_dr2 = string.Empty;
    string total_doc2 = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sfcsts = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataTable dtrowClr = new DataTable();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    string strqry = string.Empty;


    DB_EReporting db_ER = new DB_EReporting();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            ////// menu1.FindControl("btnBack").Visible = false;
            //FillManagers();
            //FillYear();

            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                FillYear();

            }

            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //ddlFFType.Visible = false;


                //FillManagers();
                //FillMRManagers1();
                // this.FillMasterList();
                FillYear();

            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl_TP c1 =
               (UserControl_MenuUserControl_TP)LoadControl("~/UserControl/MenuUserControl_TP.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //FillManagers();
                //ddlFieldForce.SelectedIndex = 1;

                FillYear();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl_TP c1 =
               (UserControl_MenuUserControl_TP)LoadControl("~/UserControl/MenuUserControl_TP.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }

        }
    }
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            DataSet dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division

            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    int Year = k + 1;
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

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string[] authorsList = ddlFinancial.SelectedValue.Split('-');

        string From_year = authorsList[0].ToString();
        string To_year = authorsList[1].ToString();

        if(Fieldforcewise.Checked==true)
        {
            // strqry = "Exec Sample_CalnedarYearwise_FFwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";
            if (sf_code.Contains("MGR") || sf_code.Contains("MR"))
            {
                strqry = "Exec Sample_CalnedarYearwise_FFwise_OB_Add_Hierarchy '" + div_code + "','" + sf_code + "','" + From_year + "','" + To_year + "'";

            }
            else
            {
                strqry = "Exec Sample_CalnedarYearwise_FFwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";

            }
            dsDoc2 = db_ER.Exec_DataSet(strqry);
            if (dsDoc2.Tables[0].Rows.Count > 0)
            {
                Griddespatchdetail.Visible = true;
                grdDespatch.Visible = false;
                Griddespatchdetail.DataSource = dsDoc2;
                Griddespatchdetail.DataBind();
            }
            else
            {
                Griddespatchdetail.DataSource = null;
                Griddespatchdetail.DataBind();
            }
        }
        else
        {
            if (sf_code.Contains("MGR") || sf_code.Contains("MR"))
            {
                strqry = "Exec Sample_CalnedarYearwise_OB_Add_Hierar '" + div_code + "','" + sf_code + "','" + From_year + "','" + To_year + "'";

            }
            else
            {
                strqry = "Exec Sample_CalnedarYearwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";

            }
            Griddespatchdetail.Visible = false;
            grdDespatch.Visible = true;
            //strqry = "Exec Sample_CalnedarYearwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";
            dsDoc2 = db_ER.Exec_DataSet(strqry);
            if (dsDoc2.Tables[0].Rows.Count > 0)
            {
                grdDespatch.DataSource = dsDoc2;
                grdDespatch.DataBind();
            }
            else
            {
                grdDespatch.DataSource = null;
                grdDespatch.DataBind();
            }
        }

    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        string[] authorsList = ddlFinancial.SelectedValue.Split('-');

        string From_year = authorsList[0].ToString();
        string To_year = authorsList[1].ToString();
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        if (Fieldforcewise.Checked == true)
        {
            if (sf_code.Contains("MGR") || sf_code.Contains("MR"))
            {
                strqry = "Exec Sample_CalnedarYearwise_FFwise_OB_Add_Hierarchy '" + div_code + "','" + sf_code + "','" + From_year + "','" + To_year + "'";

            }
            else{
                strqry = "Exec Sample_CalnedarYearwise_FFwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";

            }
           // strqry = "Exec Sample_CalnedarYearwise_FFwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";
            ds = db_ER.Exec_DataSet(strqry);
        }
        else
        {
            if (sf_code.Contains("MGR") || sf_code.Contains("MR"))
            {
                strqry = "Exec Sample_CalnedarYearwise_OB_Add_Hierar '" + div_code + "','" + sf_code + "','" + From_year + "','" + To_year + "'";

            }
            else
            {
                strqry = "Exec Sample_CalnedarYearwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";

            }
           // strqry = "Exec Sample_CalnedarYearwise_OB '" + div_code + "','" + From_year + "','" + To_year + "'";
            ds = db_ER.Exec_DataSet(strqry);
        }

        // ds.Tables[0].Columns.Remove("Rate");
        //ds.Tables[0].Columns.Remove("sf_code");
        //ds.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
        //ds.Tables[0].Columns.Remove("main_sf_code");
        //ds.Tables[0].Columns.Remove("clr");
        //ds.Tables[0].Columns.Remove("trans_slno");
        //ds.Tables[0].Columns.Remove("DAY");

        
        dt = ds.Tables[0];
        /*
        // ExportDataSetToExcel(ds);      
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"sample_prd\"");

        // create a string writer
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.Font.Name = "Verdana";
                dg.Font.Size = 10;
                dg.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                dg.HeaderStyle.BackColor = System.Drawing.Color.Chocolate;
                dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

                dg.RenderControl(htw);

                response.Write(sw.ToString());
                response.End();
            }
        } */

        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        //  wbook.Worksheets.Add(dt, "Chemist");

        var ws = wbook.Worksheets.Add(dt, "Sample");
        ws.Row(1).InsertRowsAbove(1);
        ws.Cell(1, 1).Value = "Sample Dump";
        ws.Cell(1, 1).Style.Font.Bold = true;
        ws.Cell(1, 1).Style.Font.FontSize = 15;
        //  ws.Cell(1, 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightPink;
        ws.Row(1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
        ws.Range("A1:K1").Row(1).Merge();
        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"Sample_Dump.xlsx\"");

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
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
using iTextSharp.text;
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

public partial class MasterFiles_AnalysisReports_CoreDrsMap_Visit_Dump : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

                //FillColor();
                // FillMRManagers1();
                FillMRManagers1();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

                FillColor();
                FillMRManagers();
                BindDate();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
        }
        FillColor();

    }
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);

        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
        {

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                //ddlSF.DataTextField = "Desig_Color";
                //ddlSF.DataValueField = "sf_code";
                //ddlSF.DataSource = dsSalesForce;
                //ddlSF.DataBind();

                //ddlSF.DataTextField = "des_color";
                //ddlSF.DataValueField = "sf_code";
                //ddlSF.DataSource = dsSalesForce;
                //ddlSF.DataBind();
            }
        }
        else
        {
            // Fetch Managers Audit Team
            DataSet dsmgrsf = new DataSet();
            DataSet dsTP = new DataSet();
            DataTable dt = sf.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsTP = dsmgrsf;

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTP;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Des_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsTP;
            //ddlSF.DataBind();
        }
        FillColor();


    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

        FillColor();
    }
    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }

    //private void FillMRManagers1()
    //{
    //    SalesForce sf = new SalesForce();

    //    dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();
    //    }
    //    FillColor();
    //}


    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlFrmYear.Items.Add(k.ToString());
            }
            //ddlFrmYear.Text = DateTime.Now.Year.ToString();
            //ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet dsdes = new DataSet();
        strQry = "select Designation_Code,Designation_Short_Name from mas_sf_designation where division_code='" + div_code + "' and Designation_Active_Flag=0 and type=2 " +
           " order by Manager_SNo ";
        dsDes = db_ER.Exec_DataSet(strQry);
        string des_code = string.Empty;
        DataTable dtSpec = new DataTable();
        dtSpec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSpec.Columns["INX"].AutoIncrementStep = 1;
        dtSpec.Columns["INX"].AutoIncrementSeed = 1;
        dtSpec.Columns.Add("SPECCIA", typeof(int));

        if (dsDes.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDes.Tables[0].Rows.Count; i++)
            {
                dtSpec.Rows.Add(null, dsDes.Tables[0].Rows[i]["Designation_Code"].ToString());
            }
        }

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
       // SqlCommand cmd = new SqlCommand("CoreDrMap_Visit_Dump", con);

        SqlCommand cmd = new SqlCommand("CoreDrMap_Visit_Dump_demo", con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month));
        cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year));
        cmd.Parameters.AddWithValue("@dtdes", dtSpec);

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        ds.Tables[0].Columns.Remove("Mgr_Code");
        ds.Tables[0].Columns.Remove("listeddrcode");
        ds.Tables[0].Columns.Remove("sf_code");
        ds.Tables[0].Columns.Remove("sf_code1");
        ds.Tables[0].Columns.Remove("listeddrcode1");
        ds.Tables[0].Columns.Remove("Doc_code");
        ds.Tables[0].Columns.Remove("Trans_Detail_Info_Code");
        ds.Tables[0].Columns.Remove("Territory_code");
        dt = ds.Tables[0];
        con.Close();
        int countRow = dt.Rows.Count;
        int countCol = dt.Columns.Count;

        for (int iCol = 0; iCol < countCol; iCol++)
        {
            DataColumn col = dt.Columns[iCol];
            if (col.ColumnName != "sno" && col.ColumnName != "sf_name" && col.ColumnName != "SVL_No" && col.ColumnName != "Unique_Code" && col.ColumnName != "ListedDr_Name" 
                && col.ColumnName != "Cat" && col.ColumnName != "Spec" && col.ColumnName != "Camp" && col.ColumnName != "Rcpa Potential"
                && col.ColumnName != "Rcpa Yield" && col.ColumnName != "Joint Work" && col.ColumnName != "Territory_code" 
                && col.ColumnName != "Emp Code" && col.ColumnName != "Doctor mobile No" && col.ColumnName != "Input Given Date"
                && col.ColumnName != "Input Name" && col.ColumnName != "Input Qty" && col.ColumnName != "MGR Name"
                && col.ColumnName != "MGR Designation" && col.ColumnName != "MGR HQ" && col.ColumnName != "MGR Emp Code" 
                && col.ColumnName != "sf_HQ" && col.ColumnName != "sf_Designation" && col.ColumnName != "Sample Name" && col.ColumnName != "Sample Given Date")
            {
                if (col.ColumnName == "0_0_AAT")
                {
                    dt.Columns[iCol].ColumnName = "MR Visit Count";
                    dt.AcceptChanges();
                }
                else if (col.ColumnName == "0_0_ABT")
                {
                    dt.Columns[iCol].ColumnName = "MR Visit Date";
                    dt.AcceptChanges();
                }
                else if (col.ColumnName.Contains("HQ"))
                {
                    string[] dess_HQ = col.ColumnName.Split('_');
                    string des_code_HQ = dess_HQ[2].Trim().ToString();

                    dt.Columns[iCol].ColumnName = des_code_HQ;
                    dt.AcceptChanges();
                }
                else
                {
                    // //  des_code = col.ColumnName.Split('_').Last();
                    string[] dess= col.ColumnName.Split('_');
                    des_code = dess[1].Trim().ToString();
                    Designation des = new Designation();
                    dsdes = des.getDesig_graph(des_code, div_code);
                    Boolean columnExists = dt.Columns.Contains(dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString() + " Visit Count");
                    if (columnExists == true)
                    {
                        dt.Columns[iCol].ColumnName = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString() + " Visit Date";
                    }
                    else
                    {
                        dt.Columns[iCol].ColumnName = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString() + " Visit Count";
                    }
                    dt.AcceptChanges();
                }
            }
        }


        //for (int i = 5; i < dt.Columns.Count; i++)
        //{
        //    foreach (DataColumn c in dt.Columns)
        //    {

        //        if (c.ColumnName != "sno" && c.ColumnName != "sf_name" && c.ColumnName != "ListedDr_Name" && c.ColumnName != "Cat" && c.ColumnName != "Spec")
        //        {
        //            if (c.ColumnName == "0_0_AAT" || c.ColumnName == "0_0_ABT")
        //            {
        //                dt.Columns[i].ColumnName = "MR";
        //                dt.AcceptChanges();
        //            }
        //            else
        //            {
        //                des_code = c.ColumnName.Split('_').First();
        //                Designation des = new Designation();
        //                dsdes = des.getDesig_graph(des_code, div_code);
        //                dt.Columns[i].ColumnName = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString();
        //                dt.AcceptChanges();
        //            }
        //            break;
        //        }

        //    }

        //}
        //

        // ExportDataSetToExcel(ds);      
        /**
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"Dump\"");

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
                dg.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

                dg.RenderControl(htw);

                response.Write(sw.ToString());
                response.End();
            }

        }**/
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        wbook.Worksheets.Add(dt, "tab1");
        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"CoreDr_Map_Visit_Dump.xlsx\"");

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



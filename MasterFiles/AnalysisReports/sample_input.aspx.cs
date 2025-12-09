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
using ClosedXML;
using iTextSharp.text.pdf.qrcode;
using System.ComponentModel.Design.Data;

public partial class MasterFiles_AnalysisReports_sample_input : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    string strQry = string.Empty;
    string mode = string.Empty;
    string item = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
    //sting chkDetail = string.Empty;
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
                ddlmode.SelectedValue = "1";
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

    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
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
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFrmYear.Items.Add(k.ToString());

            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();

            ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //DataSet dsdes = new DataSet();
        //strQry = "select Designation_Code,Designation_Short_Name from mas_sf_designation where division_code='" + div_code + "' and Designation_Active_Flag=0 and type=2 " +
        //   " order by Manager_SNo ";
        //dsDes = db_ER.Exec_DataSet(strQry);
        //string des_code = string.Empty;
        //DataTable dtSpec = new DataTable();
        //dtSpec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtSpec.Columns["INX"].AutoIncrementStep = 1;
        //dtSpec.Columns["INX"].AutoIncrementSeed = 1;
        //dtSpec.Columns.Add("SPECCIA", typeof(int));

        //if (dsDes.Tables[0].Rows.Count > 0)
        //{
        //    for (int i = 0; i < dsDes.Tables[0].Rows.Count; i++)
        //    {
        //        dtSpec.Rows.Add(null, dsDes.Tables[0].Rows[i]["Designation_Code"].ToString());
        //    }
        //}

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        SqlCommand cmd = new SqlCommand();
        con.Open();
        if (ddlmode.SelectedValue == "1")
        {
            cmd = new SqlCommand("Rep_access_with_HQ_All_sample", con);
        }
        else
        {
            cmd = new SqlCommand("Rep_access_with_HQ_All_Input", con);
        }

        //SqlCommand cmd = new SqlCommand("Listeddr_Period_MGR_II_Level_new", con);



        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue.Trim());
        cmd.Parameters.AddWithValue("@month", Convert.ToInt32(ddlFrmMonth.SelectedValue.Trim()));
        cmd.Parameters.AddWithValue("@year", Convert.ToInt32(ddlFrmYear.SelectedValue.Trim()));
        //cmd.Parameters.AddWithValue("@dtdes", dtSpec);
        //cmd.Parameters.AddWithValue("@mode", ddlmode.SelectedValue);
        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        //ds.Tables[0].Columns.Remove("listeddrcode");
        ds.Tables[0].Columns.Remove("sf_code");
        //ds.Tables[0].Columns.Remove("listeddrcode1");

        dt = ds.Tables[0];
        con.Close();
        int countRow = dt.Rows.Count;
        int countCol = dt.Columns.Count;


        //if(ddlmode.SelectedValue ==1)
        // {
        //    Stored = "Rep_access_with_HQ_All_sample";
        // }
        // else
        // {
        //     storedprocedure = "Rep_access_with_HQ_All_Input";
        // }

        //var check = "0";
        //if (chkDetail.Checked)
        //{
        //    check = "0";
        //}
        //else
        //{
        //    check = "1";
        //}


        //for (int iCol = 0; iCol < countCol; iCol++)
        //{
        //    DataColumn col = dt.Columns[iCol];
        //    if (col.ColumnName != "sno" && col.ColumnName != "sf_name" && col.ColumnName != "Uni code" && col.ColumnName != "ListedDr_Name" && col.ColumnName != "Cat" && col.ColumnName != "Spec")
        //    {
        //        if (col.ColumnName == "0_0_AAT")
        //        {
        //            dt.Columns[iCol].ColumnName = "MR Visit Count";
        //            dt.AcceptChanges();
        //        }
        //        else if (col.ColumnName == "0_0_ABT")
        //        {
        //            dt.Columns[iCol].ColumnName = "MR Visit Date";
        //            dt.AcceptChanges();
        //        }
        //        else
        //        {
        //            // des_code = col.ColumnName.Split('_').First();
        //            string[] dess = col.ColumnName.Split('_');
        //            des_code = dess[1].Trim().ToString();
        //            Designation des = new Designation();
        //            dsdes = des.getDesignationEd(des_code, div_code);
        //            Boolean columnExists = dt.Columns.Contains(dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString() + " Visit Count");
        //            if (columnExists == true)
        //            {
        //                dt.Columns[iCol].ColumnName = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString() + " Visit Date";
        //            }
        //            else
        //            {
        //                dt.Columns[iCol].ColumnName = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString() + " Visit Count";
        //            }
        //            dt.AcceptChanges();
        //        }
        //    }
        //}


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

        //    ExportDataSetToExcel(ds);
        //    *
        //    HttpResponse response = HttpContext.Current.Response;

        //    first let's clean up the response.object
        //    response.Clear();
        //    response.Charset = "";

        //    set the response mime type for excel

        //   response.ContentType = "application/vnd.ms-excel";
        //   response.AddHeader("Content-Disposition", "attachment;filename=\"Dump\"");

        //    create a string writer
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //        {
        //            instantiate a datagrid
        //           DataGrid dg = new DataGrid();
        //            dg.DataSource = ds.Tables[0];
        //            dg.DataBind();
        //            dg.HeaderStyle.Font.Bold = true;
        //            dg.Font.Name = "Verdana";
        //            dg.Font.Size = 10;
        //            dg.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //            dg.HeaderStyle.BackColor = System.Drawing.Color.Blue;
        //            dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

        //            dg.RenderControl(htw);

        //            response.Write(sw.ToString());
        //            response.End();
        //        }

        //    }
        //    *
    


        //ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        //wbook.Worksheets.Add(dt, "tab1");
        //HttpResponse httpResponse = Response;
        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "sample_input.xls"));
        //Response.ContentType = "application/ms-excel";
        //DataTable dt = BindDatatable();
        //string str = string.Empty;
        //foreach (DataColumn dtcol in dt.Columns)
        //{
        //    Response.Write(str + dtcol.ColumnName);
        //    str = "\t";
        //}
        //Response.Write("\n");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    str = "";
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        Response.Write(str + Convert.ToString(dr[j]));
        //        str = "\t";
        //    }
        //    Response.Write("\n");
        //}
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        wbook.Worksheets.Add(dt, "tab1");
        HttpResponse httpResponse = Response;
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "sample_input.xls"));
        Response.ContentType = "application/ms-excel";
        //DataTable dt = BindDatatable();
        //string str = string.Empty;
        //foreach (DataColumn dtcol in dt.Columns)
        //{
        //    Response.Write(str + dtcol.ColumnName);
        //    str = "\t";
        //}
        //Response.Write("\n");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    str = "";
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        Response.Write(str + Convert.ToString(dr[j]));
        //        str = "\t";
        //    }
        //    Response.Write("\n");
        //}

        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }
        httpResponse.End();

    }


}
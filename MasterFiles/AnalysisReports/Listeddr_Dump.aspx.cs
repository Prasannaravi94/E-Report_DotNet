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
public partial class MasterFiles_AnalysisReports_Listeddr_Dump : System.Web.UI.Page
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
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers2();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers2();
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
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
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
    private void FillMRManagers2()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

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
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
   
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        if (ddlmode.SelectedValue == "1")
        {
            //cmd = new SqlCommand("Listeddr_Dump_1_New", con);//comment 30-08-2025 Active Drs
            cmd = new SqlCommand("ListeddrDump_fast_dev", con);//added done by 30-08-2025
        }
        else if (ddlmode.SelectedValue == "2")
        {
             
            // Including deactive Drs (Active & Deactive drs)
            // cmd = new SqlCommand("Listeddr_Dump_Deactive_1", con);//comment 30-08-2025 
            cmd = new SqlCommand("ListeddrDump_fast_dev_deact", con);//added done by 30-08-2025
                                                               
        }
        else
        {
            // Details
            //   cmd = new SqlCommand("Listeddr_Dump_Detail_1", con);//comment 30-08-2025
            cmd = new SqlCommand("ListeddrDump_fast_dev", con);//added done by 30-08-2025
        }

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);


        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
       // ds.Tables[0].Columns.Remove("ListedDrCode");
        ds.Tables[0].Columns.Remove("Sf_Code");
        ds.Tables[0].Columns.Remove("Division Code");


        dt = ds.Tables[0];
        con.Close();



        ///excel
        //ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        //wbook.Worksheets.Add(dt, "tab1");
        //HttpResponse httpResponse = Response;
        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Listeddr_Dump.xls"));
        //Response.ContentType = "application/ms-excel";
        ////DataTable dt = BindDatatable();
        ////string str = string.Empty;
        ////foreach (DataColumn dtcol in dt.Columns)
        ////{
        ////    Response.Write(str + dtcol.ColumnName);
        ////    str = "\t";
        ////}
        ////Response.Write("\n");
        ////foreach (DataRow dr in dt.Rows)
        ////{
        ////    str = "";
        ////    for (int j = 0; j < dt.Columns.Count; j++)
        ////    {
        ////        Response.Write(str + Convert.ToString(dr[j]));
        ////        str = "\t";
        ////    }
        ////    Response.Write("\n");
        ////}

        //using (MemoryStream memoryStream = new MemoryStream())
        //{
        //    wbook.SaveAs(memoryStream);
        //    memoryStream.WriteTo(httpResponse.OutputStream);
        //    memoryStream.Close();
        //}
        //httpResponse.End();


        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Dump.csv");
        Response.Charset = "";
        Response.ContentType = "text/csv";

        using (StringWriter sw = new StringWriter())
        {

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write("\"" + dt.Columns[i].ColumnName.Replace("\"", "\"\"") + "\"");
                if (i < dt.Columns.Count - 1)
                    sw.Write(",");
            }
            sw.Write("\r\n");


            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string field = row[i].ToString().Replace("\"", "\"\"");
                    sw.Write("\"" + field + "\"");
                    if (i < dt.Columns.Count - 1)
                        sw.Write(",");
                }
                sw.Write("\r\n");
            }

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }




    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }



}
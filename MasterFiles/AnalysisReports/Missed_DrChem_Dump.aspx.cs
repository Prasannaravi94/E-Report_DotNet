using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Data.Sql;

using System.Drawing.Imaging;

public partial class MasterFiles_AnalysisReports_Missed_DrChem_Dump : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    DataSet dsLst = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    DataSet dsSf = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
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
                FillManagers();
                FillColor();
                BindDate();
            }
            else if  (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers1();

                FillColor();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                SalesForce sf = new SalesForce();
                dsSf = sf.getReportingTo(sf_code);
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }

                if (!Page.IsPostBack)
                {
                    FillMRManagers();
                }
                ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
                ddlFieldForce.Enabled = false;
                // FillColor();
                BindDate();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
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
          else  if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
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

        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();

        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "Desig_Color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();
            }
        }
        else
        {
            // Fetch Managers Audit Team
            DataSet dsmgrsf = new DataSet();
            DataSet dsTP = new DataSet();
            DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsTP = dsmgrsf;

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTP;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsTP;
            ddlSF.DataBind();
        }
        FillColor();
    }
    private void FillMRManagers1()
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
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        //dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
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
        FillColor();


    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.Hierarchy_Team(div_code, "admin");


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
    //private void FillDr()
    //{
    //    ListedDR lst = new ListedDR();
    //    dsLst = lst.getListedDr_Spec_Area(ddlFieldForce.SelectedValue);
    //    if (dsLst.Tables[0].Rows.Count > 0)
    //    {
    //        ddldoctor.DataTextField = "ListedDr_Name";
    //        ddldoctor.DataValueField = "ListedDrCode";
    //        ddldoctor.DataSource = dsLst;
    //        ddldoctor.DataBind();
    //    }
        
    //}
    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
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
               // ddlToYear.Items.Add(k.ToString());
            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();
            //ddlToYear.Text = DateTime.Now.Year.ToString();

            ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
        //    ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;
        string FMonth = ddlFrmMonth.SelectedValue.ToString();
        string FYear = ddlFrmYear.SelectedValue.ToString();
        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        string sProc_Name = "";
        DataSet ds = new DataSet();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
         
        if (ddlmode.SelectedValue == "1")
            //sProc_Name = "Missed_Dr_List_Dump";
        sProc_Name = "Missed_Dr_List_Dump_AddMgrName";
        else
           // sProc_Name = "Missed_Chem_List_Dump";
        sProc_Name = "Missed_Chem_List_Dump_AddMgrName";
        //Missed_Chem_List_Dump_AddMgrName
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@cMnth", ddlFrmMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@cYrs", ddlFrmYear.SelectedValue);
        cmd.Parameters.AddWithValue("@cdate", dtCurrent.ToString());

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        dt = ds.Tables[0];
        con.Close();

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

        }
    **/
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        wbook.Worksheets.Add(dt, "tab1");
        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"Dump.xlsx\"");

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

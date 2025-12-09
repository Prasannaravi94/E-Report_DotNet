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
using System.Text;
using Bus_EReport;
using System.Net;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;

public partial class MIS_Reports_Statusview_VisitCard_view : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sf_code = string.Empty;
    string strFieledForceName = string.Empty;
    string hq = string.Empty;
    string desig = string.Empty;
    string div_code = string.Empty;
    string flag = string.Empty;
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"].ToString();
        div_code = Request.QueryString["Div_code"].ToString();

        if (!Page.IsPostBack)
        {
            sf_code = Request.QueryString["sfcode"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            hq = Request.QueryString["sf_hq"].ToString();
            desig = Request.QueryString["sf_short"].ToString();
            flag = Request.QueryString["flg"].ToString();
         
           if (flag == "2")
            {
                lblHead.Text = "Doctor - Registration Number Details";
            }
            else
            {
                lblHead.Text = "Doctor - Visiting Card Details";
            }
            if (flag == "1" )
            {
                grdDoctor.Columns[14].Visible = false;
                //grdDoctor.Columns[12].Visible = false;

            }
            else if(flag == "2")
            {
                grdDoctor.Columns[13].Visible = false;
                grdDoctor.Columns[14].Visible = true;
                grdDoctor.Columns[12].Visible = false;

            }
            else
            {
                  grdDoctor.Columns[14].Visible = false;
                //grdDoctor.Columns[12].Visible = false;

            }

            Doctor dc = new Doctor();

            //if (sf_code.Contains("MR"))
            //{
            //    dsDoctor = dc.drsVisitcard(div_code, sf_code);
            //    // DataTable mainTable = dsDoctor.Tables[0]; 

            //    if (dsDoctor.Tables[0].Rows.Count > 0)
            //    {
            //        grdDoctor.Visible = true;
            //        grdDoctor.DataSource = dsDoctor;
            //        grdDoctor.DataBind();
            //    }
            //    else
            //    {
            //        grdDoctor.DataSource = dsDoctor;
            //        grdDoctor.DataBind();
            //    }
            //}
            //else
            {
                SqlConnection con = new SqlConnection(strConn);
                DataSet dsts = new DataSet();
                SqlCommand cmd = null;
                if (flag=="1")
                 cmd = new SqlCommand("sp_Visitcard_Status", con);
                else if(flag =="2")
                    cmd = new SqlCommand("sp_Visitcard_Status_Reg", con);
                else
                    cmd = new SqlCommand("sp_Visitcard_Status", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
                cmd.Parameters.AddWithValue("@Msf_code", sf_code);

                cmd.CommandTimeout = 600;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsts);
                //dsts.Tables[0].Columns.Remove("Sf_Code");

                grdDoctor.DataSource = dsts;
                grdDoctor.DataBind();
                
            }
        }
        LblForceName.Text = "Field Force Name : " + strFieledForceName;

        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('btnDelete').style.visibility = 'hidden';</script>");
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[14].FindControl("chkAppDel");
            bool bCheck = chkDR.Checked;
            Label lblImgpath = (Label)gridRow.Cells[12].FindControl("lblImgpath");
            string Imgpath = lblImgpath.Text.ToString();

            Label lblDrCode = (Label)gridRow.Cells[3].FindControl("lblDrCode");
            string DrCode = lblDrCode.Text.ToString();

            Label lblsf_code = (Label)gridRow.Cells[1].FindControl("lblsf_code");
            string FF_Code = lblsf_code.Text.ToString();

            if ((bCheck == true))
            {
                string url = Imgpath;
                string path = Server.MapPath(url);
                Doctor dc = new Doctor();
                iReturn = dc.VisitCard_Delete(FF_Code, DrCode, div_code);
                if (File.Exists(path))
                {
                    File.Delete(path); // deletes file from folder                   
                }
            }
        }
        if (iReturn != -1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Deactivated Successfully');", true);
        }
    }


    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblVisiting_Card = (Label)e.Row.FindControl("lblImgpath");
            //if (lblVisiting_Card.Text != "")
            //{
            //    string url = lblVisiting_Card.Text.ToString();
            //    string path = Server.MapPath(url);
            //    if (File.Exists(path))
            //    {

            //    }
            //    else
            //    {
            //        lblVisiting_Card.Text = "";
            //        e.Row.Cells[(e.Row.Cells.Count) - 2].Text = "";
            //e.Row.Cells[(e.Row.Cells.Count) - 1].Enabled = false;
            //    }
            //}
            //else
            //{
            //    lblVisiting_Card.Text = "";
            //    e.Row.Cells[(e.Row.Cells.Count) - 2].Text = "";
            //e.Row.Cells[(e.Row.Cells.Count) - 1].Enabled = false;
            //}

            
            Label lblVisiting_Card = (Label)e.Row.FindControl("lblImgpath");
            CheckBox chkAppDel = (CheckBox)e.Row.FindControl("chkAppDel");
            if (lblVisiting_Card.Text != "")
            {
                string url = lblVisiting_Card.Text.ToString();
                string path = Server.MapPath(url);
                if (File.Exists(path))
                {
                    chkAppDel.Checked = false;
                }
                else
                {
                    lblVisiting_Card.Text = "";
                    chkAppDel.Checked = true;
                    //e.Row.Cells[(e.Row.Cells.Count) - 2].Text = "";

                }
            }
           
             

        }

    }
    private void FillSalesForce()
    {
        //int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total columns for the table
        Doctor dr = new Doctor();
        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            //tot_cols = dsDoctor.Tables[0].Rows.Count;
            ViewState["dsDoctor"] = dsDoctor;
        }

        //CreateDynamicTable();
    }


    //gvImages_DeleteCommand
    //string url = ((Image)grdDoctor.Items[e.Item.ItemIdex].FindControl("PICID")).ImageUrl;

    //string path = Server.MapPath(url);

    //if (File.Exists(path))
    //{
    //    File.Delete(path); // deletes file from folder
    //}

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["ctrl"] = pnlContents;
            Control ctrl = (Control)Session["ctrl"];
            PrintWebControl(ctrl);
        }
        catch (Exception ex)
        {

        }
    }

    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }

    private void Exportbutton()
    {
        btnExcel.Visible = true;
        btnPDF.Visible = false;
        btnPrint.Visible = true;
        btnClose.Visible = true;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string Export = Title;
            string attachment = "attachment; filename=" + Export + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();
            pnlContents.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(pnlContents);
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }




    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {
            string strFileName = Title;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();
            pnlContents.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(pnlContents);
            frm.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
}
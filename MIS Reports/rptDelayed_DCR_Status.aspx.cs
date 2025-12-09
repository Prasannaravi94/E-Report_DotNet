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

public partial class MIS_Reports_rptDelayed_DCR_Status : System.Web.UI.Page
{

    int cmonth = -1;
    int cyear = -1;
    DataSet dsSalesForce = null;
    DataSet dsDCR = null;
    string sPending = string.Empty;
    string dcrdays = string.Empty;
    string SF_code = string.Empty;
    int tot_days = -1;
    string div_code = string.Empty;
    DateTime ldcrdate;
    int count = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();

        cmonth = Convert.ToInt16(Request.QueryString["cmon"].ToString());
        cyear = Convert.ToInt16(Request.QueryString["cyear"].ToString());
        SF_code = Request.QueryString["SF_code"].ToString();

        FillSalesForce(cmonth, cyear);

        string sMonth = getMonthName(cmonth) + " - " + cyear.ToString();
        lblHead.Text = lblHead.Text + sMonth;
       // ExportButton();

    }

    protected void GrdDoctor_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataSet dsSf = new DataSet();
        SalesForce sf1 = new SalesForce();
        string strSF_Code = "";
        string strNSDays = "";
        string strNSDays_Release = "";
        DateTime strJoinDate;
        DateTime LastDCRDate;
        int strNSDays1;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Desig_color")));

            strSF_Code = DataBinder.Eval(e.Row.DataItem, "Sf_Code").ToString();
            strJoinDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "sf_joining_date").ToString());
            LastDCRDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Last_DCR_Date").ToString());
            strNSDays = DataBinder.Eval(e.Row.DataItem, "DCR_Dalayed_Dates").ToString();
            strNSDays_Release = DataBinder.Eval(e.Row.DataItem, "Delayed_Dates_Release").ToString();
            Label lblResigned_Date = (Label)e.Row.FindControl("lblResigned_Date");
            //Label lblNSC = (Label)e.Row.FindControl("lblNSC");
            Label lblSf = (Label)e.Row.FindControl("lblSf");
            Label lblNSD = (Label)e.Row.FindControl("lblNSD");
            Label lblNSD_Rel = (Label)e.Row.FindControl("lblNSD_Rel");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            if (strNSDays == "" && strNSDays_Release == "")
            {
                e.Row.Visible = false;              
            }

            if (strNSDays != "" || strNSDays_Release != "")
            {
                count += 1;
                lblSNo.Text = Convert.ToString(count);
            }

            //if (count !=1)
            //{
            //    GrdDCRDelayed.Visible = false;
            //}

          

            //if (strNSDays != "")
            //{
            //    string[] str2 = strNSDays.Split(',');
            //    strNSDays1 = str2.Count();
            //    lblNSC.Text = strNSDays1.ToString();
            //}
            dsSf = sf1.CheckSFNameVacant_Temp_Delay(strSF_Code, Convert.ToInt16(cmonth), Convert.ToInt16(cyear));

            if (lblSf.Text != "")
            {
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    string[] str = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

                    if (str.Count() >= 2)
                    {
                        if ("( " + str[0].Trim() + " )" != str[1].Trim())
                        {
                            lblSf.Text = str[0] + "<span style='color: red;'>" + str[1] + "</span>" + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                            lblResigned_Date.Text = dsSf.Tables[0].Rows[0][4].ToString();
                        }
                        else
                        {
                            lblSf.Text = str[0];
                        }
                    }
                    else
                    {
                        lblSf.Text = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0) + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                        if (dsSf.Tables[0].Rows[0][1].ToString() != "")
                        {
                            lblResigned_Date.Text = dsSf.Tables[0].Rows[0][4].ToString();
                            lblNSD.Text = "";
                            lblNSD_Rel.Text = "";
                           // lblNSC.Text = "";
                        }
                    }

                    if (dsSf.Tables[0].Rows[0][2].ToString() != "")
                    {
                        lblSf.Text = "<span style='color: red;'> " + dsSf.Tables[0].Rows[0].ItemArray.GetValue(2) + " </span>";
                        lblResigned_Date.Text = dsSf.Tables[0].Rows[0][4].ToString();
                    }
                }
                else
                {
                    if (LastDCRDate.Month == strJoinDate.Month && strJoinDate.Year == LastDCRDate.Year)
                    {
                        if (LastDCRDate.Month > Convert.ToInt16(cmonth) && LastDCRDate.Year >= Convert.ToInt16(cyear))
                        {
                            lblSf.Text = "<span style='color: red;'>  Vacant  </span>";
                        }
                    }
                    else
                    {
                        lblSf.Text = lblSf.Text;
                    }
                }

                if (lblSf.Text.Trim() == "<span style='color: red;'>  Vacant  </span>" || lblSf.Text.Trim() == "Vacant")
                {
                    lblNSD.Text = "-";
                    lblNSD_Rel.Text = "-";
                    //lblNSC.Text = "";
                }

            }


        }
    }

    private void FillSalesForce(int cmonth, int cyear)
    {
        // Fetch the total rows for the table
        //DCR dc = new DCR();
        //dsSalesForce = dc.get_dcr_ff_details(cmonth, cyear);
        // Userlist

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.DCR_Delayed_Dates(div_code, SF_code, cmonth.ToString(), cyear.ToString());

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ViewState["dsSalesForce"] = dsSalesForce;
            //CreateDynamicTable();

            GrdDCRDelayed.DataSource = dsSalesForce;
            GrdDCRDelayed.DataBind();
        }
     
    }

    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 28;
        else if (imonth == 3)
            idays = 31;
        else if (imonth == 4)
            idays = 30;
        else if (imonth == 5)
            idays = 31;
        else if (imonth == 6)
            idays = 30;
        else if (imonth == 7)
            idays = 31;
        else if (imonth == 8)
            idays = 31;
        else if (imonth == 9)
            idays = 30;
        else if (imonth == 10)
            idays = 31;
        else if (imonth == 11)
            idays = 30;
        else if (imonth == 12)
            idays = 31;

        return idays;
    }

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string FileName = this.Page.Title;
        string attachment = "attachment; filename=" + FileName + ".xls";
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
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = this.Page.Title;
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
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

  
}
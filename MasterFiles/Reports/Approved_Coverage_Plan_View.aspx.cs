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
public partial class MIS_Reports_Approved_Coverage_Plan_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDoctor = null;
    DataSet dsTerritory = null;
    DataSet dsTerritory1 = null;
    DataSet dsSpec = null;
    string sf_code = string.Empty;
    string strFieledForceName = string.Empty;
    string hq = string.Empty;
    string desig = string.Empty;
    string sDRCatg_Count = string.Empty;
    string div_code = string.Empty;
    int tot_catg = 0;
    int i = -1;
    int iDRCatg = -1;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[200];
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["dsSalesForce"] = null;
        sf_code = Request.QueryString["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            sf_code = Request.QueryString["sf_code"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
          

        }
       
            div_code = Session["div_code"].ToString();

      
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }


        Territory dc = new Territory();
        dsTerritory = dc.getTerritory_Slno(sf_code);
        DataTable dist = dc.getTerr_DistAllowTable(sf_code);
        DataTable allw = dc.getTerr_AllowTable(sf_code);
        DataTable Multipleterr = dc.getTerr_drscntMultiple(sf_code);
        DataTable singleterr = dc.getTerr_drscntSingle(sf_code);
        DataTable terrtot = dc.getTerr_drscntTot(sf_code);
        DataTable mainTable = dsTerritory.Tables[0];


        mainTable.Columns.Add("Distance_in_kms");
        mainTable.Columns.Add("Allow");
        mainTable.Columns.Add("fare");
        mainTable.Columns.Add("MultiDrs");
        mainTable.Columns.Add("SingleDrs");
        mainTable.Columns.Add("tbldrs");



        if (mainTable.Rows.Count > 0)
        {
            
            foreach (DataRow row in mainTable.Rows)
            {

                String filter = "To_Code='" + row["Territory_Code"].ToString() + "'";
                DataRow[] rows = dist.Select(filter);




                if (rows.Count() > 0)
                {
                    row["Distance_in_kms"] = rows[0]["Distance_in_kms"];
                    row["fare"] = rows[0]["fare"];
                }
                String filter1 = "Territory_Code='" + row["Territory_Code"].ToString() + "'";
                DataRow[] rows1 = allw.Select(filter1);
                if (rows1.Count() > 0)
                {
                    
                    row["Allow"] = rows1[0]["Allow"];
                    
                }
                String filter2 = "Territory_Code='" + row["Territory_Code"].ToString() + "'";
                DataRow[] rows2 = Multipleterr.Select(filter2);
                if (rows2.Count() > 0)
                {

                    row["MultiDrs"] = rows2[0]["cnt"];

                }
                String filter3 = "Territory_Code='" + row["Territory_Code"].ToString() + "'";
                DataRow[] rows3 = singleterr.Select(filter3);
                if (rows3.Count() > 0)
                {

                    row["SingleDrs"] = rows3[0]["cnt"];

                }
                String filter4 = "Territory_Code='" + row["Territory_Code"].ToString() + "'";
                DataRow[] rows4 = terrtot.Select(filter4);
                if (rows4.Count() > 0)
                {

                    row["tbldrs"] = rows4[0]["cnt"];

                }

            }



            grdTerrPlan.Visible = true;
            grdTerrPlan.DataSource = mainTable;
            grdTerrPlan.DataBind();
                    int tot_cols = 0;
            Doctor dr = new Doctor();

            dsDoctor = dr.getDocCat_terr1(div_code);
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                tot_cols = dsDoctor.Tables[0].Rows.Count;
                ViewState["dsDoctor"] = dsDoctor;
            }
            CreateDynamicTable(tot_cols);
            
        }
        else
        {
            grdTerrPlan.DataSource = dsTerritory;
            grdTerrPlan.DataBind();
        }
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        DataSet expParamsAmnt = dc.getTerritory_Cnthq(sf_code);
        if (expParamsAmnt.Tables[0].Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = expParamsAmnt;
            GridView1.DataBind();

            
        }
        FillSalesForce();
       
    }




    
           

    private void CreateDynamicTable(int tblCols)
    {

        if (ViewState["dsDoctor"] != null)
        {

            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_catg = new TableCell();
            Literal lit_catg = new Literal();
            tc_catg.ForeColor = System.Drawing.Color.White;
            tc_catg.Style.Add("font-family", "Calibri");
            tc_catg.Style.Add("font-size", "10pt");


            lit_catg.Text = "<center>Expenses</center>";



            tc_catg.Controls.Add(lit_catg);
            tc_catg.BorderStyle = BorderStyle.Solid;
            tc_catg.BorderColor = System.Drawing.Color.Black;
            tc_catg.BackColor = System.Drawing.Color.FromName("#0097ac");
            tc_catg.BorderWidth = 1;
            tc_catg.ForeColor = System.Drawing.Color.White;


            tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;


            tr_header.Cells.Add(tc_catg);


            tbl.Rows.Add(tr_header);

            TableRow tr_catg = new TableRow();

            if (Session["sf_type"].ToString() == "1")
            {
                tr_catg.BackColor = System.Drawing.Color.FromName("#0097ac");

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                tr_catg.BackColor = System.Drawing.Color.FromName("#0097ac");
            }
            else
            {
                tr_catg.BackColor = System.Drawing.Color.FromName("#0097ac");
            }

            // dsDoctor = (DataSet)ViewState["dsDoctor"];

            dsDoctor = (DataSet)ViewState["dsDoctor"];




            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                TableCell tc_catg_name = new TableCell();
                tc_catg_name.BorderStyle = BorderStyle.Solid;
                tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_name.BorderWidth = 1;
                tc_catg_name.BorderColor = System.Drawing.Color.Black;
                tc_catg_name.Width = 60;
                tc_catg_name.ForeColor = System.Drawing.Color.White;
                Literal lit_catg_name = new Literal();
                lit_catg_name.Text = "<center>" + dataRow["Expense_Parameter_Name"].ToString() + "</center>";
                tc_catg_name.Controls.Add(lit_catg_name);
                tc_catg_name.Style.Add("font-family", "Calibri");
                tc_catg_name.Style.Add("font-size", "10pt");
                tr_catg.Cells.Add(tc_catg_name);
            }

            tbl.Rows.Add(tr_catg);

            // Details Section
            string sURL = string.Empty;

            iTotal_FF = 0;
            i = 0;
            TableRow tr_det = new TableRow();
            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                TableCell tc_catg_det_name = new TableCell();

                HyperLink hyp_catg_det_name = new HyperLink();


                Doctor dr_cat = new Doctor();

                iDRCatg = dr_cat.getFixedValues(sf_code, i);


                if (Session["sf_type"].ToString() == "1")
                {
                    sf_code = Request.QueryString["sf_code"].ToString();

                }
                else
                {
                    sf_code = Request.QueryString["sf_code"].ToString();
                }
                if (iDRCatg == 0)
                {
                    sDRCatg_Count = " - ";
                }

                else
                {
                    sDRCatg_Count = iDRCatg.ToString();
                    iTotal_FF = iTotal_FF + iDRCatg;
                    //hyp_catg_det_name.NavigateUrl = "rptDoctorCategory.aspx?cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString();

                    //sURL = "rptListedDr_View.aspx?sf_code=" + sf_code + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                    // hyp_catg_det_name.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                    // hyp_catg_det_name.NavigateUrl = "#";


                    iTotal_Catg[i] = iTotal_Catg[i] + iDRCatg;
                }

                hyp_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

                tc_catg_det_name.BorderStyle = BorderStyle.Solid;
                tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
                tc_catg_det_name.BorderWidth = 1;

                tc_catg_det_name.Controls.Add(hyp_catg_det_name);
                tr_det.Style.Add("text-align", "left");
                tr_det.Style.Add("font-family", "Calibri");
                tr_det.Cells.Add(tc_catg_det_name);

                i++;

            }

            tbl.Rows.Add(tr_det);
        }
        else
        {
            lblNoRecord.Visible = true;
        }


    }
    private void FillSalesForce()
    {

        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        Territory terr = new Territory();
        dsSalesForce = terr.getmod();
        int tot_rows1 = 0;
        int tot_cols1 = 0;

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tot_rows1 = dsSalesForce.Tables[0].Rows.Count;
            ViewState["dsSalesForce"] = dsSalesForce;

        }

        Doctor dr1 = new Doctor();


        dsSpec = dr1.getDocSpec(div_code);
        if (dsSpec.Tables[0].Rows.Count > 0)
        {
            ViewState["dsSpec"] = dsSpec;
        }
        CreateDynamicTable2(tot_rows1, tot_cols1);
    }


    private void CreateDynamicTable2(int tblRows, int tblCols)
    {
        Territory terr = new Territory();
        dsTerritory1 = terr.getWorkAreaName(div_code);
        if (dsTerritory1.Tables[0].Rows.Count > 0)
        {
            string str = "" + dsTerritory1.Tables[0].Rows[0]["wrk_area_Name"];

        }
        if (ViewState["dsSalesForce"] != null)
        {
            ViewState["HQ_Det"] = null;
            
            dsSpec = (DataSet)ViewState["dsSpec"];
            

            TableRow tr_header1 = new TableRow();
            tr_header1.BorderStyle = BorderStyle.Solid;
            tr_header1.BorderWidth = 1;


            TableCell tc_FF1 = new TableCell();
            tc_FF1.BorderStyle = BorderStyle.Solid;
            tc_FF1.BorderWidth = 1;
            tc_FF1.Width = 250;
            tc_FF1.BorderColor = System.Drawing.Color.Black;
            tc_FF1.BackColor = System.Drawing.Color.FromName("#0097ac");
            Literal lit_FF1 = new Literal();
            if (dsTerritory1.Tables[0].Rows.Count > 0)
            {
                lit_FF1.Text = " <center> Doctor </center>";
            }
            else
            {
                lit_FF1.Text = "";
            }
            //lit_FF.Text = " <center> "+ dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() +" </center>";
            tc_FF1.Controls.Add(lit_FF1);
            tc_FF1.ForeColor = System.Drawing.Color.White;
            tc_FF1.RowSpan = 2;
            tc_FF1.Style.Add("margin-top", "20px");
            tc_FF1.Style.Add("font-family", "Calibri");
            tc_FF1.Style.Add("font-size", "10pt");

            tr_header1.Cells.Add(tc_FF1);

            //TableCell tc_catg = new TableCell();
            //Literal lit_catg = new Literal();          
            //lit_catg.Text = "<center>Category</center>"; 

            //tc_catg.Controls.Add(lit_catg);
            //tc_catg.BorderStyle = BorderStyle.Solid;
            //tc_catg.BorderColor = System.Drawing.Color.Black;
            //tc_catg.BorderWidth = 1;
            //tc_catg.ForeColor = System.Drawing.Color.White;
            //tc_catg.Style.Add("font-family", "Calibri");
            //tc_catg.Style.Add("font-size", "10pt");           
            //tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;        
            //tr_header.Cells.Add(tc_catg);

            TableCell tc_catg1 = new TableCell();
            Literal lit_catg1 = new Literal();
            tc_catg1.ForeColor = System.Drawing.Color.White;
            tc_catg1.Style.Add("font-family", "Calibri");
            tc_catg1.Style.Add("font-size", "10pt");
                     
                lit_catg1.Text = "<center>Speciality</center>";
           


            tc_catg1.Controls.Add(lit_catg1);
            tc_catg1.BorderStyle = BorderStyle.Solid;
            tc_catg1.BorderColor = System.Drawing.Color.Black;
            tc_catg1.BorderWidth = 1;
            tc_catg1.ForeColor = System.Drawing.Color.White;
            tc_catg1.BackColor = System.Drawing.Color.FromName("#0097ac");

           
                tc_catg1.ColumnSpan = dsSpec.Tables[0].Rows.Count;
           


            tr_header1.Cells.Add(tc_catg1);




            tbldrs.Rows.Add(tr_header1);

            TableRow tr_catg1 = new TableRow();

            if (Session["sf_type"].ToString() == "1")
            {
                tr_catg1.BackColor = System.Drawing.Color.FromName("#0097ac");

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                tr_catg1.BackColor = System.Drawing.Color.FromName("#0097ac");
            }
            else
            {
                tr_catg1.BackColor = System.Drawing.Color.FromName("#0097ac");
            }

            // dsDoctor = (DataSet)ViewState["dsDoctor"];
           
                dsDoctor = (DataSet)ViewState["dsSpec"];
            



            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                TableCell tc_catg_name1 = new TableCell();
                tc_catg_name1.BorderStyle = BorderStyle.Solid;
                tc_catg_name1.HorizontalAlign = HorizontalAlign.Center;
                tc_catg_name1.BorderWidth = 1;
                tc_catg_name1.BorderColor = System.Drawing.Color.Black;
                tc_catg_name1.Width = 60;
                tc_catg_name1.ForeColor = System.Drawing.Color.White;
                Literal lit_catg_name1 = new Literal();
                lit_catg_name1.Text = "<center>" + dataRow["Doc_Cat_Name"].ToString() + "</center>";
                tc_catg_name1.Controls.Add(lit_catg_name1);
                tc_catg_name1.Style.Add("font-family", "Calibri");
                tc_catg_name1.Style.Add("font-size", "10pt");
                tr_catg1.Cells.Add(tc_catg_name1);
            }

            tbldrs.Rows.Add(tr_catg1);

            // Details Section
            string sURL = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                TableRow tr_det1 = new TableRow();

                TableCell tc_det_FF1 = new TableCell();
                Literal lit_det_FF1 = new Literal();
                lit_det_FF1.Text = "&nbsp" + drFF["mode"].ToString();
                tc_det_FF1.BorderStyle = BorderStyle.Solid;
                tc_det_FF1.BorderWidth = 1;
                tc_det_FF1.Controls.Add(lit_det_FF1);
                tc_det_FF1.Style.Add("text-align", "left");
                tc_det_FF1.Style.Add("font-family", "Calibri");
                tr_det1.Cells.Add(tc_det_FF1);

                iTotal_FF = 0;
                i = 0;
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_catg_det_name1 = new TableCell();
                    HyperLink hyp_catg_det_name1 = new HyperLink();


                    Doctor dr_cat = new Doctor();
                    
                        if (drFF["mode"].ToString() == "2**")
                        {
                            iDRCatg = dr_cat.getSpec_drscntMultiple(dataRow["Doc_Cat_Code"].ToString(), sf_code);
                        }
                        else
                        {
                            iDRCatg = dr_cat.getSpec_drscntSingle(dataRow["Doc_Cat_Code"].ToString(), sf_code);
                        }
                    

                    if (Session["sf_type"].ToString() == "1")
                    {
                        sf_code = Request.QueryString["sf_code"].ToString();

                    }
                    else
                    {
                        sf_code = Request.QueryString["sf_code"].ToString();
                    }
                    if (iDRCatg == 0)
                    {
                        sDRCatg_Count = " - ";
                    }

                    else
                    {
                        sDRCatg_Count = iDRCatg.ToString();
                        iTotal_FF = iTotal_FF + iDRCatg;
                        //hyp_catg_det_name.NavigateUrl = "rptDoctorCategory.aspx?sf_code=" + drFF["sf_code"].ToString() + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&sf_name=" + drFF["sf_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString();

                        //sURL = "rptListedDr_View.aspx?sf_code=" + sf_code + "&cat_code=" + dataRow["Doc_Cat_Code"].ToString() + "&cat_name=" + dataRow["Doc_Cat_name"].ToString() + "&type=" + rdoType.SelectedValue.ToString() + "&div=" + div_code;
                        //hyp_catg_det_name.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=yes,status=no,width=600,height=800,left=0,top=0');";
                        // hyp_catg_det_name.NavigateUrl = "#";


                        iTotal_Catg[i] = iTotal_Catg[i] + iDRCatg;
                    }

                    hyp_catg_det_name1.Text = "<center>" + sDRCatg_Count + "</center>";

                    tc_catg_det_name1.BorderStyle = BorderStyle.Solid;
                    tc_catg_det_name1.VerticalAlign = VerticalAlign.Middle;
                    tc_catg_det_name1.BorderWidth = 1;

                    tc_catg_det_name1.Controls.Add(hyp_catg_det_name1);
                    tr_det1.Style.Add("text-align", "left");
                    tr_det1.Style.Add("font-family", "Calibri");
                    tr_det1.Cells.Add(tc_catg_det_name1);

                    i++;
                }



                tbldrs.Rows.Add(tr_det1);
            }


        }
        else
        {
            lblrcd.Visible = true;
            
        }
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
    
}
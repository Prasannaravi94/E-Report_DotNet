using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_AnalysisReports_rpt_Lstdr_Remarks : System.Web.UI.Page
{
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    DataSet dsChemist = null;
    DataSet dsterrType = null;
    DataSet dsDivision = null;
    bool isff = false;
    //   int iDRCatg = -1;
    DataSet iDRCatg = new DataSet();
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[20];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    string strFieledForceName = string.Empty;

    DataSet dsState = null;
    string sState = string.Empty;
    string[] statecd;
    string slno;
    string state_cd = string.Empty;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFrmMonth = string.Empty;
    DataSet dsmgrsf = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_Code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        strFrmMonth = sf.getMonthName(FMonth);
        FillSalesForce();
    }
    private void FillSalesForce()
    {
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total rows for the table
        SalesForce sf = new SalesForce();
        Territory terr = new Territory();

        // dsSalesForce = terr.getTerritory(sf_code);
        AdminSetup adm = new AdminSetup();
        //DataTable dt = sf1.getMRJointWork_camp(div_code, sf_code, 0);
        //dsmgrsf.Tables.Add(dt);
        DataSet dsFF = new DataSet();
        dsSalesForce = adm.get_day(Convert.ToInt32(FMonth));
        //dsmgrsf.Tables.Add(dt);
        // dsSalesForce = dsmgrsf;
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tot_rows = dsSalesForce.Tables[0].Rows.Count;
            ViewState["dsSalesForce"] = dsSalesForce;
        }

        lblHead.Text = "Visit (Daywise Remarks) - " + strFrmMonth + " " + FYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;

        CreateDynamicTable(tot_rows, tot_cols);
    }
    private void CreateDynamicTable(int tblRows, int tblCols)
    {

        if (ViewState["dsSalesForce"] != null)
        {
            ViewState["HQ_Det"] = null;


            TableRow tr_header = new TableRow();
            //tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.BorderWidth = 1;
            //tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            //tr_header.ForeColor = System.Drawing.Color.White;

            TableCell tc_FF = new TableCell();
            //tc_FF.BorderStyle = BorderStyle.Solid;
            //tc_FF.BorderWidth = 1;

            tc_FF.BorderColor = System.Drawing.Color.Black;

            Literal lit_FF = new Literal();
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);

            lit_FF.Text = "<center>Day</center>";

            tc_FF.Controls.Add(lit_FF);
            //tc_FF.ForeColor = System.Drawing.Color.White;
            tc_FF.RowSpan = 2;

            //tc_FF.Style.Add("font-family", "Calibri");
            //tc_FF.Style.Add("font-size", "10pt");
            //tc_FF.Style.Add("border-color", "Black");
            tc_FF.Attributes.Add("class", "stickyFirstRow");
            tr_header.Cells.Add(tc_FF);

            SalesForce sf1 = new SalesForce();
            DataTable dt = sf1.getMRJointWork_camp(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsDoctor = dsmgrsf;
            TableCell tc_catg = new TableCell();
            Literal lit_catg = new Literal();
            //tc_catg.ForeColor = System.Drawing.Color.White;
            //tc_catg.Style.Add("font-family", "Calibri");
            //tc_catg.Style.Add("font-size", "10pt");
            tc_catg.Attributes.Add("class", "stickyFirstRow");
            lit_catg.Text = " " + strFrmMonth + " " + FYear;

            tc_catg.HorizontalAlign = HorizontalAlign.Center;
            tc_catg.Controls.Add(lit_catg);
            //tc_catg.BorderStyle = BorderStyle.Solid;
            //tc_catg.BorderColor = System.Drawing.Color.Black;
            //tc_catg.BorderWidth = 1;
            //tc_catg.ForeColor = System.Drawing.Color.White;
            tc_catg.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
            tr_header.Cells.Add(tc_catg);

            tbl.Rows.Add(tr_header);

            //tc_catg.ColumnSpan = dsQual.Tables[0].Rows.Count;

            TableRow tr_catg = new TableRow();
            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
            {
                TableCell tc_catg_name = new TableCell();
                //tc_catg_name.BorderStyle = BorderStyle.Solid;
                tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
                //tc_catg_name.BorderWidth = 1;
                //tc_catg_name.BorderColor = System.Drawing.Color.Black;
                //tc_catg_name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
                //tc_catg_name.ForeColor = System.Drawing.Color.White;
                Literal lit_catg_name = new Literal();
                lit_catg_name.Text = dataRow["sf_Designation_Short_Name"].ToString();
                tc_catg_name.Controls.Add(lit_catg_name);
                //tc_catg_name.Style.Add("font-family", "Calibri");
                //tc_catg_name.Style.Add("font-size", "10pt");
                tc_catg_name.Attributes.Add("class", "stickySecondRow");
                tr_catg.Cells.Add(tc_catg_name);
                // tbl.Rows.Add(tr_catg);
            }
            //   tbl.Cells.Add(tc_catg_name);


            tbl.Rows.Add(tr_catg);


            string sURL = string.Empty;
            int iCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();

                //tr_det.Height = 10;


                tr_det.BackColor = System.Drawing.Color.White;
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp" + drFF["day"].ToString();
                //tc_det_FF.BorderStyle = BorderStyle.Solid;
                //tc_det_FF.BorderWidth = 1;
                tc_det_FF.Controls.Add(lit_det_FF);
                //tc_det_FF.Style.Add("text-align", "left");

                //tc_det_FF.Style.Add("font-family", "Calibri");
                //tc_det_FF.Style.Add("font-size", "10pt");
                //tc_det_FF.Style.Add("border-color", "Black");
         
                tr_det.Cells.Add(tc_det_FF);

                iTotal_FF = 0;
                i = 0;
                //foreach (DataRow dataRow in dsSalesForce.Tables[0].Rows)
                //{
                //    TableCell tc_catg_det_name = new TableCell();
                //    HyperLink hyp_catg_det_name = new HyperLink();
                DataSet dsdoc = new DataSet();
                SalesForce dr_cat = new SalesForce();
                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                {
                    TableCell tc_catg_det_name = new TableCell();
                    Literal hyp_catg_det_name = new Literal();
                 
                

                    // iDRCatg = dr_cat.getUnlistDoctorMRcount(drFF["Territory_Code"].ToString(), dataRow["Doc_Cat_Code"].ToString());

                    dsdoc = dr_cat.Get_Remarks(dataRow["sf_Code"].ToString(), drFF["day"].ToString(), Convert.ToInt32(FMonth), Convert.ToInt32(FYear));



                    if (dsdoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_catg_det_name.Text = dsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        hyp_catg_det_name.Text = " ";
                    }
                    //tc_catg_det_name.BorderStyle = BorderStyle.Solid;
                    tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
                    //tc_catg_det_name.BorderWidth = 1;

                    tc_catg_det_name.Controls.Add(hyp_catg_det_name);
                    //tr_det.Style.Add("text-align", "left");
                    //tr_det.Style.Add("font-family", "Calibri");
                    //tr_det.Style.Add("font-size", "10pt");
                    //tr_det.Style.Add("border-color", "Black");
                    tr_det.Cells.Add(tc_catg_det_name);

                    i++;
                }


                tbl.Rows.Add(tr_det);
            }


        }
    }
}
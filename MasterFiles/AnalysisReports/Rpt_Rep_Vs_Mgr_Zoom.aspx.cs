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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_AnalysisReports_Rpt_Rep_Vs_Mgr_Zoom : System.Web.UI.Page
{
    string div_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string sf_code = string.Empty;
    string sfname = string.Empty;
    string mr_code = string.Empty;
    string mrname = string.Empty;
    string fday = string.Empty;
    DataSet dsdoctor = new DataSet();
    DataSet dsdoc_Mr = new DataSet();
    DataTable dtrowMgr = new DataTable();
    DataTable dtrowMr = new DataTable();
    List<string> iLstmgr = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            sfname = Request.QueryString["sfname"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            mrname = Request.QueryString["mrname"].ToString();
            mr_code = Request.QueryString["mrcode"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();
            fday = Request.QueryString["fday"].ToString();
            FillMr();
            FillMGR();
        }
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Label lbldocname = (Label)e.Row.FindControl("lblDocName");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            ListedDR lst = new ListedDR();
            dsdoctor = lst.getDoc_Notequal_Visit(div_code, mr_code, sf_code, FMonth, FYear, fday);
            if (dsdoctor.Tables[0].Rows.Count > 0)
            {

                for (int j =0; j < dsdoctor.Tables[0].Rows.Count; j++)
                {

                    if (lbldocname.Text == dsdoctor.Tables[0].Rows[j]["ListedDr_Name"].ToString())
                    {
                      //  e.Row.Attributes.Add("style", "background-color:" + "Red");
                       // e.Row.Attributes.Add("style", "background-color:Red;font-bold:true; font-size:14px; Color:White; border-color:Black");
                        //e.Row.Attributes.CssStyle.Value = "background-color: Red; color: White";
                        //lbldocname.Style.Add("color", "white");
                        //lblSNo.Style.Add("color", "white");


                    }
                    
                }

            }
      
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = Request.QueryString["mrname"].ToString() + "<br>" + " Visit Doctors Name " + fday + " / " + FMonth + " / " + FYear;
        }
    }

    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            //GridView objGridView = (GridView)sender;

            ////Creating a gridview row object
            //GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            ////Creating a table cell object
            //TableCell objtablecell = new TableCell();
            //TableCell objtablecell2 = new TableCell();
            //#region Merge cells

            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "LightPink", true);

            //AddMergedCells(objgridviewrow, objtablecell2, 0, 0, Request.QueryString["mrname"].ToString(), "LightPink", false);
            //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Doctors Name " + fday + " / " + FMonth + " / " + FYear, "LightPink", false);
             
           

            //objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            //objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);


           
        }
    }
    protected void grdmgr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            Label lbldocname = (Label)e.Row.FindControl("lblDocName");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            ListedDR lst = new ListedDR();
            dsdoctor = lst.getDoc_Notequal_Visit_MGR(div_code, mr_code, sf_code, FMonth, FYear, fday);
            if (dsdoctor.Tables[0].Rows.Count > 0)
            {

                for (int j = 0; j < dsdoctor.Tables[0].Rows.Count; j++)
                {

                    if (lbldocname.Text == dsdoctor.Tables[0].Rows[j]["ListedDr_Name"].ToString())
                    {
                        //  e.Row.Attributes.Add("style", "background-color:" + "Red");
                        //   e.Row.Attributes.Add("style", "background-color:Green;Color:White; ");
                        //e.Row.Attributes.CssStyle.Value = "background-color: Green; color: White";
                        //lbldocname.Style.Add("color", "white");
                        //lblSNo.Style.Add("color", "white");
                    }
                  
                }

            }
            
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = Request.QueryString["sfname"].ToString() + "<br>" + " Visit Doctors Name " + fday + " / " + FMonth + " / " + FYear;
        }

    }
    protected void grdmgr_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            //GridView objGridView = (GridView)sender;

            ////Creating a gridview row object
            //GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            ////Creating a table cell object
            //TableCell objtablecell = new TableCell();
            //TableCell objtablecell2 = new TableCell();
            //#region Merge cells

            //AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "LightPink", true);

            //AddMergedCells(objgridviewrow, objtablecell2, 0, 0, Request.QueryString["sfname"].ToString(), "LightPink", false);
            //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Visit Doctors Name " +fday + " / " + FMonth + " / " + FYear, "LightPink", false);



            //objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            //objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);


            //#endregion
        }
    }
   

    private void FillMr()
    {
        ListedDR lst = new ListedDR();
        dsdoc_Mr = lst.getDoc_Rep_Mgr(div_code, mr_code, sf_code, FMonth, FYear, fday);
        if (dsdoc_Mr.Tables[0].Rows.Count > 0)
        {
            GrdFixation.DataSource = dsdoc_Mr;
            GrdFixation.DataBind();
           
        }
    }
    private void FillMGR()
    {
        ListedDR lst = new ListedDR();
        dsdoctor = lst.getDoc_Rep_Mgr(div_code, sf_code, mr_code, FMonth, FYear, fday);
        if (dsdoctor.Tables[0].Rows.Count > 0)
        {
            grdmgr.DataSource = dsdoctor;
            grdmgr.DataBind();
            
       
        }
    }
}

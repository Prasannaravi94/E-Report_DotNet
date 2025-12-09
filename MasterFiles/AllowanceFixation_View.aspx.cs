using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Data.SqlClient;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Web.Script.Serialization;

public partial class MasterFiles_AllowanceFixation_View : System.Web.UI.Page
{

    string div_code = string.Empty;
    string sfCode = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsExp = new DataSet();
    DataSet dsSFMR = new DataSet();
    TextBox TextBox1 = new TextBox();
    static DataTable Sdt = new DataTable();
    static DataTable dt = new DataTable();
    static DataTable wrkType_dt = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();

            
            if (!Page.IsPostBack)
            {
                //menu1.Title = Page.Title;
                //// menu1.FindControl("btnBack").Visible = false;
                FillReporting();
                BindSelectedValue();
               
               
                
            }
            pnlExcel.Visible = true;
           
        }
        catch (Exception ex)
        {

        }        
    }

    private void FillReporting()
    {
        Territory sf = new Territory();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.getExp_Managers_View(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataTextField = "Sf_Name";
            ddlRegion.DataValueField = "Sf_Code";
            ddlRegion.DataSource = dsSalesForce;
            ddlRegion.DataBind();

        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSelectedValue();
        }
        catch (Exception ex)
        {

        }
    }

    private void BindSelectedValue()
    {
            
        Territory terr = new Territory();
        grdWTAllowance.DataSource = null;
        grdWTAllowance.DataBind();
        
        dsSFMR = sf.GetExp_AllwanceFixation(div_code, ddlRegion.SelectedValue);      

        if (dsSFMR.Tables[0].Rows.Count > 0)
        {
           
        }
        else
        {
            dsSFMR = sf.SalesForceListMgrGet(div_code, ddlRegion.SelectedValue);
            
           
        }
        dsExp = terr.getWorkType_allow_type(div_code);
        if (dsExp.Tables[0].Rows.Count > 0)
        {
            wrkType_dt = dsExp.Tables[0];
            dsSFMR.Tables[0].Merge(wrkType_dt);

            for (int i = 0; i < wrkType_dt.Columns.Count; i++)
            {

                grdWTAllowance.Columns[18 + i].Visible = true;
                grdWTAllowance.Columns[18 + i].HeaderText = wrkType_dt.Columns[i].ColumnName;
            }
          

        }

        dsExp = terr.getExp_FixedType1(div_code);
        string strTextbox = string.Empty;
        DataRow dr = null;
        if (dsExp.Tables[0].Rows.Count > 0)
        {
            dt = dsExp.Tables[0];
            dsSFMR.Tables[0].Merge(dt);
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                grdWTAllowance.Columns[13 + i].Visible = true;
                grdWTAllowance.Columns[13 + i].HeaderText = dt.Columns[i].ColumnName;
            }
          

        }
      

        grdWTAllowance.DataSource = dsSFMR;
        grdWTAllowance.DataBind();
        GrdViewExpense.DataSource = dsSFMR;
        GrdViewExpense.DataBind();
        DataTable rGdT = new DataTable();
        rGdT = sf.GetRange(div_code);

        GridViewRow row = grdWTAllowance.HeaderRow;

        if (rGdT.Rows.Count > 0)
        {
            ((Label)row.FindControl("txtRange")).Text = rGdT.Rows[0]["Range1_KMS"].ToString();
            ((RadioButtonList)row.FindControl("rbtLstRating")).SelectedValue = rGdT.Rows[0]["Range1_status"].ToString();

            ((Label)row.FindControl("txtRange2")).Text = rGdT.Rows[0]["Range2_KMS"].ToString();
            ((RadioButtonList)row.FindControl("rbtLstRating2")).SelectedValue = rGdT.Rows[0]["Range2_status"].ToString();
        }
        else
        {
            ((Label)row.FindControl("txtRange")).Text = "0";
            ((RadioButtonList)row.FindControl("rbtLstRating")).SelectedValue = "Consolidate";

            ((Label)row.FindControl("txtRange2")).Text = "0";
            ((RadioButtonList)row.FindControl("rbtLstRating2")).SelectedValue = "Consolidate";

        }

      
      
    }

    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        pnlExcel.Visible = true;
        BindSelectedValue();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Export.xls";
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
}

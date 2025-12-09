using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
public partial class MasterFiles_UniqueDr_Approval_Pending : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsListedDR = null;
    string sQryStr = string.Empty;
    int search = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
     
        if (!Page.IsPostBack)
        {
            
         //menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            btnAdd_Pending.BackColor = Color.LightPink;
            btnDeact_Pending.BackColor = Color.LightPink;
            btnReject.BackColor = Color.LightGreen;
            lblADD.Visible = true;
            FillDoc_Reject();
        }        
    }
  
    private void FillDoc()
    {
        grdDoctor.Columns[9].Visible = false;
        grdDoctor.Columns[10].Visible = false;
        grdDoctor.Columns[12].Visible = false;
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_Add_approve_FDC_New(sfCode, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {           
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
    }
    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Territory terr = new Territory();
            //DataSet dsTerritory = new DataSet();
            //dsTerritory = terr.getWorkAreaName(div_code);
            //if (dsTerritory.Tables[0].Rows.Count > 0)
            //{
            //    e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();                      
            //}
        }
    }
    private void FillDoc_React()
    {
        grdDoctor.Columns[9].Visible = false;
        grdDoctor.Columns[10].Visible = false;
        grdDoctor.Columns[11].Visible = false;
        grdDoctor.Columns[12].Visible = false;
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.Unique_Deact_approve_New(sfCode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {          
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
    }
    private void FillDoc_Reject()
    {
       grdDoctor.Columns[9].Visible = true;
       grdDoctor.Columns[10].Visible = true;
       grdDoctor.Columns[11].Visible = true;
       grdDoctor.Columns[12].Visible = true;
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.Unique_Reject_New(sfCode);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
            foreach (GridViewRow row in grdDoctor.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkcount");
                Label lblmode = (Label)row.FindControl("lblmode");

                // if (Convert.ToInt32(dsDocCls.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if (lblmode.Text != "New")
                {
                    lnkdeact.Visible = false;

                }
            }
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
    }   
    protected DataSet Doc_Territory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(sfCode);
        return dsListedDR;
    }

    protected DataSet Doc_Category()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sfCode);
        return dsListedDR;
    }

    protected DataSet Doc_Speciality()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchSpeciality(sfCode);
        return dsListedDR;
    }

    protected DataSet Doc_Class()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchClass(sfCode);
        return dsListedDR;
    }
    protected DataSet Doc_Qualification()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchQualification(sfCode);
        return dsListedDR;
    }

    protected void btnAdd_Pending_Click(object sender, EventArgs e)
    {
        lblADD.Visible = true;
        lblDeAct.Visible = false;
        lblReject.Visible = false;
        btnAdd_Pending.BackColor = Color.LightGreen;
        btnDeact_Pending.BackColor = Color.LightPink;
        btnReject.BackColor = Color.LightPink;
        FillDoc();
       
    }
    protected void btnDeact_Pending_Click(object sender, EventArgs e)
    {
        lblADD.Visible = false;
        lblDeAct.Visible = true;
        lblReject.Visible = false;
        btnDeact_Pending.BackColor = Color.LightGreen;
        btnAdd_Pending.BackColor = Color.LightPink;
        btnReject.BackColor = Color.LightPink;
        FillDoc_React();
        
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        lblReject.Visible = true;
        lblADD.Visible = false;
        lblDeAct.Visible = false;
        btnDeact_Pending.BackColor = Color.LightPink;
        btnAdd_Pending.BackColor = Color.LightPink;
        btnReject.BackColor = Color.LightGreen;
        FillDoc_Reject();
    }
}
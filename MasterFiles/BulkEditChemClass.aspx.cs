using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_BulkEditChemCat : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemClass = null;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        heading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ChemistClassList.aspx";
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillChemClass();
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void FillChemClass()
    {
        Chemist chem = new Chemist();
        dsChemClass = chem.getChemClass(div_code);
        if (dsChemClass.Tables[0].Rows.Count > 0)
        {
            grdChemClass.Visible = true;
            grdChemClass.DataSource = dsChemClass;
            grdChemClass.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdChemClass.DataSource = dsChemClass;
            grdChemClass.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string chem_class_code = string.Empty;
        string chem_class_sname = string.Empty;
        string chem_class_name = string.Empty;

        Chemist chem = new Chemist();

        int iReturn = -1;
        bool err = false;

        foreach (GridViewRow gridRow in grdChemClass.Rows)
        {
            Label lblChemClassCode = (Label)gridRow.Cells[1].FindControl("lblChemClassCode");
            chem_class_code = lblChemClassCode.Text;
            TextBox txtChem_class_SName = (TextBox)gridRow.Cells[1].FindControl("txtChem_class_SName");
            chem_class_sname = txtChem_class_SName.Text;
            TextBox txtChemClassName = (TextBox)gridRow.Cells[1].FindControl("txtChemClassName");
            chem_class_name = txtChemClassName.Text;

            iReturn = chem.RecordUpdate_Chem_code(Convert.ToInt16(chem_class_code), chem_class_sname, chem_class_name, div_code);

            if (iReturn > 0)
                err = false;

            if ((iReturn == -2))
            {
                txtChemClassName.Focus();
                err = true;
                break;
            }

            if ((iReturn == -3))
            {
                txtChem_class_SName.Focus();
                err = true;
                break;
            }
        }
        if (err == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ChemistClassList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Class Name Already Exist');</script>");
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChemistClassList.aspx");
    }
}
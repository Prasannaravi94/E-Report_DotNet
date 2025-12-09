using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_ChemistCategory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemClass = null;
    string divcode = string.Empty;
    string Chem_Class_SName = string.Empty;
    string ChemClassName = string.Empty;
    string Chem_Class_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ChemistClassList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        Chem_Class_Code = Request.QueryString["Class_Code"];
        heading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = true;

            if (Chem_Class_Code != "" && Chem_Class_Code != null)
            {
                Chemist chem = new Chemist();
                dsChemClass = chem.getChemClass_code(divcode, Chem_Class_Code);
                if (dsChemClass.Tables[0].Rows.Count > 0)
                {
                    txtChem_Class_SName.Text = dsChemClass.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtChemClassName.Text = dsChemClass.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
        }
        txtChem_Class_SName.Focus();

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Chemist chem = new Chemist();
        Chem_Class_SName = txtChem_Class_SName.Text;
        ChemClassName = txtChemClassName.Text;

        if (Chem_Class_Code == null)
        {
            int iReturn = chem.RecordAdd_chemClass(divcode, Chem_Class_SName, ChemClassName);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtChemClassName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtChem_Class_SName.Focus();
            }
        }
        else
        {
            int ChemClassCode = Convert.ToInt16(Chem_Class_Code);
            int iReturn = chem.RecordUpdate_ChemClass_code(ChemClassCode, Chem_Class_SName, ChemClassName, divcode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ChemistClassList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
                txtChemClassName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtChem_Class_SName.Focus();
            }
        }
    }
    private void Resetall()
    {
        txtChem_Class_SName.Text = "";
        txtChemClassName.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChemistClassList.aspx");
    }
}
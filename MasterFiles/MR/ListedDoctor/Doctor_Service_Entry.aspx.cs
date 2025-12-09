using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_Entry : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsDoc = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DataSet dsListedDR = null;
    DataSet dsTerritory = null;
    DataSet dsSalesForce = null;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string sCmd = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    string doc_code = string.Empty;
    string Territory_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    string sf_code = string.Empty;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strDeact = string.Empty;
    string strView = string.Empty;
    string ClsSName = string.Empty;
    string QuaSName = string.Empty;
    string CatSName = string.Empty;
    string SpecSName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        Session["backurl"] = "Doctor_Service_CRM.aspx";

        string doctorcode = Request.QueryString["ListedDrCode"];

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = true;

            if (Request.QueryString["ListedDrCode"] != null)
            {
                doc_code = Convert.ToString(Request.QueryString["ListedDrCode"]);

                LoadDoctor(doc_code);
            }           
        }
    }

    private void LoadDoctor(string docCode)
    {
        ListedDR lst = new ListedDR();
        dsListedDR = lst.ViewListedDr(docCode);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            doc_code = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txt_DoctorName.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txt_Doctor_Address.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txt_Category.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txt_Speciality.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            txt_Qualification.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            txt_Class.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            txt_Mobile.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            txt_Email.Text = dsListedDR.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();

            txt_DoctorName.Enabled = false;
            txt_Doctor_Address.Enabled = false;
            txt_Category.Enabled = false;
            txt_Speciality.Enabled = false;
            txt_Qualification.Enabled = false;
            txt_Class.Enabled = false;
            txt_Mobile.Enabled = false;
            txt_Email.Enabled = false;

        }
    }
}
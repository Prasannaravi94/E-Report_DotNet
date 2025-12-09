using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_Resigned_User_Status : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string tot_dr = string.Empty;
    string total_doc = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsTP = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
           // Filldiv();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //// menu1.FindControl("btnBack").Visible = false;
            FillYear();
        }


    }

    //private void Filldiv()
    //{
    //    Division dv = new Division();
    //    if (sf_type == "3")
    //    {
    //        string[] strDivSplit = div_code.Split(',');
    //        foreach (string strdiv in strDivSplit)
    //        {
    //            if (strdiv != "")
    //            {
    //                dsdiv = dv.getDivisionHO(strdiv);
    //                ListItem liTerr = new ListItem();
    //                liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
    //                ddlDivision.Items.Add(liTerr);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        dsDivision = dv.getDivision_Name();
    //        if (dsDivision.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDivision.DataTextField = "Division_Name";
    //            ddlDivision.DataValueField = "Division_Code";
    //            ddlDivision.DataSource = dsDivision;
    //            ddlDivision.DataBind();
    //        }
    //    }
    //}

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlFYear.Items.Add(k.ToString());
                //ddlTYear.Items.Add(k.ToString());
                //ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        DateTime FromMonth = DateTime.Now;
        DateTime ToMonth = DateTime.Now;
        txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        //ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        //ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

  
}
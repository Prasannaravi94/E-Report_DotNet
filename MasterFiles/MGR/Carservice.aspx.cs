using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Carservice : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    string request_doctor = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsTP = null;

    string sf_Name = string.Empty;
    string Sf_HQ = string.Empty;
    string sf_Dsg_Sh = string.Empty;
    string Empcode = string.Empty;
    string date = string.Empty;
    string sl_no = string.Empty;
    string mode = string.Empty;
    DataSet dsDoc = new DataSet();


    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["Sf_Code"].ToString();
        sf_type = Session["sf_type"].ToString();

        Sf_HQ = Session["Sf_HQ"].ToString();
        sf_Dsg_Sh = Session["Designation_Short_Name"].ToString();
        sf_Name = Session["sf_name"].ToString();
 
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
       
        if (!Page.IsPostBack)
        {
            
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillCategory();
            
        }
       
       if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            if (!Page.IsPostBack)
            {

            }
        }
       else
       {
           mode = Request.QueryString["type"].ToString();

           if (mode == "1")
           {

               sl_no = Request.QueryString["Sl_No"].ToString();
              service_view();
              tbl.Visible = true;
              tbl1.Visible = true;
              tbl5.Visible = true;
               btnSubmit.Visible = false;
               tblfm.Visible = true;
           }
           else
           {
               tbl.Visible = false;
           }
       }
        
    }

    private void service_view()
    {
        div_code = div_code.TrimEnd(',');
        ListedDR lstDR = new ListedDR();
        dsDoc = lstDR.GetRequest_Service(sl_no, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            LabelADate.Visible = true;
            LabelATime.Visible = true;
            LabelCategory.Visible = true;
            Labeldays.Visible = true;
            LabelDDate.Visible = true;
            LabelDtime.Visible = true;
            Labelearlier.Visible = true;
            LabelHQ.Visible = true;
            Labellastmonth1.Visible = true;
            Labellastmonth2.Visible = true;
            Labellastmonth3.Visible = true;
            LabellastService.Visible = true;
            LabelMobileNo.Visible = true;
            LabelName.Visible = true;
            Labelnextmonth1.Visible = true;
            Labelnextmonth2.Visible = true;
            Labelnextmonth3.Visible = true;
            Labelpersons.Visible = true;
            LabelPic.Visible = true;
            LabelPlace.Visible = true;
            LabelSf_name.Visible = true;
            Labeltrainname.Visible = true;
            LabelName.Text = dsDoc.Tables[0].Rows[0]["Name_of_Doctor"].ToString();
            LabelADate.Text = dsDoc.Tables[0].Rows[0]["A_D"].ToString();
            LabelATime.Text = dsDoc.Tables[0].Rows[0]["A_T"].ToString();
            LabelCategory.Text = dsDoc.Tables[0].Rows[0]["category"].ToString();
            Labeldays.Text = dsDoc.Tables[0].Rows[0]["No_days"].ToString();
            LabelDDate.Text = dsDoc.Tables[0].Rows[0]["D_D"].ToString();
            LabelDtime.Text = dsDoc.Tables[0].Rows[0]["D_T"].ToString();
            Labelearlier.Text = dsDoc.Tables[0].Rows[0]["Earlier_C_S"].ToString();
            LabelHQ.Text = dsDoc.Tables[0].Rows[0]["From_HQ"].ToString();
            Labellastmonth1.Text = dsDoc.Tables[0].Rows[0]["B_I"].ToString();
            Labellastmonth2.Text = dsDoc.Tables[0].Rows[0]["B_II"].ToString();
            Labellastmonth3.Text = dsDoc.Tables[0].Rows[0]["B_III"].ToString();
            Labelnextmonth1.Text = dsDoc.Tables[0].Rows[0]["B_N_I"].ToString();
            Labelnextmonth2.Text = dsDoc.Tables[0].Rows[0]["B_N_II"].ToString();
            Labelnextmonth3.Text = dsDoc.Tables[0].Rows[0]["B_N_III"].ToString();
            LabellastService.Text = dsDoc.Tables[0].Rows[0]["L_C_S_M"].ToString();
            Labelpersons.Text = dsDoc.Tables[0].Rows[0]["N_P"].ToString();
            LabelPic.Text = dsDoc.Tables[0].Rows[0]["C_P"].ToString();
            LabelPlace.Text = dsDoc.Tables[0].Rows[0]["Place"].ToString();
            LabelSf_name.Text = dsDoc.Tables[0].Rows[0]["From_Sf_Name"].ToString();
            Labeltrainname.Text = dsDoc.Tables[0].Rows[0]["F_No_T_N"].ToString();
            LabelMobileNo.Text = dsDoc.Tables[0].Rows[0]["MobileNo"].ToString(); 
            txtADate.Visible = false;
            txtATime.Visible = false;
            txtdays.Visible = false;
            txtDDate.Visible = false;
            txtDtime.Visible = false;
            txtearlier.Visible = false;
            txtlastmonth1.Visible = false;
            txtlastmonth2.Visible = false;
            txtlastmonth3.Visible = false;
            txtlastService.Visible = false;
            txtMobileNo.Visible = false;
            txtName.Visible = false;
            txtnextmonth1.Visible = false;
            txtnextmonth2.Visible = false;
            txtnextmonth3.Visible = false;
            txtpersons.Visible = false;
            txtPic.Visible = false;
            txtPlace.Visible = false;
            txttrainname.Visible = false;
            drpCategory.Visible = false;
              
        }
    }


    private void FillCategory()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchCategory(sf_code);
        drpCategory.DataTextField = "Doc_Cat_SName";
        drpCategory.DataValueField = "Doc_Cat_Code";
        drpCategory.DataSource = dsListedDR;
        drpCategory.DataBind();
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        div_code = div_code.TrimEnd(',');

        DateTime now = DateTime.Now;

        string datenew = DateTime.Today.ToString("dd-MM-yyyy");
        DateTime dte;
        dte = Convert.ToDateTime(txtADate.Text);
        string ADate = dte.Day.ToString() + "-" + dte.Month.ToString() + "-" + dte.Year.ToString(); 
        DateTime dte1;

        dte1 = Convert.ToDateTime(txtDDate.Text);
        string DDate = dte1.Day.ToString() + "-" + dte1.Month.ToString() + "-" + dte1.Year.ToString(); 
        ListedDR lstDR = new ListedDR();
        int iReturn1 = lstDR.RecordInsert(div_code, sf_code, sf_Name, Sf_HQ, sf_Dsg_Sh, 
                                          txtName.Text, drpCategory.SelectedItem.ToString(), txtPlace.Text, txtMobileNo.Text,
                                          txtdays.Text, txtPic.Text, txtpersons.Text, ADate, txtATime.Text,
                                          DDate, txtDtime.Text, txttrainname.Text, txtlastmonth1.Text, txtlastmonth2.Text,
                                          txtlastmonth3.Text, txtnextmonth1.Text, txtnextmonth2.Text, txtnextmonth3.Text, txtearlier.Text,
                                           txtlastService.Text, datenew);
        if (iReturn1 > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
        }
    }

  
}
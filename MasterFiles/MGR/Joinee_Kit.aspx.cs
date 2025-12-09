using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_MGR_Joinee_Kit : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string sf_Hq = string.Empty;
    string sf_Desig = string.Empty;
    string sf_name = string.Empty;
    string div_code = string.Empty;
    string wrkbag = string.Empty;
    string sl_no = string.Empty;
    string mode = string.Empty;
    DataSet dsrep = new DataSet();
    int iReturn = -1;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["division_code"].ToString();
        sf_Hq = Session["Sf_HQ"].ToString();
        sf_Desig = Session["Designation_Short_Name"].ToString();
        sf_name = Session["sf_name"].ToString();


        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            
            div_code = Session["div_code"].ToString();

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
                joinee_kit();
                tbl.Visible = true;
                btnsave.Visible = false;
                pnlbutton.Visible = true;
                lblhead.Visible = true;
            }
            else
            {
                tbl.Visible = false;
                pnlbutton.Visible = false;
            }
        }
        
    }
    private void joinee_kit()
    {
        div_code = div_code.TrimEnd(',');
        Rep rr = new Rep();
        dsrep = rr.gettraineekitview(sl_no, div_code);
        

        if (dsrep.Tables[0].Rows.Count > 0)
        {
            lblnameNC.Visible = true;
            lbldesiNC.Visible = true;
            lblhqNC.Visible = true;
            lbldate.Visible = true;
            lblwrkbag.Visible = true;
            lblsamples.Visible = true;
            lblstationary.Visible = true;
            lblvisuall.Visible = true;
            lblglossary.Visible = true;
            lblreckoner.Visible = true;
            lbladdress.Visible = true;
            lblreplace.Visible = true;
            lblcompany.Visible = true;
            lblsubmi.Visible = true;
            lblnameNC.Text = dsrep.Tables[0].Rows[0]["N_C"].ToString();
            lbldesiNC.Text = dsrep.Tables[0].Rows[0]["N_C_D"].ToString();
            lblhqNC.Text = dsrep.Tables[0].Rows[0]["N_C_H"].ToString();
            lbldate.Text = dsrep.Tables[0].Rows[0]["DOJ"].ToString();
            lblwrkbag.Text = dsrep.Tables[0].Rows[0]["Work_Bag"].ToString();
            lblsamples.Text = dsrep.Tables[0].Rows[0]["Samples"].ToString();
            lblstationary.Text = dsrep.Tables[0].Rows[0]["Stationary"].ToString();
            lblvisuall.Text = dsrep.Tables[0].Rows[0]["Visual_Aid"].ToString();
            lblglossary.Text = dsrep.Tables[0].Rows[0]["Product_Glossary"].ToString();
            lblreckoner.Text = dsrep.Tables[0].Rows[0]["Ready_Reckoner"].ToString();
            lbladdress.Text = dsrep.Tables[0].Rows[0]["Add_sample_request"].ToString();
            lblreplace.Text = dsrep.Tables[0].Rows[0]["Replacement_nme"].ToString();
            lblcompany.Text = dsrep.Tables[0].Rows[0]["C_P_C"].ToString();
            lblsubmi.Text= dsrep.Tables[0].Rows[0]["Created_Date"].ToString();
           
          lblnameofmgr.Text = dsrep.Tables[0].Rows[0]["From_Sf_Name"].ToString();
            txtName.Visible = false;
            txtdesig.Visible = false;
            txthq.Visible = false;
            txtEffFrom.Visible = false;
            rdowrk.Visible = false;
            rdosample.Visible = false;
            rdostationary.Visible = false;
            rdovisualaid.Visible = false;
            rdoglossary.Visible = false;
            rdoreck.Visible = false;
            txtadd.Visible = false;
            txtreplaced.Visible = false;
            rdocompany.Visible = false;
           // txtbox.Visible = false;
           
        }

    } 
    
    protected void btnsave_click(object sender, EventArgs e)
    {
        div_code = div_code.TrimEnd(',');
        string name = txtName.Text;
        string desig = txtdesig.Text;
        string hq = txthq.Text;
        DateTime dte;
        dte = Convert.ToDateTime(txtEffFrom.Text);
        string Date_Of_Join = dte.Month.ToString() + "-" + dte.Day.ToString() + "-" + dte.Year.ToString();
        //DateTime subdt;
        //subdt = Convert.ToDateTime(txtbox.Text);
        //string sub_dte= subdt.Month.ToString() + "-" + subdt.Day.ToString() + "-" + subdt.Year.ToString();
        Rep rep = new Rep();

        iReturn = rep.AddJoinee(sf_code, sf_name, sf_Hq, sf_Desig,name, desig, hq, Date_Of_Join, rdowrk.SelectedItem.Text, rdosample.SelectedItem.Text, rdostationary.SelectedItem.Text, rdovisualaid.SelectedItem.Text,
                               rdoglossary.SelectedItem.Text, rdoreck.SelectedItem.Text, txtadd.Text, txtreplaced.Text, rdocompany.SelectedItem.Text, div_code);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='Joinee_Kit.aspx'</script>");
        }
        clear();
    }
    private void clear()
    {
        txtName.Text = "";
        txtdesig.Text = "";
        txthq.Text = "";
        txtEffFrom.Text = "";
        rdowrk.SelectedIndex = -1;
        rdosample.SelectedIndex = -1;
        rdostationary.SelectedIndex = -1;
        rdovisualaid.SelectedIndex = -1;
        rdoglossary.SelectedIndex = -1;
        rdoreck.SelectedIndex = -1;
        txtadd.Text = "";
        txtreplaced.Text = "";
        rdocompany.SelectedIndex = -1;
    }
   
}
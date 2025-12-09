using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.Sql;
using DBase_EReport;

public partial class MasterFiles_MGR_Recommendation_For_Confirmation : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_Hq = string.Empty;
    string sf_name = string.Empty;
    string sf_Desig = string.Empty;
    DataSet dsrep = new DataSet();
    DB_EReporting db_ER = new DB_EReporting();
    string strQry = string.Empty;
    int iReturn = -1;
    string chkdetailgud = string.Empty;
    string chkdetailavg = string.Empty;
    string chkdetailpoor = string.Empty;
    string chkchambergud = string.Empty;
    string chkchamberavg = string.Empty;
    string chkchamberpoor = string.Empty;
    string chkwrkgud = string.Empty;
    string chkwrkavg = string.Empty;
    string chkwrkpoor = string.Empty;
    string chkreportgud = string.Empty;
    string chkreportavg = string.Empty;
    string chkreportpoor = string.Empty;
    string mode = string.Empty;
    string sl_no = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["division_code"].ToString();
        sf_Hq = Session["Sf_HQ"].ToString();
        sf_name = Session["sf_name"].ToString();
        sf_Desig = Session["Designation_Short_Name"].ToString();
       

        if (Session["sf_type"].ToString() == "2")
        {
            div_code = Session["div_code"].ToString();

            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
       

            if (!Page.IsPostBack)
            {
              
                FieldForce_List();
                Fielddetail();
               
            }

        }
        else
        {
            mode = Request.QueryString["type"].ToString();

            if (mode == "1")
            {

                sl_no = Request.QueryString["Sl_No"].ToString();
                Recommend();
                tbl.Visible = true;
                btnsave.Visible = false;
                lblhead.Visible = true;
                pnlbutton.Visible = true;
            }
            else
            {
                tbl.Visible = false;
                pnlbutton.Visible = false;
            }
        }
    }

    private void Recommend()
    {
        div_code = div_code.TrimEnd(',');
        Rep rr = new Rep();
        
        dsrep = rr.getRcmdview(sl_no, div_code);
        if (dsrep.Tables[0].Rows.Count > 0)
        {
            ddlfieldforce.Visible = false;
            txtsalary.Visible = false;
            chkgudD.Visible = false;
            chkavgD.Visible = false;
            chkpoorD.Visible = false;
            txtcommentD.Visible = false;
            chkgudC.Visible = false;
            chkavgC.Visible = false;
            chkpoorC.Visible = false;
            txtcommentC.Visible = false;
            chkgudW.Visible = false;
            chkavgW.Visible = false;
            chkpoorW.Visible = false;
            txtcommentW.Visible = false;
            chkgudR.Visible = false;
            chkavgR.Visible = false;
            chkpoorR.Visible = false;
            txtcommentR.Visible = false;
            txtexttar.Visible = false;
            txtextfmnth.Visible = false;
            txtextsmnth.Visible = false;
            txtextthidmnth.Visible = false;
            txtextfrthmnth.Visible = false;
            txtextfivthmnth.Visible = false;
            txtextsixmnth.Visible = false;
            txtacvetar.Visible = false;
            txtacveext.Visible = false;
            txtacvefmnth.Visible = false;
            txtacvesmnth.Visible = false;
            txtacvethrdmnht.Visible = false;
            txtacvefthmnth.Visible = false;
            txtacvefvthmnth.Visible = false;
            txtachevsecext.Visible = false;
            txtachevsecfmnth.Visible = false;
            txtachevsecsmnth.Visible = false;
            txtachevsecthdmnth.Visible = false;
            txtachevsecfrthmnth.Visible = false;
            txtachevsecfivthmnth.Visible = false;
            txtachevsecsixmnth.Visible = false;
            txtperfomfmnth.Visible = false;
            txtperfomsmnth.Visible = false;
            txtperfomthdmnth.Visible = false;
            txtperfomforthmnth.Visible = false;
            txtperfomfivthmnth.Visible = false;
            txtperfomsixthmnth.Visible = false;
            txtcallfmnth.Visible = false;
            txtcallSmnth.Visible = false;
            txtcallthrdmnth.Visible = false;
            txtmarcov.Visible = false;
            txtmarcovsec.Visible = false;
            txtmarcovthrd.Visible = false;
            txtEffFrom.Visible = false;
            lblFieldname.Text = dsrep.Tables[0].Rows[0]["recmd_name"].ToString();
            lbljoindte.Text = dsrep.Tables[0].Rows[0]["recmd_doj"].ToString();
            lblehq.Text = dsrep.Tables[0].Rows[0]["recmd_Hq"].ToString();
            labeladm.Text = dsrep.Tables[0].Rows[0]["recmd_rptname"].ToString();
            lblDgud.Text = dsrep.Tables[0].Rows[0]["Detailing_Gud"].ToString();
            lblDa.Text = dsrep.Tables[0].Rows[0]["Detailing_Avg"].ToString();
            lblDp.Text = dsrep.Tables[0].Rows[0]["Detailing_Poor"].ToString();
            lblDcomm.Text = dsrep.Tables[0].Rows[0]["Detailing_Comm_FB"].ToString();
            lblCgud.Text = dsrep.Tables[0].Rows[0]["Inchamber_Gud"].ToString();
            lblCavg.Text=dsrep.Tables[0].Rows[0]["Inchamber_Avg"].ToString();
            lblCpoor.Text=dsrep.Tables[0].Rows[0]["Inchamber_Poor"].ToString();
            lblCcomment.Text=dsrep.Tables[0].Rows[0]["Inchamber_Comm_FB"].ToString();
            lblWgud.Text=dsrep.Tables[0].Rows[0]["Work_Punct_Gud"].ToString();
            lblWAvg.Text=dsrep.Tables[0].Rows[0]["Work_Punct_Avg"].ToString();
            lblWpoor.Text=dsrep.Tables[0].Rows[0]["Work_Punct_Poor"].ToString();
            lblWcomment.Text=dsrep.Tables[0].Rows[0]["Work_Punct_Comm_FB"].ToString();
            lblRgud.Text = dsrep.Tables[0].Rows[0]["Report_Punct_Gud"].ToString();
            lblRAvg.Text = dsrep.Tables[0].Rows[0]["Report_Punct_Avg"].ToString();
            lblRPoor.Text = dsrep.Tables[0].Rows[0]["Report_Punct_poor"].ToString();
            lblRcomment.Text = dsrep.Tables[0].Rows[0]["Report_Punct_Commen_FB"].ToString();
            lblexttar.Text = dsrep.Tables[0].Rows[0]["Perf_Exist_Target"].ToString();
            lblextfmnth.Text = dsrep.Tables[0].Rows[0]["Perf_Fmnth_Target"].ToString();
            lblextsmnth.Text = dsrep.Tables[0].Rows[0]["Perf_Smnth_Target"].ToString();
            lblextthdmnth.Text = dsrep.Tables[0].Rows[0]["Perf_Tmnth_Target"].ToString();
            lblextfrthmnth.Text = dsrep.Tables[0].Rows[0]["Perf_Frthmnth_Target"].ToString();
            lblextfivmnth.Text = dsrep.Tables[0].Rows[0]["Perf_Fivthmnth_Target"].ToString();
            lblextsixmnth.Text = dsrep.Tables[0].Rows[0]["Perf_sixmnth_Target"].ToString();
            lblachvetar.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Fmnth"].ToString();
            lblachveext.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Smnth"].ToString();
            lblachvefmnth.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Tmnth"].ToString();
            lblachvesmnth.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Frthmnth"].ToString();
            lblachvethdmnth.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Fivthmnth"].ToString();
            lblachvefrthmnth.Text = dsrep.Tables[0].Rows[0]["Per_Achve_sixthmnth"].ToString();
            lblachvefvthmnth.Text = dsrep.Tables[0].Rows[0]["Per_Achve_ext"].ToString();
            lblachsecext.Text = dsrep.Tables[0].Rows[0]["Per_Achve_ext_sec"].ToString();
            lblachsecone.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Fmnth_sec"].ToString();
            lblachsectwo.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Smnth_sec"].ToString();
            lblachsecthree.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Tmnth_sec"].ToString();
            lblachsecfour.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Frthmnth_sec"].ToString();
            lblachsecfive.Text = dsrep.Tables[0].Rows[0]["Per_Achve_Fivthmnth_sec"].ToString();
            lblachsecsix.Text = dsrep.Tables[0].Rows[0]["Per_Achve_sixthmnth_sec"].ToString();
            lblperfmnth.Text = dsrep.Tables[0].Rows[0]["Performance_Fmnth"].ToString();
            lblpersec.Text = dsrep.Tables[0].Rows[0]["Performance_Smnth"].ToString();
            lblperthree.Text = dsrep.Tables[0].Rows[0]["Performance_Tmnth"].ToString();
            lblperfour.Text = dsrep.Tables[0].Rows[0]["Performance_Frthmnth"].ToString();
            lblperfive.Text = dsrep.Tables[0].Rows[0]["Performance_Fivthmnth"].ToString();
            lblpersix.Text = dsrep.Tables[0].Rows[0]["Performance_Sxthmnth"].ToString();
            lblcallfirst.Text = dsrep.Tables[0].Rows[0]["Last_Three_Callavg_F"].ToString();
            lblcallsecond.Text = dsrep.Tables[0].Rows[0]["Last_Three_Callavg_S"].ToString();
            lblcallthird.Text = dsrep.Tables[0].Rows[0]["Last_Three_Callavg_T"].ToString();
            lblmarfirst.Text = dsrep.Tables[0].Rows[0]["Last_Three_MarCov_F"].ToString();
            lblmarsecond.Text = dsrep.Tables[0].Rows[0]["Last_Three_MarCov_S"].ToString();
            lblthird.Text = dsrep.Tables[0].Rows[0]["Last_Three_MarCov_T"].ToString();
            lblcondte.Text= dsrep.Tables[0].Rows[0]["Confirm_Date"].ToString();
            lblnameofmgr.Text= dsrep.Tables[0].Rows[0]["Sf_name"].ToString();
            lblsubdte.Text = dsrep.Tables[0].Rows[0]["created_date"].ToString();
            lblFieldname.Visible = true;
            lbljoindte.Visible = true;
            lblehq.Visible = true;
            labeladm.Visible = true;
            lblDgud.Visible = true;
            lblDa.Visible = true;
            lblDp.Visible = true;
            lblDcomm.Visible = true;
            lblCgud.Visible = true;
            lblCavg.Visible = true;
            lblCpoor.Visible = true;
            lblCcomment.Visible = true;
            lblWgud.Visible = true;
            lblWAvg.Visible = true;
            lblWpoor.Visible = true;
            lblWcomment.Visible = true;
            lblRAvg.Visible = true;
            lblRPoor.Visible = true;
            lblRcomment.Visible = true;
            lblexttar.Visible = true;
            lblextfmnth.Visible = true;
            lblextsmnth.Visible = true;
            lblextthdmnth.Visible = true;
            lblextfrthmnth.Visible = true;
            lblextfivmnth.Visible = true;
            lblextsixmnth.Visible = true;
            lblachvetar.Visible = true;
            lblachveext.Visible = true;
            lblachvefmnth.Visible = true;
            lblachvesmnth.Visible = true;
            lblachvethdmnth.Visible = true;
            lblachvefrthmnth.Visible = true;
            lblachvefvthmnth.Visible = true;
            lblachsecext.Visible = true;
            lblachsecone.Visible = true;
            lblachsectwo.Visible = true;
            lblachsecthree.Visible = true;
            lblachsecfour.Visible = true;
            lblachsecfive.Visible = true;
            lblachsecsix.Visible = true;
            lblperfmnth.Visible = true;
            lblpersec.Visible = true;
            lblperthree.Visible = true;
            lblperfour.Visible = true;
            lblperfive.Visible = true;
            lblpersix.Visible = true;
            lblcallfirst.Visible = true;
           lblcallsecond.Visible = true;
          lblcallthird.Visible = true;
          lblmarfirst.Visible = true;
          lblmarsecond.Visible = true;
          lblthird.Visible = true;
            lblcondte.Visible = true;


        }
    }
    
    private void FieldForce_List()
    {
        Rep rr_hier = new Rep();
        dsrep = rr_hier.getSFCode(div_code,sf_code);

        if (dsrep.Tables[0].Rows.Count > 0)
        {
            ddlfieldforce.DataTextField = "Sf_Name";
            ddlfieldforce.DataValueField = "Sf_Code";
            ddlfieldforce.DataSource = dsrep;
            ddlfieldforce.DataBind();
        }
    }
    private void Fielddetail()
    {
        string rpt = string.Empty;
        Rep rr_hier = new Rep();
        dsrep = rr_hier.getfieldforce(div_code, ddlfieldforce.SelectedValue);
        if (dsrep.Tables[0].Rows.Count > 0)
        {
            lbljoindte.Text = dsrep.Tables[0].Rows[0]["Sf_Joining_Date"].ToString();
            lblehq.Text = dsrep.Tables[0].Rows[0]["Sf_HQ"].ToString();
            rpt = dsrep.Tables[0].Rows[0]["Reporting_To_SF"].ToString();

            DataSet dsvisit = new DataSet();
            strQry = "select Sf_Name from mas_salesforce where sf_code='" + rpt + "'";
            dsvisit = db_ER.Exec_DataSet(strQry);

            labeladm.Text = dsvisit.Tables[0].Rows[0]["Sf_Name"].ToString();

        }

    }
    protected void ddlfield_selectindexchanged(object sender, EventArgs e)
    {
        Fielddetail();
    }

    protected void btnsave_click(object sender, EventArgs e)
    {
        div_code = div_code.TrimEnd(',');

        
        if(chkgudD.Checked)
        {
            chkdetailgud="Yes";
            chkdetailavg="";
            chkdetailpoor="";

        }
        else if(chkavgD.Checked)
        {
            chkdetailgud="";
            chkdetailavg="Yes";
            chkdetailpoor="";
        }
         else if(chkpoorD.Checked)
        {
            chkdetailgud="";
              chkdetailavg="";
            chkdetailpoor="Yes";
        }
        else
        {
            chkdetailgud="";
            chkdetailavg="";
            chkdetailpoor="";
        }
        if(chkgudC.Checked)
        {
            chkchambergud="Yes";
            chkchamberavg="";
            chkchamberpoor="";
        }
        else if(chkavgC.Checked)
        {
            chkchambergud="";
            chkchamberavg="Yes";
            chkchamberpoor="";
        }
        else if(chkpoorC.Checked)
        {
            chkchambergud="";
            chkchamberavg="";
            chkchamberpoor="Yes";
        }
         if(chkgudC.Checked)
        {
            chkchambergud="Yes";
            chkchamberavg="";
            chkchamberpoor="";
        }
        else if(chkavgC.Checked)
        {
            chkchambergud="";
            chkchamberavg="Yes";
            chkchamberpoor="";
        }
        else if(chkpoorC.Checked)
        {
            chkchambergud="";
            chkchamberavg="Yes";
            chkchamberpoor="Yes";
        }
         if(chkgudW.Checked)
        {
            chkchambergud="Yes";
            chkchamberavg="";
            chkchamberpoor="";
        }
        else if(chkavgW.Checked)
        {
            chkchambergud="";
            chkchamberavg="Yes";
            chkchamberpoor="";
        }
        else if(chkpoorW.Checked)
        {
            chkwrkgud="";
            chkwrkavg="";
            chkwrkpoor="Yes";
        }
         if(chkgudR.Checked)
        {
            chkreportgud="Yes";
            chkreportavg="";
            chkreportpoor="";
        }
        else if(chkavgR.Checked)
        {
            chkreportgud="";
            chkreportavg="Yes";
            chkreportpoor="";
        }
        else if(chkpoorR.Checked)
        {
            chkreportgud="";
            chkreportavg="";
            chkreportpoor="Yes";
        }
          

        Rep rep = new Rep();

        DateTime dte;
        dte = Convert.ToDateTime(txtEffFrom.Text);
        string confirm_dte = dte.Month.ToString() + "-" + dte.Day.ToString() + "-" + dte.Year.ToString();

        iReturn = rep.AddRecommendation(sf_code, sf_name, sf_Hq, sf_Desig,labeladm.Text, ddlfieldforce.SelectedItem.Text, ddlfieldforce.SelectedValue, lbljoindte.Text, lblehq.Text, labeladm.Text,
            txtsalary.Text, chkdetailgud, chkdetailavg, chkdetailpoor, txtcommentD.Text, chkchambergud, chkchamberavg, chkchamberpoor, txtcommentC.Text, chkwrkgud, chkwrkavg, chkwrkpoor, txtcommentW.Text,
            chkreportgud, chkreportavg, chkreportpoor, txtcommentR.Text, txtexttar.Text, txtextfmnth.Text, txtextsmnth.Text, txtextthidmnth.Text, txtextfrthmnth.Text, txtextfivthmnth.Text, txtextsixmnth.Text,
            txtacvetar.Text,txtacveext.Text, txtacvefmnth.Text, txtacvesmnth.Text, txtacvethrdmnht.Text, txtacvefthmnth.Text, txtacvefvthmnth.Text, txtachevsecext.Text, txtachevsecfmnth.Text, txtachevsecsmnth.Text,
            txtachevsecthdmnth.Text, txtachevsecfrthmnth.Text, txtachevsecfivthmnth.Text, txtachevsecsixmnth.Text, txtperfomfmnth.Text, txtperfomsmnth.Text, txtperfomthdmnth.Text, txtperfomforthmnth.Text,
            txtperfomfivthmnth.Text, txtperfomsixthmnth.Text, txtcallfmnth.Text, txtcallSmnth.Text, txtcallthrdmnth.Text, txtmarcov.Text, txtmarcovsec.Text, txtmarcovthrd.Text,div_code, confirm_dte);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='Recommendation_For_Confirmation.aspx'</script>");
        }
       
    }
}
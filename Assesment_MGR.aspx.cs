using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Assesment_MGR : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sl_no = string.Empty;
    string mode = string.Empty;
    string strMultiDiv = string.Empty;
    string request_doctor = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sf_Hq = string.Empty;
    string sf_Desig = string.Empty;
    string sf_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        sf_code = Session["sf_code"].ToString();
        div_code = Session["Division_Code"].ToString();

        sf_Hq = Session["Sf_HQ"].ToString();
        sf_Desig = Session["Designation_Short_Name"].ToString();
        sf_name = Session["sf_name"].ToString();
       // if (!Page.IsPostBack)
        {
            getParametes();
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
            else
            {
                mode = Request.QueryString["mode"].ToString();
                if (mode == "1")
                {
                    sl_no = Request.QueryString["Sl_No"].ToString();
                    tbl.Visible = true;
                 //   tblBtn.Visible = true;
                    LblVisible();
                    btnSubmit.Visible = false;
                    lblhead.Visible = true;
                    tblclose.Visible = true;
                }
                else
                {
                    tbl.Visible = false; 
                }
            }
        }
    }

    private void LblVisible()
    {
        div_code = div_code.TrimEnd(',');
        SalesForce rr = new SalesForce();
        dsListedDR = rr.getAsses_MGRview(sl_no, div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            txtName.Visible = false;
            txtPost.Visible = false;
            TxtQual.Visible = false;
            txtExp.Visible = false;
            txtage.Visible = false;
            txtmarital.Visible = false;
            ddltwowheel.Visible = false;
            txtuan.Visible = false;
            txtHq.Visible = false;
            txtMbl.Visible = false;
            txtEMail.Visible = false;
            txtSale_Pre.Visible = false;
            txtPerfo.Visible = false;
            txtCur_Sale.Visible = false;
            txtReason.Visible = false;
            txtL_Salary.Visible = false;
            txtmin_sal.Visible = false;
            txtJ_Date.Visible = false;
            txtIndu.Visible = false;
            txtEffFrom.Visible = false;
            txtToDate.Visible = false;
            txtCover.Visible = false;

            txtNamelbl.Text = dsListedDR.Tables[0].Rows[0]["Name"].ToString();
            txtPostlbl.Text = dsListedDR.Tables[0].Rows[0]["Post"].ToString();
            TxtQuallbl.Text = dsListedDR.Tables[0].Rows[0]["Qual"].ToString();
            txtExplbl.Text = dsListedDR.Tables[0].Rows[0]["Pharma_Exp"].ToString();
            txtagelbl.Text = dsListedDR.Tables[0].Rows[0]["Age"].ToString();
            txtmaritallbl.Text = dsListedDR.Tables[0].Rows[0]["Marital_Status"].ToString();
            ddltwowheellbl.Text = dsListedDR.Tables[0].Rows[0]["Two_Wheeler"].ToString();
            txtuanlbl.Text = dsListedDR.Tables[0].Rows[0]["UAN_no"].ToString();
            txtHqlbl.Text = dsListedDR.Tables[0].Rows[0]["Hq"].ToString();
            txtMbllbl.Text = dsListedDR.Tables[0].Rows[0]["Mob_No"].ToString();

            txtEMaillbl.Text = dsListedDR.Tables[0].Rows[0]["Email_Pre_Comp"].ToString();
            txtSale_Prelbl.Text = dsListedDR.Tables[0].Rows[0]["Sale_Pre_Comp"].ToString();
            txtPerfolbl.Text = dsListedDR.Tables[0].Rows[0]["Perform_Percentage"].ToString();

            txtCur_Salelbl.Text = dsListedDR.Tables[0].Rows[0]["Cur_Avg_Sale"].ToString();
            txtReasonlbl.Text = dsListedDR.Tables[0].Rows[0]["Change_Reason"].ToString();
            txtL_Salarylbl.Text = dsListedDR.Tables[0].Rows[0]["L_Salary"].ToString();
            txtmin_sallbl.Text = dsListedDR.Tables[0].Rows[0]["Min_Salary_Exp"].ToString();
            txtJ_Datelbl.Text = dsListedDR.Tables[0].Rows[0]["join_Date"].ToString();
            txtIndulbl.Text = dsListedDR.Tables[0].Rows[0]["Induction_By"].ToString();
            txtEffFromlbl.Text = dsListedDR.Tables[0].Rows[0]["Ind_Frm_Date"].ToString();
            txtToDatelbl.Text = dsListedDR.Tables[0].Rows[0]["Ind_To_Date"].ToString();
            txtCover.Text = dsListedDR.Tables[0].Rows[0]["Cover_Areas"].ToString();

            lblSubmitDate.Text = "&nbsp;&nbsp;&nbsp;" + dsListedDR.Tables[0].Rows[0]["Created_Date"].ToString();
            lblSenderName.Text = "&nbsp;&nbsp;&nbsp;" + dsListedDR.Tables[0].Rows[0]["Frm_Sf_Name"].ToString();

            string Personality = string.Empty;
            string Dress_Sense = string.Empty;
            string Spok_Eng = string.Empty;
            string Job_Knowl = string.Empty;
            string C_Growth = string.Empty;
            string U_Earn = string.Empty;
            int param = 1;

            Personality = dsListedDR.Tables[0].Rows[0]["Personality"].ToString();
            Dress_Sense = dsListedDR.Tables[0].Rows[0]["Dress_Sense"].ToString();
            Spok_Eng = dsListedDR.Tables[0].Rows[0]["Spok_Eng"].ToString();
            Job_Knowl = dsListedDR.Tables[0].Rows[0]["Job_Knowl"].ToString();
            C_Growth = dsListedDR.Tables[0].Rows[0]["C_Growth"].ToString();
            U_Earn = dsListedDR.Tables[0].Rows[0]["U_Earn"].ToString();

            foreach (GridViewRow gridRow in grdDCR.Rows)
            {

                ((CheckBox)grdDCR.Rows[param - 1].FindControl("chkPoor")).Visible = false;
                ((Label)grdDCR.Rows[param - 1].FindControl("chkPoorlbl")).Visible = true;

                ((CheckBox)grdDCR.Rows[param - 1].FindControl("chkAVERAGE")).Visible = false;
                ((Label)grdDCR.Rows[param - 1].FindControl("chkAVERAGElbl")).Visible = true;

                ((CheckBox)grdDCR.Rows[param - 1].FindControl("chkgood")).Visible = false;
                ((Label)grdDCR.Rows[param - 1].FindControl("chkgoodlbl")).Visible = true;

                Label chkPoorlbl = (Label)gridRow.Cells[2].FindControl("chkPoorlbl");
                Label chkAverlbl = (Label)gridRow.Cells[2].FindControl("chkAVERAGElbl");
                Label chkGoodlbl = (Label)gridRow.Cells[2].FindControl("chkgoodlbl");

                CheckBox chkPoor = (CheckBox)gridRow.Cells[2].FindControl("chkPoor");
                bool bCheckPoor = chkPoor.Checked;

                CheckBox chkAver = (CheckBox)gridRow.Cells[3].FindControl("chkAVERAGE");
                bool bCheckAver = chkAver.Checked;

                CheckBox chkGood = (CheckBox)gridRow.Cells[4].FindControl("chkgood");
                bool bCheckGood = chkGood.Checked;

                if (param == 1)
                {
                    if (Personality == "P")
                    {
                        chkPoor.Checked = true;
                        chkPoorlbl.Text = "✔";
                    }
                    else if (Personality == "A")
                    {
                        chkAver.Checked = true;
                        chkAverlbl.Text = "✔";
                    }
                    else if (Personality == "G")
                    {
                        chkGood.Checked = true;
                        chkGoodlbl.Text = "✔";
                    }
                }

                if (param == 2)
                {
                    if (Dress_Sense == "P")
                    {
                        chkPoor.Checked = true;
                        chkPoorlbl.Text = "✔";
                    }
                    else if (Dress_Sense == "A")
                    {
                        chkAver.Checked = true;
                        chkAverlbl.Text = "✔";
                    }
                    else if (Dress_Sense == "G")
                    {
                        chkGood.Checked = true;
                        chkGoodlbl.Text = "✔";
                    }
                }

                if (param == 3)
                {
                    if (Spok_Eng == "P")
                    {
                        chkPoor.Checked = true;
                        chkPoorlbl.Text = "✔";
                    }
                    else if (Spok_Eng == "A")
                    {
                        chkAver.Checked = true;
                        chkAverlbl.Text = "✔";
                    }
                    else if (Spok_Eng == "G")
                    {
                        chkGood.Checked = true;
                        chkGoodlbl.Text = "✔";
                    }
                }

                if (param == 4)
                {
                    if (Job_Knowl == "P")
                    {
                        chkPoor.Checked = true;
                        chkPoorlbl.Text = "✔";
                    }
                    else if (Job_Knowl == "A")
                    {
                        chkAver.Checked = true;
                        chkAverlbl.Text = "✔";
                    }
                    else if (Job_Knowl == "G")
                    {
                        chkGood.Checked = true;
                        chkGoodlbl.Text = "✔";
                    }
                }

                if (param == 5)
                {
                    if (C_Growth == "P")
                    {
                        chkPoor.Checked = true;
                        chkPoorlbl.Text = "✔";
                    }
                    else if (C_Growth == "A")
                    {
                        chkAver.Checked = true;
                        chkAverlbl.Text = "✔";
                    }
                    else if (C_Growth == "G")
                    {
                        chkGood.Checked = true;
                        chkGoodlbl.Text = "✔";
                    }
                }

                if (param == 6)
                {
                    if (U_Earn == "P")
                    {
                        chkPoor.Checked = true;
                        chkPoorlbl.Text = "✔";
                    }
                    else if (U_Earn == "A")
                    {
                        chkAver.Checked = true;
                        chkAverlbl.Text = "✔";
                    }
                    else if (U_Earn == "G")
                    {
                        chkGood.Checked = true;
                        chkGoodlbl.Text = "✔";
                    }
                }


                param = param + 1;
            }

            param = 1;
            string Sale_month1 = string.Empty;
            string Sale_month2 = string.Empty;
            string Sale_month3 = string.Empty;
            string Sale_month4 = string.Empty;
            string Sale_month5 = string.Empty;
            string Sale_month6 = string.Empty;

            Sale_month1 = dsListedDR.Tables[0].Rows[0]["Sale_month1"].ToString();
            Sale_month2 = dsListedDR.Tables[0].Rows[0]["Sale_month2"].ToString();
            Sale_month3 = dsListedDR.Tables[0].Rows[0]["Sale_month3"].ToString();
            Sale_month4 = dsListedDR.Tables[0].Rows[0]["Sale_month4"].ToString();
            Sale_month5 = dsListedDR.Tables[0].Rows[0]["Sale_month5"].ToString();
            Sale_month6 = dsListedDR.Tables[0].Rows[0]["Sale_month6"].ToString();

            foreach (GridViewRow gridRow in GrdSale.Rows)
            {

                ((TextBox)GrdSale.Rows[param - 1].FindControl("txtmonth1")).Visible = false;
                ((Label)GrdSale.Rows[param - 1].FindControl("txtmonth1lbl")).Visible = true;

                ((TextBox)GrdSale.Rows[param - 1].FindControl("txtmonth2")).Visible = false;
                ((Label)GrdSale.Rows[param - 1].FindControl("txtmonth2lbl")).Visible = true;

                ((TextBox)GrdSale.Rows[param - 1].FindControl("txtmonth3")).Visible = false;
                ((Label)GrdSale.Rows[param - 1].FindControl("txtmonth3lbl")).Visible = true;

                ((TextBox)GrdSale.Rows[param - 1].FindControl("txtmonth4")).Visible = false;
                ((Label)GrdSale.Rows[param - 1].FindControl("txtmonth4lbl")).Visible = true;

                ((TextBox)GrdSale.Rows[param - 1].FindControl("txtmonth5")).Visible = false;
                ((Label)GrdSale.Rows[param - 1].FindControl("txtmonth5lbl")).Visible = true;

                ((TextBox)GrdSale.Rows[param - 1].FindControl("txtmonth6")).Visible = false;
                ((Label)GrdSale.Rows[param - 1].FindControl("txtmonth6lbl")).Visible = true;


                Label month1 = (Label)gridRow.Cells[1].FindControl("txtmonth1lbl");
                Label month2 = (Label)gridRow.Cells[2].FindControl("txtmonth2lbl");
                Label month3 = (Label)gridRow.Cells[3].FindControl("txtmonth3lbl");
                Label month4 = (Label)gridRow.Cells[4].FindControl("txtmonth4lbl");
                Label month5 = (Label)gridRow.Cells[5].FindControl("txtmonth5lbl");
                Label month6 = (Label)gridRow.Cells[6].FindControl("txtmonth6lbl");

                month1.Text = Sale_month1;
                month2.Text = Sale_month2;
                month3.Text = Sale_month3;
                month4.Text = Sale_month4;
                month5.Text = Sale_month5;
                month6.Text = Sale_month6;
            }


            txtNamelbl.Visible = true;
            txtPostlbl.Visible = true;
            TxtQuallbl.Visible = true;
            txtExplbl.Visible = true;
            txtagelbl.Visible = true;
            txtmaritallbl.Visible = true;
            ddltwowheellbl.Visible = true;
            txtuanlbl.Visible = true;
            txtHqlbl.Visible = true;
            txtMbllbl.Visible = true;
            txtEMaillbl.Visible = true;
            txtSale_Prelbl.Visible = true;
            txtPerfolbl.Visible = true;
            txtCur_Salelbl.Visible = true;
            txtReasonlbl.Visible = true;
            txtL_Salarylbl.Visible = true;
            txtmin_sallbl.Visible = true;
            txtJ_Datelbl.Visible = true;
            txtIndulbl.Visible = true;
            txtEffFromlbl.Visible = true;
            txtToDatelbl.Visible = true;
            txtCoverlbl.Visible = true;
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

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }




    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        // FillUserList();
    }

    private void getParametes()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getMR_Performance();

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdDCR.DataSource = dsSalesForce.Tables[0];
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsSalesForce.Tables[0];
            grdDCR.DataBind();
        }
        
        if (dsSalesForce.Tables[2].Rows.Count > 0)
        {
            GrdSale.DataSource = dsSalesForce.Tables[2];
            GrdSale.DataBind();
        }
        else
        {
            GrdSale.DataSource = dsSalesForce.Tables[2];
            GrdSale.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        string Chk = string.Empty;

        string Personality = string.Empty;
        string Dress_Sense = string.Empty;
        string Spok_Eng = string.Empty;
        string Job_Knowl = string.Empty;
        string C_Growth = string.Empty;
        string U_Earn = string.Empty;

        int param = 1;   //Trans_Assesment_MR

        foreach (GridViewRow gridRow in grdDCR.Rows)
        {
            CheckBox chkPoor = (CheckBox)gridRow.Cells[2].FindControl("chkPoor");
            bool bCheckPoor = chkPoor.Checked;

            CheckBox chkAver = (CheckBox)gridRow.Cells[3].FindControl("chkAVERAGE");
            bool bCheckAver = chkAver.Checked;
            CheckBox chkGood = (CheckBox)gridRow.Cells[4].FindControl("chkgood");
            bool bCheckGood = chkGood.Checked;

            if ((bCheckPoor == true))
            {
                Chk = "P";
            }
            else if ((bCheckAver == true))
            {
                Chk = "A";
            }
            else if ((bCheckGood == true))
            {
                Chk = "G";
            }
            if (param == 1)
            {
                Personality = Chk;
            }
            else if (param == 2)
            {
                Dress_Sense = Chk;
            }
            else if (param == 3)
            {
                Spok_Eng = Chk;
            }
            else if (param == 4)
            {
                Job_Knowl = Chk;
            }
            else if (param == 5)
            {
                C_Growth = Chk;
            }
            else if (param == 6)
            {
                U_Earn = Chk;
            }
            param = param + 1;
        }

        string Sale_month1 = string.Empty;
        string Sale_month2 = string.Empty;
        string Sale_month3 = string.Empty;
        string Sale_month4 = string.Empty;
        string Sale_month5 = string.Empty;
        string Sale_month6 = string.Empty;

        foreach (GridViewRow gridRow in GrdSale.Rows)
        {
            TextBox month1 = (TextBox)gridRow.Cells[1].FindControl("txtmonth1");
            TextBox month2 = (TextBox)gridRow.Cells[2].FindControl("txtmonth2");
            TextBox month3 = (TextBox)gridRow.Cells[3].FindControl("txtmonth3");
            TextBox month4 = (TextBox)gridRow.Cells[4].FindControl("txtmonth4");
            TextBox month5 = (TextBox)gridRow.Cells[5].FindControl("txtmonth5");
            TextBox month6 = (TextBox)gridRow.Cells[6].FindControl("txtmonth6");

            Sale_month1 = month1.Text;
            Sale_month2 = month2.Text;
            Sale_month3 = month3.Text;
            Sale_month4 = month4.Text;
            Sale_month5 = month5.Text;
            Sale_month6 = month6.Text;
        }

        int output = -1;
        //if (txtTarget.Text != "" && txtAchi.Text != "")
        {
            output = sf.Trans_Assesment_MGR(sf_code, div_code, sf_Hq, sf_Desig, sf_name, txtName.Text, txtPost.Text, TxtQual.Text, txtExp.Text, txtage.Text, txtmarital.Text, ddltwowheel.SelectedValue, txtuan.Text, txtHq.Text, txtMbl.Text,
                txtEMail.Text, txtSale_Pre.Text, txtPerfo.Text, txtCur_Sale.Text, txtReason.Text, txtL_Salary.Text, txtmin_sal.Text, txtJ_Date.Text, txtIndu.Text, txtEffFrom.Text, txtToDate.Text,
                Personality, Dress_Sense, Spok_Eng, Job_Knowl, C_Growth, U_Earn, Sale_month1, Sale_month2, Sale_month3, Sale_month4, Sale_month5, Sale_month6, txtCover.Text);
        }

        if (output > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='Assesment_MGR.aspx'</script>");
            clear();
        }
    }

    private void clear()
    {
        txtName.Text = "";

        txtPost.Text = "";
        TxtQual.Text = "";
        txtExp.Text = "";
        txtage.Text = "";
        txtmarital.Text = "";

        txtuan.Text = "";
        txtHq.Text = "";
        txtMbl.Text = "";
        txtEMail.Text = "";
        txtSale_Pre.Text = "";
        txtPerfo.Text = "";
        txtCur_Sale.Text = "";
        txtReason.Text = "";
        txtL_Salary.Text = "";
        txtmin_sal.Text = "";
        txtJ_Date.Text = "";
        txtIndu.Text = "";
        txtEffFrom.Text = "";
        txtToDate.Text = "";
        txtCover.Text = "";
    }
    
}
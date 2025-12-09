using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;


public partial class MIS_Reports_rptQuiz_Status : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsQuizAvg = null;
    DataSet dsDivision = null;
    DataSet dssfName = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sf_name = string.Empty;
    string ans = string.Empty;
    string imonth = string.Empty;
    string iyear = string.Empty;
    string day = string.Empty;
        
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"];
        sf_name = Request.QueryString["sf_name"];
        imonth = Request.QueryString["imonth"];
        iyear = Request.QueryString["iyear"];
        day = Request.QueryString["day"];
        ans = Request.QueryString["ans"];
        if (!Page.IsPostBack)
        {
            getAnswer();
            getsfName();
            lblSal.Text = "Online Test Results";
           
        }
    }
    private void getsfName()
    {
        SalesForce sf1 = new SalesForce();
        dssfName = sf1.getSfName_quiz(sf_code);
        if (dssfName.Tables[0].Rows.Count > 0)
        {
            lblsfname.Text = dssfName.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
    }

    private void getAnswer()
    {

        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
        // dsSalesForce = sf.getSales(div_code, sReport, Image_Id);
        dsSalesForce = sf.Quiz_Result_ViewNew(div_code, sf_code, imonth, iyear, day);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();

            dsQuizAvg = sf.Quiz_Result_View_AvgNew(div_code, sf_code, imonth, iyear, ans, day);
            {
                if (dsQuizAvg.Tables[0].Rows.Count > 0)
                {
                    grdQuizResultAvg.Visible = true;
                    grdQuizResultAvg.DataSource = dsQuizAvg;
                    grdQuizResultAvg.DataBind();
                }
            }
        }
    }
    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

         //   Label lblBackColor = (Label)e.Row.FindControl("lblCorrect");
            //string bcolor = "#" + lblBackColor.Text;
            //e.Row.BackColor = System.Drawing.Color.FromName(bcolor);
           
            Label lblS = (Label)e.Row.FindControl("lblCorrectans");
            Label lblactual = (Label)e.Row.FindControl("lblact");
            Label lblCorrect = (Label)e.Row.FindControl("lblCorrect");
            Label lblCorr_attem = (Label)e.Row.FindControl("lblCorrectAtt");
            if (ans == "1")
            {
                grdSalesForce.Columns[5].Visible = false;
                if (lblactual.Text == lblCorrect.Text)
                {
                    lblS.ForeColor = System.Drawing.Color.Red;
                    lblS.Style.Add("font-size", "12pt");
                    lblS.Style.Add("font-weight", "Bold");
                    lblS.Text = "*";
                }
            }
            else if (ans == "2")
            {
                grdSalesForce.Columns[4].Visible = false;
                if (lblactual.Text == lblCorr_attem.Text)
                {
                    lblS.ForeColor = System.Drawing.Color.Red;
                    lblS.Style.Add("font-size", "12pt");
                    lblS.Style.Add("font-weight", "Bold");
                    lblS.Text = "*";
                }
            }
    
        }
       
    }
}
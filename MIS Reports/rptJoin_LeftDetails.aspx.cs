using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MIS_Reports_rptJoin_LeftDetails : System.Web.UI.Page
{
    string Frm_Month = string.Empty;
    string To_Month = string.Empty;
    string Frm_Year = string.Empty;
    string To_Year = string.Empty;
    string Div_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Frm_Month = Request.QueryString["FMonth"].ToString();
        To_Month = Request.QueryString["TMonth"].ToString();
        Frm_Year = Request.QueryString["FYear"].ToString();
        To_Year = Request.QueryString["TYear"].ToString();
        Div_Code=Session["div_code"].ToString();
        string strFrmMonth=string.Empty;
        string strToMonth=string.Empty;

        SalesForce sf = new SalesForce();
        DataSet dsjoin = new DataSet();
        DataSet dsLeft = new DataSet();

        dsjoin = sf.sp_get_Join_Left_Details(Frm_Month, To_Month, Frm_Year, To_Year, Div_Code);



        //strFrmMonth= System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(DateTime.Parse("01/"+Frm_Month+"/"+Frm_Year).DayOfWeek);
        //strToMonth= System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.GetDayName(DateTime.Parse("01/"+To_Month+"/"+To_Year).DayOfWeek);

        strFrmMonth = sf.getMonthName(Frm_Month);
        strToMonth = sf.getMonthName(To_Month);

        lblJoining.Text = "New Join Details For " + strFrmMonth +" " + Frm_Year + " to " +strToMonth +" " + To_Year ;
        if (dsjoin.Tables[0].Rows.Count > 0)
        {
            grdJoining.DataSource = dsjoin;
            grdJoining.DataBind();
        }
        else
        {
            grdJoining.DataSource = null;
            grdJoining.DataBind();
        }

        lblLeft.Text = "Left Out Details For " + strFrmMonth + " " + Frm_Year + " to " + strToMonth + " " + To_Year;
        dsLeft = sf.sp_get_Left_Details(Frm_Month, To_Month, Frm_Year, To_Year, Div_Code);

        if (dsLeft.Tables[0].Rows.Count > 0)
        {
            grdLeft.DataSource = dsLeft;
            grdLeft.DataBind();
        }
        else
        {
            grdLeft.DataSource = dsLeft;
            grdLeft.DataBind();
        }



        



    }
}
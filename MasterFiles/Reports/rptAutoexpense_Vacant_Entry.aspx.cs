using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string st1 = string.Empty;
    string test = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataTable table = null;
    int slno = 0;
    int count = 0;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

             FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
            //expsub();
            

        }
       
        if (Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            // c1.FindControl("btnBack").Visible = false;
        }

        //FillFieldForcediv(divcode);

    }

   
    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
           monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2017; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    private void FillFieldForcediv(string divcode)
    {
        SalesForce dv = new SalesForce();
        dsSubDivision = dv.salesforce_vcant(divcode);
           
        
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            
        }
    }

   
   
    //protected void linkcheck_Click(object sender, EventArgs e)
    //{
       
    //   FillFieldForcediv(divcode);
    //   ddlSubdiv.Visible = true;
    //    linkcheck.Visible = false;
    //    txtNew.Visible = true;
    //    btnSF.Visible = true;
        
    //}
    protected void btnSF_Click(object sender, EventArgs e)
    {

        expsub();
       
    }
    private void expsub ()
    {
 MnthId.Visible = true;
        string sfcode = ddlSubdiv.SelectedValue.ToString();
        string[] sf=sfcode.Split('$');
        string s1 = sf[0];
        string s2 = sf[1];
        string s3 = sf[2];
        string s4 = sf[3];
        string s5 = sf[4];
        stdt.InnerHtml = "DCR Start Date :" + s4;
        enddt.InnerHtml = "DCR End Date :" + s5;
        Distance_calculation dsexp=new Distance_calculation();
        DataTable grdexpsub = dsexp.getExpSubmitMonth(s1, divcode, s2, s3);
        
        ExpSubmitgrdvw.DataSource = grdexpsub;
        ExpSubmitgrdvw.DataBind();
        

        DataTable grdexpntsub = dsexp.getExpNotSubmitMonth(s1, divcode, s2, s3);
        ExpNotSubmitgrdvw.DataSource = grdexpntsub;
        ExpNotSubmitgrdvw.DataBind();
}
   

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class Birthday_Wish : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Dear " + Session["sf_name"];
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce(sfCode);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {

                lbldob.Text = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(13).ToString(); // DOB
            }
        }
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {


        int Count;
        AdminSetup admin = new AdminSetup();

        Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

        if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MGR.aspx");
            }
        }
        else
        {

            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
}
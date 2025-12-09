using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class MasterFiles_Options_Bulk_Deactivation_Dr_Chem : System.Web.UI.Page
{
    string strsfcode = string.Empty;
    string sf_code = string.Empty;
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    string[] Distinct;
    string div_code = string.Empty;
    string str = string.Empty;
    DataSet dsSal = new DataSet();
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        // menu1.FindControl("btnBack").Visible = false;
        menu1.Title = Page.Title;
        //txtNew.Text = "";
        if (!Page.IsPostBack)
        {

            if (ViewState["AddValues"] == null)
            {

                DataTable dtSlip = new DataTable();
                dtSlip.Columns.AddRange(new DataColumn[2] { new DataColumn("SlipId", typeof(int)), 
                                    new DataColumn("SlipName", typeof(string)) });
                dtSlip.Columns["SlipId"].AutoIncrement = true;
                dtSlip.Columns["SlipId"].AutoIncrementSeed = 1;
                dtSlip.Columns["SlipId"].AutoIncrementStep = 1;
                ViewState["AddValues"] = dtSlip;
            }
        }

    }
    protected void AddValues(object sender, EventArgs e)
    {
        List<string> list = new List<string>(
                           txtadd.Text.Trim().Split(new string[] { "\r\n" },
                           StringSplitOptions.RemoveEmptyEntries));
        foreach (string item in list)
        {
            DataTable dtSlip = (DataTable)ViewState["AddValues"];
            dtSlip.Rows.Add();
            //lstAddColumns.Items.Add(new ListItem("SlNo", "0"));
            //lstAddColumns.Items.Add(new ListItem("EmpCode", "1"));
            SalesForce lst = new SalesForce();
            dsSal = lst.getSfCode(item);
            if (dsSal.Tables[0].Rows.Count > 0)
            {

                str = dsSal.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }

            dtSlip.Rows[dtSlip.Rows.Count - 1]["SlipName"] =  str;

          //  txtadd.Text = item;

            //  txtadd.Text = string.Empty;

            ViewState["AddValues"] = dtSlip;
            lstAddColumns.DataSource = dtSlip;
            lstAddColumns.DataTextField = "SlipName";
            lstAddColumns.DataValueField = "SlipId";
            lstAddColumns.DataBind();

        }
        txtadd.Text = "";
    }

   
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "0")
        {
            foreach (ListItem li in lstAddColumns.Items)
            {
                conn.Open();
                string sql = "update Mas_ListedDr set ListedDr_Active_Flag = 1,listeddr_deactivate_date = getdate() where ListedDr_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + li.Text + "' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
              
                    
               
            }
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            lstAddColumns.Items.Clear();
            ViewState["AddValues"] = null;
            if (ViewState["AddValues"] == null)
            {

                DataTable dtSlip = new DataTable();
                dtSlip.Columns.AddRange(new DataColumn[2] { new DataColumn("SlipId", typeof(int)), 
                                    new DataColumn("SlipName", typeof(string)) });
                dtSlip.Columns["SlipId"].AutoIncrement = true;
                dtSlip.Columns["SlipId"].AutoIncrementSeed = 1;
                dtSlip.Columns["SlipId"].AutoIncrementStep = 1;
                ViewState["AddValues"] = dtSlip;
            }
        }
        else if (ddlmode.SelectedValue == "1")
        {

            foreach (ListItem li in lstAddColumns.Items)
            {
                    conn.Open();
                    string sql = "update mas_chemists set Chemists_Active_Flag = 1,Chemists_DeActivate_Date = getdate() where Chemists_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + li.Text + "' ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();                 

               
            }
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            lstAddColumns.Items.Clear();
            ViewState["AddValues"] = null;
            if (ViewState["AddValues"] == null)
            {

                DataTable dtSlip = new DataTable();
                dtSlip.Columns.AddRange(new DataColumn[2] { new DataColumn("SlipId", typeof(int)), 
                                    new DataColumn("SlipName", typeof(string)) });
                dtSlip.Columns["SlipId"].AutoIncrement = true;
                dtSlip.Columns["SlipId"].AutoIncrementSeed = 1;
                dtSlip.Columns["SlipId"].AutoIncrementStep = 1;
                ViewState["AddValues"] = dtSlip;
            }
        }
    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstAddColumns.Items.Clear();
        ViewState["AddValues"] = null;
        if (ViewState["AddValues"] == null)
        {

            DataTable dtSlip = new DataTable();
            dtSlip.Columns.AddRange(new DataColumn[2] { new DataColumn("SlipId", typeof(int)), 
                                    new DataColumn("SlipName", typeof(string)) });
            dtSlip.Columns["SlipId"].AutoIncrement = true;
            dtSlip.Columns["SlipId"].AutoIncrementSeed = 1;
            dtSlip.Columns["SlipId"].AutoIncrementStep = 1;
            ViewState["AddValues"] = dtSlip;
        }
    }
}
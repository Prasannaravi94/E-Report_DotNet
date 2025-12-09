using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class HelpVideo : System.Web.UI.Page
{
    string search = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!Page.IsPostBack)
        {
            LoadTopic();
            Freq();
            Fillresult("1", "Fieldforce");
        }
    }
    private void LoadTopic()
    {
        //  int surveyID = Convert.ToInt32(hidSurveyId.Value);
        SqlDataAdapter da;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmd = new SqlCommand("select Topic_ID,Topic_Name from Mas_Support_Topics", con);
        DataSet ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        grdTopic.DataSource = ds;
        grdTopic.DataBind();
        con.Close();
        cmd.Dispose();
    }
    protected void grdTopic_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Topic")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string firstArgVal = commandArgs[0];
            string secondArgVal = commandArgs[1];
          //  string top_id = Convert.ToString(e.CommandArgument);

            Session["Topic_id"] = firstArgVal;
        
            grdResult.DataSource = null;
            grdResult.DataBind();
            // lblHeader.Text = "";
       //     lblrt.Text = Territory_Code;
            Fillresult(firstArgVal, secondArgVal);
           
        }
       
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Bind();
    }
    public DataSet Bind()
    {
        string name = Request.Form["Name"];
        SqlConnection cons = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        string sFind= string.Empty;
        SqlCommand cmd2 = new SqlCommand("select Search_Name from Mas_Support_Search where Search_Against_Nme like'%" + name + "%'", cons);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            search = ds2.Tables[0].Rows[0]["Search_Name"].ToString();
        }
        if (!string.IsNullOrWhiteSpace(name))
        {
            sFind += "  ( Topic_Display_Name like'%" + name + "%') ";
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            sFind += "  or ( Topic_Display_Name like'%" + search + "%') ";
        }
        SqlCommand cmd = new SqlCommand("select Topic_Display_ID,Topic_ID,Topic_Display_Name,Topic_Display_AName_One,Topic_Display_AName_Two,Topic_Display_AName_Three,Topic_Display_URL from Mas_Support_Topic_Display where "+sFind+" ", cons);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (!object.Equals(ds, null))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblhead.Text = "Searched Result : -";
                grdResult.Visible = true;
                grdResult.DataSource = ds.Tables[0];
                grdResult.DataBind();
            }
            else
            {
                lblhead.Text = "No Records Found";
                grdResult.Visible = false;
            }

        }


        return ds;
    }  

    private void Fillresult(string top_id,string top_name)
    {
        //  int surveyID = Convert.ToInt32(hidSurveyId.Value);
        SqlDataAdapter da;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmd = new SqlCommand("select Topic_Display_ID,Topic_ID,Topic_Display_Name,Topic_Display_AName_One,Topic_Display_AName_Two,Topic_Display_AName_Three,Topic_Display_URL from Mas_Support_Topic_Display where Topic_ID='" + top_id + "'", con);
        DataSet ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblhead.Text = top_name;
            grdResult.Visible = true;
            grdResult.DataSource = ds;
            grdResult.DataBind();
          
        }
        con.Close();
        cmd.Dispose();
    }
    private void Freq()
    {
        //  int surveyID = Convert.ToInt32(hidSurveyId.Value);
        SqlDataAdapter da;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmd = new SqlCommand("select Frequent_Q_ID,Topic_Name,Topic_Display_Name,Url from Mas_Support_Frequent_QA", con);
        DataSet ds = new DataSet();
        da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        grdFreq.DataSource = ds;
        grdFreq.DataBind();
        con.Close();
        cmd.Dispose();
    }
  

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
public partial class MasterFiles_Survey_Survey_Creation : System.Web.UI.Page
{
    public SqlConnection con;
    public SqlCommand com;
    string constr;
    string div_code = string.Empty;
    static string divcode = string.Empty;
    string Process_type = string.Empty;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        divcode = Session["div_code"].ToString();
        if (!this.IsPostBack)
        {
            if (Request.QueryString["Survey_Id"] != null)
            {
                hidSurveyId.Value = Request.QueryString["Survey_Id"].ToString();

                Session["Survey_Id"] = hidSurveyId.Value;
                btnAddQuestion.Text = "Update Survey";
                head.InnerText = "Survey Updation";
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Question"), new DataColumn("Control"), new DataColumn("Date", typeof(DateTime)), new DataColumn("id", typeof(Int32)), new DataColumn("Process_Type") });

                ViewState["Customers"] = dt;
                LoadSurveyQuestions();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Question"), new DataColumn("Control"), new DataColumn("Date", typeof(DateTime)), new DataColumn("id", typeof(Int32)), new DataColumn("Process_Type") });

                ViewState["Customers"] = dt;
                this.BindGrid();
            }
        }
    }
    protected void BindGrid()
    {

        GridView1.DataSource = (DataTable)ViewState["Customers"];
        GridView1.DataBind();
    }
    private void LoadSurveyQuestions()
    {

        // int surveyID = Convert.ToInt32(hidSurveyId.Value);

        //   int surveyID = Convert.ToInt32(Session["SurveyId"].ToString());

        SqlDataAdapter daSurveyQuest;
        SqlDataAdapter daSurvey;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmdSurveyQuest = new SqlCommand("select distinct S.Survey_ID,Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
                                                   " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
                                                   "  Question_Name as Question,Control_Name as Control, Control_Para, Question_Add_Names, q.Creation_Date as Date,cast(D.Question_Id as int) as id,Processing_Type as Process_Type " +
                                                   " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
                                                   "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id ", con);
        DataSet dsSurveyQuest = new DataSet();
        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
        daSurveyQuest.Fill(dsSurveyQuest);
        //GridView1.DataSource = dsSurveyQuest;
        //GridView1.DataBind();
        if (dsSurveyQuest.Tables[0].Rows.Count > 0)
        {
            txtsurvey.Text = dsSurveyQuest.Tables[0].Rows[0]["Survey_Title"].ToString();
            txtFdate.Text = dsSurveyQuest.Tables[0].Rows[0]["From_date"].ToString();
            txtTdate.Text = dsSurveyQuest.Tables[0].Rows[0]["To_Date"].ToString();
            txtno.Text = dsSurveyQuest.Tables[0].Rows[0]["Survey_No_Question"].ToString();
            dsSurveyQuest.Tables[0].Columns.Remove("Survey_ID");
            dsSurveyQuest.Tables[0].Columns.Remove("Survey_Title");
            dsSurveyQuest.Tables[0].Columns.Remove("dt");
            dsSurveyQuest.Tables[0].Columns.Remove("Survey_No_Question");
            dsSurveyQuest.Tables[0].Columns.Remove("From_date");
            dsSurveyQuest.Tables[0].Columns.Remove("To_Date");
            dsSurveyQuest.Tables[0].Columns.Remove("Control_Id");

            dsSurveyQuest.Tables[0].Columns.Remove("Control_Para");
            dsSurveyQuest.Tables[0].Columns.Remove("Question_Add_Names");
            DataTable dtr = dsSurveyQuest.Tables[0].Copy();
            ViewState["Customers"] = dtr;
            GridView1.DataSource = dsSurveyQuest;
            GridView1.DataBind();
            foreach (GridViewRow gridRow in GridView1.Rows)
            {

                string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
                CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
                Process_type = "";


                SqlCommand cmdSurvey = new SqlCommand("select distinct S.Survey_ID, Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
                                                           " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
                                                           " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,Processing_Type " +
                                                           " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
                                                           "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id and D.Question_Id ='" + Ques_id + "'", con);
                DataSet dsSurvey = new DataSet();
                daSurvey = new SqlDataAdapter(cmdSurvey);
                daSurvey.Fill(dsSurvey);

                string[] strchkstate;
                string process = dsSurvey.Tables[0].Rows[0]["Processing_Type"].ToString();
                if (process != "")
                {
                    strchkstate = process.Split(',');
                    foreach (string chkst in strchkstate)
                    {
                        for (int iIndex = 0; iIndex < chkpro.Items.Count; iIndex++)
                        {
                            if (chkst.Trim() == chkpro.Items[iIndex].Value.Trim())
                            {
                                chkpro.Items[iIndex].Selected = true;

                            }
                        }
                    }
                }
            }

        }
        con.Close();
        //  cmdSurveyQuest.Dispose();


    }

    private void LoadSurveyQuestions2()
    {

        // int surveyID = Convert.ToInt32(hidSurveyId.Value);

        //   int surveyID = Convert.ToInt32(Session["SurveyId"].ToString());

        SqlDataAdapter daSurveyQuest;
        SqlDataAdapter daSurvey;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmdSurveyQuest = new SqlCommand("select distinct " +
                                                   "  Question_Name as Question,Control_Name as Control,  q.Creation_Date as Date,cast(D.Question_Id as int) as id,Processing_Type as Process_Type " +
                                                   " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
                                                   "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id ", con);
        DataSet dsSurveyQuest = new DataSet();
        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
        daSurveyQuest.Fill(dsSurveyQuest);
        //GridView1.DataSource = dsSurveyQuest;
        //GridView1.DataBind();
        if (dsSurveyQuest.Tables[0].Rows.Count > 0)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Question"), new DataColumn("Control"), new DataColumn("Date", typeof(DateTime)), new DataColumn("id", typeof(Int32)), new DataColumn("Process_Type") });
            DataTable dsnew = (DataTable)ViewState["Customers"];
            DataTable dtrow = dsSurveyQuest.Tables[0].Copy();
            //if (dsnew.Rows.Count != null)
            //{
            //    dtrow.Merge(dsnew);
            //}
            // ViewState["Customers"] = dtrow;
            GridView1.DataSource = dsnew;
            GridView1.DataBind();
            foreach (GridViewRow gridRow in GridView1.Rows)
            {

                string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
                CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
                Process_type = "";

                SqlCommand cmdSurvey = new SqlCommand("select distinct S.Survey_ID,cast(D.Question_Id as int) as id, Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
                                                           " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
                                                           " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,Processing_Type " +
                                                           " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
                                                           "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id and D.Question_Id ='" + Ques_id + "'", con);
                DataSet dsSurvey = new DataSet();
                daSurvey = new SqlDataAdapter(cmdSurvey);
                daSurvey.Fill(dsSurvey);

                string[] strchkstate;
                if (dsSurvey.Tables[0].Rows.Count > 0)
                {
                    string process = dsSurvey.Tables[0].Rows[0]["Processing_Type"].ToString();
                    if (process != "")
                    {
                        strchkstate = process.Split(',');
                        foreach (string chkst in strchkstate)
                        {
                            for (int iIndex = 0; iIndex < chkpro.Items.Count; iIndex++)
                            {
                                if (chkst.Trim() == chkpro.Items[iIndex].Value.Trim())
                                {
                                    chkpro.Items[iIndex].Selected = true;

                                }
                            }
                        }

                    }
                }
            }

        }
        con.Close();
        //  cmdSurveyQuest.Dispose();


    }
    private void connection()
    {
        constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        con = new SqlConnection(constr);
        con.Open();


    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        return AutoFillProducts(prefixText);

    }

    private static List<string> AutoFillProducts(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select Question_Name from Mas_Question_Creation where Question_Name like '%" + prefixText + "%' and Division_Code='" + divcode + "' and Active_Flag=0 ";

                //   com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["Question_Name"].ToString());
                    }
                }
                con.Close();
                return countryNames;


            }

        }
    }



    private void GetProductMasterDet(string Question_Name)
    {
        connection();
        com = new SqlCommand("GetQuestionDet", con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@Question_Name", Question_Name);
        com.Parameters.AddWithValue("@div_code", divcode);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds);
        DataTable dt = ds.Tables[0];
        con.Close();
        //Binding TextBox From dataTable
        if (dt.Rows.Count > 0)
        {
            txtques.Text = dt.Rows[0]["Question_Name"].ToString();
            txtcont.Text = dt.Rows[0]["Control_Name"].ToString();
            txtpara.Text = dt.Rows[0]["Control_Para"].ToString();
            txtname.Text = dt.Rows[0]["Creation_Date"].ToString();
            txtid.Text = dt.Rows[0]["Question_Id"].ToString();

        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        //calling method and Passing Values
        GetProductMasterDet(TextBox1.Text);
        DataTable dt = (DataTable)ViewState["Customers"];
        if (txtid.Text != "")
        {
            dt.Rows.Add(txtques.Text.Trim(), txtcont.Text.Trim(), txtname.Text, txtid.Text, "");
            dt = dt.DefaultView.ToTable(true, "Question", "Control", "Date", "id", "Process_Type");
            ViewState["Customers"] = dt;
            if (dt.Rows.Count > 0)
            {
                if (Request.QueryString["Survey_Id"] != null)
                {
                    LoadSurveyQuestions2();
                    TextBox1.Text = "";
                }
                else
                {
                    this.BindGrid();
                    TextBox1.Text = "";
                }
            }
        }
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = ViewState["Customers"] as DataTable;
        dt.Rows[index].Delete();
        ViewState["Customers"] = dt;
        if (dt.Rows.Count > 0)
        {
            if (Request.QueryString["Survey_Id"] != null)
            {
                LoadSurveyQuestions2();
            }
            else
            {
                this.BindGrid();
            }
        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string item = e.Row.Cells[0].Text;
            foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
            {
                if (button.CommandName == "Delete")
                {
                    //  button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
                    button.Attributes["onclick"] = "if(!confirm('Do you want to delete?')){ return false; };";
                }
            }
        }
    }

    protected void btnAddQuestion_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        int iReturn = -1;
        sb.Append("<root>");
        DateTime EffFrom = Convert.ToDateTime(txtFdate.Text.Trim());
        DateTime EffTo = Convert.ToDateTime(txtTdate.Text.Trim());
        string EffFromdate = EffFrom.Month.ToString() + "-" + EffFrom.Day + "-" + EffFrom.Year;
        string EffTodate = EffTo.Month.ToString() + "-" + EffTo.Day + "-" + EffTo.Year;
        foreach (GridViewRow gridRow in GridView1.Rows)
        {
            // string Ques_id = e.gridRow["value"].ToString();
            //  int Ques_id = Convert.ToInt32(this.GridView1.DataKeys[gridRow.RowIndex].Value);
            string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
            CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
            Process_type = "";
            for (int i = 0; i < chkpro.Items.Count; i++)
            {
                if (chkpro.Items[i].Selected)
                {

                    Process_type += chkpro.Items[i].Value + ",";

                }
            }

            sb.Append("<survey Question_Id='" + Ques_id + "'    " +
                                 "  Processing_Type ='" + Process_type.Trim() + "'  Division_Code='" + divcode + "' />");

        }
        sb.Append("</root>");
        conn.Open();
        if (Request.QueryString["Survey_Id"] != null)
        {
            SqlCommand cmd = new SqlCommand("Survey_Update", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@div_code", divcode);
            cmd.Parameters.AddWithValue("@Survey_Title", txtsurvey.Text.Trim());
            cmd.Parameters.AddWithValue("@Survey_Ques", txtno.Text.Trim());
            cmd.Parameters.AddWithValue("@Efrom", EffFromdate);
            cmd.Parameters.AddWithValue("@Eto", EffTodate);
            cmd.Parameters.AddWithValue("@Survey_Id", Request.QueryString["Survey_Id"]);
            cmd.Parameters.AddWithValue("@XMLProduct", sb.ToString());
            cmd.Parameters.Add("@retValue", SqlDbType.Int);
            cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
            conn.Close();
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Survey_Ques_Process.aspx'</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
            }
        }
        else
        {
            if (GridView1.Rows.Count != 0)
            {
                SqlCommand cmd = new SqlCommand("Survey_Insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@div_code", divcode);
                cmd.Parameters.AddWithValue("@Survey_Title", txtsurvey.Text.Trim());
                cmd.Parameters.AddWithValue("@Survey_Ques", txtno.Text.Trim());
                cmd.Parameters.AddWithValue("@Efrom", EffFromdate);
                cmd.Parameters.AddWithValue("@Eto", EffTodate);
                cmd.Parameters.AddWithValue("@XMLProduct", sb.ToString());
                cmd.Parameters.Add("@retValue", SqlDbType.Int);
                cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
                conn.Close();
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');window.location='Survey_Ques_Process.aspx'</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Question Search');</script>");
            }
        }

    }

    protected void btnADD_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Ques_Process.aspx");
    }
    protected void btnQues_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Ques_Creation.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Data;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Text;
//public partial class MasterFiles_Survey_Survey_Creation : System.Web.UI.Page
//{
//    public SqlConnection con;
//    public SqlCommand com;
//    string constr;
//    string div_code = string.Empty;
//    static string divcode = string.Empty;
//    string Process_type = string.Empty;
//    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        div_code = Session["div_code"].ToString();
//        divcode = Session["div_code"].ToString();
//        if (!this.IsPostBack)
//        {
//            if (Request.QueryString["Survey_Id"] != null)
//            {
//                hidSurveyId.Value = Request.QueryString["Survey_Id"].ToString();

//                Session["Survey_Id"] = hidSurveyId.Value;
//                btnAddQuestion.Text = "Update Survey";
//                head.InnerText = "Survey Updation";
//                DataTable dt = new DataTable();
//                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Question"), new DataColumn("Control"), new DataColumn("Date", typeof(DateTime)), new DataColumn("id", typeof(Int32)),new DataColumn("Process_Type") });

//                ViewState["Customers"] = dt;
//                LoadSurveyQuestions();
//            }
//            else
//            {
//                DataTable dt = new DataTable();
//                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Question"), new DataColumn("Control"), new DataColumn("Date", typeof(DateTime)), new DataColumn("id", typeof(Int32)), new DataColumn("Process_Type") });

//                ViewState["Customers"] = dt;
//                this.BindGrid();
//            }
//        }
//    }
//    protected void BindGrid()
//    {

//        GridView1.DataSource = (DataTable)ViewState["Customers"];
//        GridView1.DataBind();
//    }
//    private void LoadSurveyQuestions()
//    {

//        // int surveyID = Convert.ToInt32(hidSurveyId.Value);

//     //   int surveyID = Convert.ToInt32(Session["SurveyId"].ToString());

//            SqlDataAdapter daSurveyQuest;
//            SqlDataAdapter daSurvey;
//            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

//            con.Open();
//            SqlCommand cmdSurveyQuest = new SqlCommand("select distinct S.Survey_ID,Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
//                                                       " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
//                                                       "  Question_Name as Question,Control_Name as Control, Control_Para, Question_Add_Names, q.Creation_Date as Date,cast(D.Question_Id as int) as id,Processing_Type as Process_Type " +
//                                                       " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
//                                                       "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id ", con);
//            DataSet dsSurveyQuest = new DataSet();
//            daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
//            daSurveyQuest.Fill(dsSurveyQuest);
//            //GridView1.DataSource = dsSurveyQuest;
//            //GridView1.DataBind();
//            if(dsSurveyQuest.Tables[0].Rows.Count > 0)
//            {
//                txtsurvey.Text = dsSurveyQuest.Tables[0].Rows[0]["Survey_Title"].ToString();
//                txtFdate.Text = dsSurveyQuest.Tables[0].Rows[0]["From_date"].ToString();
//                txtTdate.Text = dsSurveyQuest.Tables[0].Rows[0]["To_Date"].ToString();
//            txtno.Text = dsSurveyQuest.Tables[0].Rows[0]["Survey_No_Question"].ToString();
//            dsSurveyQuest.Tables[0].Columns.Remove("Survey_ID");
//            dsSurveyQuest.Tables[0].Columns.Remove("Survey_Title");
//            dsSurveyQuest.Tables[0].Columns.Remove("dt");
//            dsSurveyQuest.Tables[0].Columns.Remove("Survey_No_Question");
//            dsSurveyQuest.Tables[0].Columns.Remove("From_date");
//            dsSurveyQuest.Tables[0].Columns.Remove("To_Date");
//            dsSurveyQuest.Tables[0].Columns.Remove("Control_Id");

//            dsSurveyQuest.Tables[0].Columns.Remove("Control_Para");
//            dsSurveyQuest.Tables[0].Columns.Remove("Question_Add_Names");
//            DataTable dtr = dsSurveyQuest.Tables[0].Copy();
//            ViewState["Customers"] = dtr;
//            GridView1.DataSource = dsSurveyQuest;
//            GridView1.DataBind();
//            foreach (GridViewRow gridRow in GridView1.Rows)
//            {

//                string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
//                CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
//                Process_type = "";


//                SqlCommand cmdSurvey = new SqlCommand("select distinct S.Survey_ID, Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
//                                                           " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
//                                                           " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,Processing_Type " +
//                                                           " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
//                                                           "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id and D.Question_Id ='" + Ques_id + "'", con);
//                DataSet dsSurvey = new DataSet();
//                daSurvey = new SqlDataAdapter(cmdSurvey);
//                daSurvey.Fill(dsSurvey);

//                string[] strchkstate;
//                string process = dsSurvey.Tables[0].Rows[0]["Processing_Type"].ToString();
//                if (process != "")
//                {
//                    strchkstate = process.Split(',');
//                    foreach (string chkst in strchkstate)
//                    {
//                        for (int iIndex = 0; iIndex < chkpro.Items.Count; iIndex++)
//                        {
//                            if (chkst.Trim() == chkpro.Items[iIndex].Value.Trim())
//                            {
//                                chkpro.Items[iIndex].Selected = true;

//                            }
//                        }
//                    }
//                }
//            }

//        }
//        con.Close();
//          //  cmdSurveyQuest.Dispose();


//    }

//    private void LoadSurveyQuestions2()
//    {

//        // int surveyID = Convert.ToInt32(hidSurveyId.Value);

//        //   int surveyID = Convert.ToInt32(Session["SurveyId"].ToString());

//        SqlDataAdapter daSurveyQuest;
//        SqlDataAdapter daSurvey;
//        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

//        con.Open();
//        SqlCommand cmdSurveyQuest = new SqlCommand("select distinct "+
//                                                   "  Question_Name as Question,Control_Name as Control,  q.Creation_Date as Date,cast(D.Question_Id as int) as id,Processing_Type as Process_Type " +
//                                                   " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
//                                                   "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id ", con);
//        DataSet dsSurveyQuest = new DataSet();
//        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
//        daSurveyQuest.Fill(dsSurveyQuest);
//        //GridView1.DataSource = dsSurveyQuest;
//        //GridView1.DataBind();
//        if (dsSurveyQuest.Tables[0].Rows.Count > 0)
//        {

//            DataTable dt = new DataTable();
//            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Question"), new DataColumn("Control"), new DataColumn("Date", typeof(DateTime)), new DataColumn("id", typeof(Int32)), new DataColumn("Process_Type") });
//            DataTable dsnew = (DataTable)ViewState["Customers"];
//            DataTable dtrow = dsSurveyQuest.Tables[0].Copy();
//            //if (dsnew.Rows.Count != null)
//            //{
//            //    dtrow.Merge(dsnew);
//            //}
//           // ViewState["Customers"] = dtrow;
//            GridView1.DataSource = dsnew;
//            GridView1.DataBind();
//            foreach (GridViewRow gridRow in GridView1.Rows)
//            {

//                string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
//                CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
//                Process_type = "";

//                SqlCommand cmdSurvey = new SqlCommand("select distinct S.Survey_ID,cast(D.Question_Id as int) as id, Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
//                                                           " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
//                                                           " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,Processing_Type " +
//                                                           " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + divcode + "' " +
//                                                           "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id and D.Question_Id ='" + Ques_id + "'", con);
//                DataSet dsSurvey = new DataSet();
//                daSurvey = new SqlDataAdapter(cmdSurvey);
//                daSurvey.Fill(dsSurvey);

//                string[] strchkstate;
//                if (dsSurvey.Tables[0].Rows.Count > 0)
//                {
//                    string process = dsSurvey.Tables[0].Rows[0]["Processing_Type"].ToString();
//                    if (process != "")
//                    {
//                        strchkstate = process.Split(',');
//                        foreach (string chkst in strchkstate)
//                        {
//                            for (int iIndex = 0; iIndex < chkpro.Items.Count; iIndex++)
//                            {
//                                if (chkst.Trim() == chkpro.Items[iIndex].Value.Trim())
//                                {
//                                    chkpro.Items[iIndex].Selected = true;

//                                }
//                            }
//                        }

//                    }
//                }
//            }

//        }
//        con.Close();
//        //  cmdSurveyQuest.Dispose();


//    }
//    private void connection()
//    {
//        constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
//        con = new SqlConnection(constr);
//        con.Open();


//    }

//    [System.Web.Script.Services.ScriptMethod()]
//    [System.Web.Services.WebMethod]
//    public static List<string> GetCompletionList(string prefixText, int count)
//    {
//        return AutoFillProducts(prefixText);

//    }

//    private static List<string> AutoFillProducts(string prefixText)
//    {
//        using (SqlConnection con = new SqlConnection())
//        {
//            con.ConnectionString = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;

//            using (SqlCommand com = new SqlCommand())
//            {
//                com.CommandText = "select Question_Name from Mas_Question_Creation where Question_Name like '%" + prefixText + "%' and Division_Code='"+ divcode + "' and Active_Flag=0 ";

//            //   com.Parameters.AddWithValue("@Search", prefixText);
//                com.Connection = con;
//                con.Open();
//                List<string> countryNames = new List<string>();
//                using (SqlDataReader sdr = com.ExecuteReader())
//                {
//                    while (sdr.Read())
//                    {
//                        countryNames.Add(sdr["Question_Name"].ToString());
//                    }
//                }
//                con.Close();
//                return countryNames;


//            }

//        }
//    }



//    private void GetProductMasterDet(string Question_Name)
//    {
//        connection();
//        com = new SqlCommand("GetQuestionDet", con);
//        com.CommandType = CommandType.StoredProcedure;
//        com.Parameters.AddWithValue("@Question_Name", Question_Name);
//        com.Parameters.AddWithValue("@div_code", divcode);
//        SqlDataAdapter da = new SqlDataAdapter(com);
//        DataSet ds=new DataSet();
//        da.Fill(ds);
//        DataTable dt = ds.Tables[0];
//        con.Close();
//        //Binding TextBox From dataTable
//        if (dt.Rows.Count > 0)
//        {
//            txtques.Text = dt.Rows[0]["Question_Name"].ToString();
//            txtcont.Text = dt.Rows[0]["Control_Name"].ToString();
//            txtpara.Text = dt.Rows[0]["Control_Para"].ToString();
//            txtname.Text = dt.Rows[0]["Creation_Date"].ToString();
//            txtid.Text = dt.Rows[0]["Question_Id"].ToString();

//        }
//    }
//    protected void TextBox1_TextChanged(object sender, EventArgs e)
//    {
//        //calling method and Passing Values
//        GetProductMasterDet(TextBox1.Text);
//        DataTable dt = (DataTable)ViewState["Customers"];
//        if (txtid.Text != "")
//        {
//            dt.Rows.Add(txtques.Text.Trim(), txtcont.Text.Trim(), txtname.Text, txtid.Text,"");
//            dt = dt.DefaultView.ToTable(true, "Question", "Control", "Date", "id","Process_Type");
//            ViewState["Customers"] = dt;
//            if (dt.Rows.Count > 0)
//            {
//                if (Request.QueryString["Survey_Id"] != null)
//                {
//                    LoadSurveyQuestions2();
//                    TextBox1.Text = "";
//                }
//                else
//                {
//                    this.BindGrid();
//                    TextBox1.Text = "";
//                }
//            }
//        }
//    }
//    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
//    {
//        int index = Convert.ToInt32(e.RowIndex);
//        DataTable dt = ViewState["Customers"] as DataTable;
//        dt.Rows[index].Delete();
//        ViewState["Customers"] = dt;
//        if (dt.Rows.Count > 0)
//        {
//            if (Request.QueryString["Survey_Id"] != null)
//            {
//                LoadSurveyQuestions2();
//            }
//            else
//            {
//                this.BindGrid();
//            }
//        }
//    }
//    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
//    {
//        if (e.Row.RowType == DataControlRowType.DataRow)
//        {
//            string item = e.Row.Cells[0].Text;
//            foreach (Button button in e.Row.Cells[2].Controls.OfType<Button>())
//            {
//                if (button.CommandName == "Delete")
//                {
//                    //  button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
//                    button.Attributes["onclick"] = "if(!confirm('Do you want to delete?')){ return false; };";
//                }
//            }
//        }
//    }

//    protected void btnAddQuestion_Click(object sender, EventArgs e)
//    {
//        StringBuilder sb = new StringBuilder();
//        int iReturn = -1;
//        sb.Append("<root>");
//        DateTime EffFrom = Convert.ToDateTime(txtFdate.Text.Trim());
//        DateTime EffTo = Convert.ToDateTime(txtTdate.Text.Trim());
//        string EffFromdate = EffFrom.Month.ToString() + "-" + EffFrom.Day + "-" + EffFrom.Year;
//        string EffTodate = EffTo.Month.ToString() + "-" + EffTo.Day + "-" + EffTo.Year;
//        foreach (GridViewRow gridRow in GridView1.Rows)
//        {
//            // string Ques_id = e.gridRow["value"].ToString();
//            //  int Ques_id = Convert.ToInt32(this.GridView1.DataKeys[gridRow.RowIndex].Value);
//            string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
//            CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
//            Process_type = "";
//            for (int i = 0; i < chkpro.Items.Count; i++)
//            {
//                if (chkpro.Items[i].Selected)
//                {

//                    Process_type += chkpro.Items[i].Value + ",";

//                }
//            }

//            sb.Append("<survey Question_Id='" + Ques_id + "'    " +
//                                 "  Processing_Type ='" + Process_type.Trim() + "'  Division_Code='" + divcode + "' />");

//        }
//        sb.Append("</root>");
//        conn.Open();
//        if (Request.QueryString["Survey_Id"] != null)
//        {
//            SqlCommand cmd = new SqlCommand("Survey_Update", conn);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@div_code", divcode);
//            cmd.Parameters.AddWithValue("@Survey_Title", txtsurvey.Text.Trim());
//            cmd.Parameters.AddWithValue("@Survey_Ques", txtno.Text.Trim());
//            cmd.Parameters.AddWithValue("@Efrom", EffFromdate);
//            cmd.Parameters.AddWithValue("@Eto", EffTodate);
//            cmd.Parameters.AddWithValue("@Survey_Id", Request.QueryString["Survey_Id"]);
//            cmd.Parameters.AddWithValue("@XMLProduct", sb.ToString());
//            cmd.Parameters.Add("@retValue", SqlDbType.Int);
//            cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
//            cmd.ExecuteNonQuery();
//            iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
//            conn.Close();
//            if (iReturn > 0)
//            {
//                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Survey_Ques_Process.aspx'</script>");
//            }
//            else
//            {
//                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
//            }
//        }
//        else
//        {
//            if (GridView1.Rows.Count != 0)
//            {
//                SqlCommand cmd = new SqlCommand("Survey_Insert", conn);
//                cmd.CommandType = CommandType.StoredProcedure;

//                cmd.Parameters.AddWithValue("@div_code", divcode);
//                cmd.Parameters.AddWithValue("@Survey_Title", txtsurvey.Text.Trim());
//                cmd.Parameters.AddWithValue("@Survey_Ques", txtno.Text.Trim());
//                cmd.Parameters.AddWithValue("@Efrom", EffFromdate);
//                cmd.Parameters.AddWithValue("@Eto", EffTodate);
//                cmd.Parameters.AddWithValue("@XMLProduct", sb.ToString());
//                cmd.Parameters.Add("@retValue", SqlDbType.Int);
//                cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
//                cmd.ExecuteNonQuery();
//                iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
//                conn.Close();
//                if (iReturn > 0)
//                {
//                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');window.location='Survey_Ques_Process.aspx'</script>");
//                }
//                else
//                {
//                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
//                }
//            }
//            else
//            {
//                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Question Search');</script>");
//            }
//        }

//    }

//    protected void btnADD_Click(object sender, EventArgs e)
//    {
//        Response.Redirect("Survey_Ques_Process.aspx");
//    }
//    protected void btnQues_Click(object sender, EventArgs e)
//    {
//        Response.Redirect("Survey_Ques_Creation.aspx");
//    }
//    protected void btnBack_Click(object sender, EventArgs e)
//    {
//        Response.Redirect("~/Default.aspx");
//    }

//}
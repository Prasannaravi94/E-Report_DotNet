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
using System.Web.Configuration;
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
public partial class MasterFiles_Quesionaire_AddQuestions : System.Web.UI.Page
{
    SqlConnection con;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
    int Question_Id;
    string div_code = string.Empty;
    string sChkLocation = string.Empty;
    string sProd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {


            LoadQuestionType();
            LoadAnswerType();
            FillSubdiv();
            LoadQuestionAll();
            //FillProd();
        }
    }
    private void LoadQuestionAll()
    {
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        cmd = new SqlCommand("select Question_Id,Question_Text from Trans_Questionnaire_Head where Division_code='" + div_code + "'", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlQues.DataSource = ds;
        ddlQues.DataValueField = ds.Tables[0].Columns["Question_Id"].ColumnName;
        ddlQues.DataTextField = ds.Tables[0].Columns["Question_Text"].ColumnName;
        ddlQues.DataBind();
        ListItem li = new ListItem();
        li.Text = "-- Select Question--";
        li.Value = "0";
        ddlQues.Items.Insert(0, li);
    }

    private void FillProd()
    {
        lstProd.Items.Clear();
        Product sf = new Product();
        DataSet dsProd = new DataSet();
        string chks = string.Empty;
        for (int i = 0; i < chkSubdiv.Items.Count; i++)
        {
            if (chkSubdiv.Items[i].Selected)
            {
                chks = chks + chkSubdiv.Items[i].Value + ",";
            }
        }
        if (chks != "")
        {
            chks= chks.Remove(chks.Length - 1);
        //dsProd = sf.getProduct_SubDivision_Select(div_code, sChkLocation);
        //if (dsProd.Tables[0].Rows.Count > 0)
        //{

        //    lstProd.DataTextField = "Product_Detail_Name";
        //    lstProd.DataValueField = "Product_Code_SlNo";
        //    lstProd.DataSource = dsProd;
        //    lstProd.DataBind();

        //}
      
            string sProcName = "", sTblName = "";

            sProcName = "Get_Product_Sub_wise";

            DataSet dsts = new DataSet();
            if (sProcName != "")
            {
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);
                con.Open();
                SqlCommand cmd = new SqlCommand(sProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Division_Code", div_code);

                cmd.Parameters.AddWithValue("@SuBCode", chks);

                cmd.CommandTimeout = 600;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsts);
                if (dsts.Tables[0].Rows.Count > 0)
                {

                    lstProd.DataTextField = "Product_Detail_Name";
                    lstProd.DataValueField = "Product_Code_SlNo";
                    lstProd.DataSource = dsts;
                    lstProd.DataBind();

                }
                else
                {
                    lstProd.Items.Clear();
                }
                con.Close();
            }
        }

    }
    private void LoadQuestionType()
    {
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        cmd = new SqlCommand("select Question_Type_Id,Question_Type_Name,Question_Type_SName from Mas_QuestionType", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlQuestionType.DataSource = ds;
        ddlQuestionType.DataValueField = ds.Tables[0].Columns["Question_Type_Id"].ColumnName;
        ddlQuestionType.DataTextField = ds.Tables[0].Columns["Question_Type_Name"].ColumnName;
        ddlQuestionType.DataBind();
        ListItem li = new ListItem();
        li.Text = "-- Select Question Type--";
        li.Value = "0";
        ddlQuestionType.Items.Insert(0, li);
    }
    private void LoadAnswerType()
    {
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        cmd = new SqlCommand("select Answer_Id,Answer_Type_Name,Answer_Type_SName from Mas_Answer_Type", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlAns.DataSource = ds;
        ddlAns.DataValueField = ds.Tables[0].Columns["Answer_Id"].ColumnName;
        ddlAns.DataTextField = ds.Tables[0].Columns["Answer_Type_Name"].ColumnName;
        ddlAns.DataBind();
        ListItem li = new ListItem();
        li.Text = "-- Select Answer Type--";
        li.Value = "0";
        ddlAns.Items.Insert(0, li);
    }
    private void FillSubdiv()
    {
        //List of Sub division are loaded into the checkbox list from Division Class
        DataSet dsSubDivision = new DataSet();
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        chkSubdiv.DataTextField = "subdivision_name";
        chkSubdiv.DataSource = dsSubDivision;
        chkSubdiv.DataBind();
    }
    protected void btnAddQuestion_Click1(object sender, EventArgs e)
    {
        //if (ddlQuestionType.SelectedValue == "3")
        //{
        //    txtInputOptions.Text = "";
        //}
        //else
        string qus = hidQues.Value;
        string ans = hidAns.Value;
        DataSet dsQues = new DataSet();
        DataSet dsAns = new DataSet();
        string qus_SName = string.Empty;
        string ans_SName = string.Empty;
        txtInputOptions.Text = txtInputOptions.Text.Replace("\r\n", "$").Trim();

        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();

            // Start a local transaction.
            DataSet dsSl = new DataSet();
            command.Connection = connection;

            command.CommandText = " SELECT ISNULL(MAX(Question_Id),0)+1 FROM Trans_Questionnaire_Head";
            //  SqlCommand cmdd;
            //  cmdd = new SqlCommand(Chk, con);

            SqlDataAdapter das = new SqlDataAdapter(command);

            das.Fill(dsSl);
            Question_Id = Convert.ToInt32(dsSl.Tables[0].Rows[0][0].ToString());

            command.Connection = connection;

            command.CommandText = " select Question_Type_SName from Mas_QuestionType where Question_Type_Id='" + ddlQuestionType.SelectedValue.Trim() + "'";
            //  SqlCommand cmdd;
            //  cmdd = new SqlCommand(Chk, con);

            SqlDataAdapter das2 = new SqlDataAdapter(command);

            das2.Fill(dsQues);
            if (dsQues.Tables[0].Rows.Count > 0)
            {
                qus_SName = dsQues.Tables[0].Rows[0]["Question_Type_SName"].ToString();
            }

            command.Connection = connection;

            command.CommandText = " select Answer_Type_SName from Mas_Answer_Type where Answer_Id='" + ddlAns.SelectedValue.Trim() + "'";
            //  SqlCommand cmdd;
            //  cmdd = new SqlCommand(Chk, con);

            SqlDataAdapter das3 = new SqlDataAdapter(command);

            das3.Fill(dsAns);
            if (dsAns.Tables[0].Rows.Count > 0)
            {
                ans_SName = dsAns.Tables[0].Rows[0]["Answer_Type_SName"].ToString();
            }
            //string sQus = ddlQuestionType.SelectedItem.Text.Trim();
            //string[] spl_Qus = sQus.Split('~');
            //string qus_Name = spl_Qus[0].Trim().ToString();
            //string qus_SName = spl_Qus[1].Trim().ToString();

            //string sAns = ddlAns.SelectedItem.Text.Trim();
            //string[] spl_Ans = sAns.Split('~');
            //string ans_Name = spl_Ans[0].Trim().ToString();
            //string ans_SName = spl_Ans[1].Trim().ToString();
            for (int i = 0; i < chkSubdiv.Items.Count; i++)
            {
                if (chkSubdiv.Items[i].Selected)
                {
                    sChkLocation = sChkLocation + chkSubdiv.Items[i].Value + "/";
                }
            }
            foreach (System.Web.UI.WebControls.ListItem item in lstProd.Items)
            {
                if (item.Selected)
                {

                    sProd += item.Value + "/";

                }
            }
            if (ddlAns.SelectedValue == "7")
            {

                command.CommandText =

                   " INSERT INTO Trans_Questionnaire_Head(Question_Id,Question_Text,Question_Type,Answer_Id,Answer_Type,Answers " +
                   ",Division_Code,Created_Date,Process_Month,Process_Year,Active_Flag,Process_Flag,Question_Type_SName,Answer_Type_SName,Sub_Div,Product_Code_SlNo,Question_Type_Mode,Parent_Question_ID,Parent_Question_Answer) " +
                   " VALUES (" + Question_Id + ",'" + txtQuestionText.Text.Trim() + "','" + ddlQuestionType.SelectedItem.Text.Trim() + "','" + ddlAns.SelectedValue + "','" + ddlAns.SelectedItem.Text.Trim() + "', " +
                   " '" + ddlMas.SelectedValue.Trim() + "','" + div_code + "',getdate(),'','','0','0','" + qus_SName + "','" + ans_SName + "','" + sChkLocation.Trim() + "','" + sProd.Trim() + "','" + RblType.SelectedValue + "','"+qus+"','"+ans+"' )";

                command.ExecuteNonQuery();

                connection.Close();
                DisplayMessageAddRedirect("Question has been added successfully", "AddQuestionList.aspx");
            }
            else
            {
                command.CommandText =

                    " INSERT INTO Trans_Questionnaire_Head(Question_Id,Question_Text,Question_Type,Answer_Id,Answer_Type,Answers " +
                    ",Division_Code,Created_Date,Process_Month,Process_Year,Active_Flag,Process_Flag,Question_Type_SName,Answer_Type_SName,Sub_Div,Product_Code_SlNo,Question_Type_Mode,Parent_Question_ID,Parent_Question_Answer) " +
                    " VALUES (" + Question_Id + ",'" + txtQuestionText.Text.Trim() + "','" + ddlQuestionType.SelectedItem.Text.Trim() + "','" + ddlAns.SelectedValue + "','" + ddlAns.SelectedItem.Text.Trim() + "', " +
                    " '" + txtInputOptions.Text + "','" + div_code + "',getdate(),'','','0','0','" + qus_SName + "','" + ans_SName + "','" + sChkLocation.Trim() + "','" + sProd.Trim() + "','" + RblType.SelectedValue + "','" + qus + "','" + ans + "'  )";

                command.ExecuteNonQuery();

                connection.Close();
                DisplayMessageAddRedirect("Question has been added successfully", "AddQuestionList.aspx");

            }
        }
    }
    public void DisplayMessageAddRedirect(string message, string pageName)
    {
        string script = GetAlert(message);
        script += string.Format("location.href='{0}';", pageName);
        ScriptManager.RegisterStartupScript(this, GetType(), "message", script, true);
    }
    public void DisplayMessage(string message)
    {
        string script = GetAlert(message);
        ScriptManager.RegisterStartupScript(this, GetType(), "message", script, true);
    }
    private string GetAlert(string message)
    {
        return string.Format("alert(\"{0}\");", message);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddQuestionList.aspx");
    }
    protected void OnCheckBox_Changed(object sender, EventArgs e)
    {
        FillProd();
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod]

    public static List<answers> Bind_Answer(string answer)
    {
        DataSet dsStockist = new DataSet();
        List<answers> objData = new System.Collections.Generic.List<answers>();

        string divcode = HttpContext.Current.Session["div_code"].ToString();

        Stockist ss = new Stockist();
        DataSet ds = new DataSet();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        cmd = new SqlCommand("SELECT Split.a.value('.', 'VARCHAR(100)') AS Answer FROM   ( SELECT Question_Id,  " +
                              " CAST ('<M>' + REPLACE(Answers, '$', '</M><M>') + '</M>' AS XML) AS Data  " +
                              " FROM  Trans_Questionnaire_Head where Question_Id='" + answer + "'  " +
                              " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); ", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                objData.Add(new answers
                {

                    Ans = ds.Tables[0].Rows[i]["Answer"].ToString()
                });
            }
        }
        return objData;

    }

}

public class answers
{
    public string Sname { get; set; }
    public string Ans { get; set; }
    public int ID { get; set; }
}





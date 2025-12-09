using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DBase_EReport;

public partial class MasterFiles_Options_Quote : System.Web.UI.Page
{
    string div_code = string.Empty;

    DataSet dsAdmin = null;
    int iRet = -1;
    string Sl_No = string.Empty;
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            FillQuote();
        }
        hHeading.InnerText = Page.Title;
    }
    private void FillQuote()
    {
      

        AdminSetup adm = new AdminSetup();
        dsAdmin = adm.Get_Quote(div_code);
        txtQuote.Focus();
        if (dsAdmin.Tables[0].Rows.Count > 0)
        {

            txtQuote.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            ViewState["SlNo"] = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txtQuote.Text = txtQuote.Text.Replace("~", "'");
            if (dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString() == "1")
            {
                chkback.Checked = true;
            }
            else
            {
                chkback.Checked = false;
            }


        }
        else
        {
            ViewState["SlNo"] = null;
        }

        if ((txtQuote.Text != "") && (txtQuote.Text != null))
        {
            btnSubmit.Text = "Update";
            // chkback.Checked = true;

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtQuote.Text.Trim().Length > 0)
        {
            string strback = chkback.Text;
            if (chkback.Checked == true)
            {
                strback = "1";
            }
            else
            {
                strback = "0";
            }
            AdminSetup adm = new AdminSetup();
            txtQuote.Text = txtQuote.Text.Trim().Replace("'", "~");
            string sql = "";
            conn.Open();
            if (ViewState["SlNo"] == null)
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Trans_Quote ";
                int Sl_No = db.Exec_Scalar(strQry);
                sql = " INSERT INTO Trans_Quote(Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag) " +
                             " VALUES ('" + Sl_No + "' ,'" + txtQuote.Text + "' , '" + div_code + "', getdate(),0,'" + strback + "') ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quote has been Updated Sucessfully');</script>");
               
                btnSubmit.Text = "Update";
                txtQuote.Text = txtQuote.Text.Trim().Replace("~", "'");
                FillQuote();
            }
            else
            {
                string slno = ViewState["SlNo"].ToString();
                sql = "update Trans_Quote set Quote_Text ='" + txtQuote.Text.Trim() + "', Division_Code='" + div_code + "', Home_Page_Flag='" + strback + "' " +
                      " where Quote_Active_Flag=0 and Sl_No='" + slno + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quote has been Updated Sucessfully');</script>");
            
                btnSubmit.Text = "Update";
                txtQuote.Text = txtQuote.Text.Trim().Replace("~", "'");
                FillQuote();
            }
            conn.Close();

        }
       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (txtQuote.Text.Trim().Length > 0)
        {
            AdminSetup adm = new AdminSetup();
            iRet = adm.Delete_Quote(txtQuote.Text.Trim(), div_code);
            if (iRet > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quote Deleted Sucessfully');</script>");
                txtQuote.Text = "";
            }
            btnSubmit.Text = "Submit";
            chkback.Checked = false;
            txtQuote.Focus();

               string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
               using (SqlConnection con = new SqlConnection(constr))
               {

                 
                   SqlCommand cmd = new SqlCommand();
                   cmd.CommandText = "update Trans_Quote set Quote_Active_Flag=1 " +
                 " where Quote_Active_Flag=0 and  Division_Code = @div_code ";
                   cmd.Connection = con;
             
                   cmd.Parameters.Add(new SqlParameter("@div_code", div_code));
                   con.Open();
                   cmd.ExecuteNonQuery();
                   ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Quote Deleted Sucessfully');</script>");
                   btnSubmit.Text = "Submit";
                   chkback.Checked = false;
                   txtQuote.Focus();
                   FillQuote();
                   con.Close();
               }
        }
    }
}
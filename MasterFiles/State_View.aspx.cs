using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.HtmlControls;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_State_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    string strQry = string.Empty;
    string sf_type = string.Empty;
    DataSet dsdiv = new DataSet();
    DataSet dsDivision = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        //menu1.FindControl("btnBack").Visible = false;
        if (!IsPostBack)
        {
            Filldiv();

            // Retrieve and display the state_code values

            //txtStateCodes.Text = stateCodes;
            //string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            //using (SqlConnection connection = new SqlConnection(strConn))
            //{
            //    connection.Open();
            //    string strQry = "select Division_Name from mas_division where division_active_flag=0 ";
            //    using (SqlCommand cmd = new SqlCommand(strQry, connection))
            //    {
            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                // Create list items for each record and add them to the DropDownList
            //                ListItem item = new ListItem(reader["Division_Name"].ToString());
            //                ddlDivision.Items.Add(item);
            //            }
            //        }
            //    }
            //}

            if (sf_type == "3")
            {
                div_code = Session["division_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }


            Division dv = new Division();
            if (sf_type == "3")
            {
                string[] strDivSplit = div_code.Split(',');
                foreach (string strdiv in strDivSplit)
                {
                    if (strdiv != "")
                    {
                        dsdiv = dv.getDivisionHO(strdiv);
                        System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                        liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        ddlDivision.Items.Add(liTerr);
                    }
                }
            }
            else
            {
                dsDivision = dv.getDivision_Name();
                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    ddlDivision.DataTextField = "Division_Name";
                    ddlDivision.DataValueField = "Division_Code";
                    ddlDivision.DataSource = dsDivision;
                    ddlDivision.DataBind();
                }
            }
        }
        string stateCodes = GetStateCodes(ddlDivision.SelectedValue);
    }
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }


    private string GetStateCodes(string division)
    {

        strQry = "DECLARE @stateCodes NVARCHAR(MAX); " +
     "SET @stateCodes = (SELECT state_code FROM mas_division WHERE division_code = '" + division + "');" +
     "SELECT ms.state_code, ms.statename " +
     "FROM dbo.SplitStringnew(@stateCodes, ',') ss " +
     "JOIN mas_state ms ON CAST(ss.value AS INT) = ms.state_code";
        return strQry;





    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        div_code = ddlDivision.SelectedValue;
        GetStateCodes(div_code);
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        using (SqlConnection connection = new SqlConnection(strConn))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand(strQry, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the GridView
                    grddivision.DataSource = dataTable;
                    grddivision.DataBind();
                }
            }
        }
    }
}
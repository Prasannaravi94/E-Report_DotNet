using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.IO;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_MR_FileUpload_MR : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsAdmin = null;
    DataSet dsdes = null;
    string sf_code = string.Empty;
    string FileName = string.Empty;
    
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        hHeading.InnerText = Page.Title;
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            BindGridviewData_MR();
            if (Session["sf_type"].ToString() == "1")
            {

                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;              


            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;     
            }
        }

    }
    private void BindGridviewData()
    {
        AdminSetup adsp = new AdminSetup();
        dsSalesForce = adsp.Get_FileUpload(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();

        }
        else
        {
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
        }
       
    }
    private void BindGridviewData_MR()
    {
        Designation ds = new Designation();
        dsdes = ds.getDesig_code(sf_code);
        AdminSetup adsp = new AdminSetup();
        dsSalesForce = adsp.Get_UserManual_MR(div_code, dsdes.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();

        }
        else
        {
            gvDetails.DataSource = dsSalesForce;
            gvDetails.DataBind();
        }

    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        int ID = int.Parse((sender as LinkButton).CommandArgument);
        string filename = string.Empty;
        string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select FileName, FileSubject, Div_Code, Update_dtm,ContentType " +
                        "  from file_info " +
                        "  where ID = '" + ID + "'";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();

                    filename = sdr["FileName"].ToString();
                    ContentType = sdr["ContentType"].ToString();
                }
                con.Close();
            }
        }
        string filePath = "~/MasterFiles/Options/Files/" + filename.ToString();
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
  
}
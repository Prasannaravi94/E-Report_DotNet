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
public partial class MasterFiles_Options_FileUpload : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string FileName = string.Empty;
    string FileSubject = string.Empty;
    DataSet dsDesig = null;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            BindGridviewData();
            txtFileSubject.Focus();
            FillCheckDesig();  
        }
        hHeading.InnerText = Page.Title;
    }
    private void FillCheckDesig()
    {

        Designation dv = new Designation();
        dsDesig = dv.getDesig_Check(div_code);
        chkDesig.DataTextField = "Designation_Short_Name";
        chkDesig.DataValueField = "Designation_Code";
        chkDesig.DataSource = dsDesig;
        chkDesig.DataBind();
    }

    // Bind Gridview Data
    private void BindGridviewData()
    {
        //AdminSetup adsp = new AdminSetup();
        //dsSalesForce = adsp.Get_FileUpload(div_code);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    gvDetails.DataSource = dsSalesForce;
        //    gvDetails.DataBind();

        //}       
        string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType,Designation_Short_Name,Designation_Code " +
                        "  from file_info " +
                        "  where div_Code = '" + div_code + "'";
                cmd.Connection = con;
                con.Open();
                gvDetails.DataSource = cmd.ExecuteReader();
                gvDetails.DataBind();
                con.Close();
            }
        }
    }
    protected void Upload(object sender, EventArgs e)
    {
        string sChkLocation = string.Empty;
        string sChkLocationname = string.Empty;
        if (fileUpload1.HasFile)
        {
            string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
            string subject = txtFileSubject.Text.Trim();
            string contentType = fileUpload1.PostedFile.ContentType;
            // string Data = string.Empty;
            fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + filename));
            using (Stream fs = fileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        //string query = "INSERT INTO file_info(FileName,FileSubject,Div_Code,Update_dtm,Data, ContentType) " +
                        //     " VALUES ( '" + filename + "' , '" + txtFileSubject.Text.Trim() + "', '" + div_code + "',  getdate(),'"+Data+"','" + ContentType + "') ";
                        SqlCommand cmd1 = new SqlCommand("SELECT isnull(max(ID)+1,'1') ID from file_info", con);

                        SqlDataAdapter daimage = new SqlDataAdapter(cmd1);
                        DataSet dsimage = new DataSet();
                        daimage.Fill(dsimage);
                        //  string query = "insert into file_info values ('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@FileName, @FileSubject,@div_code, getdate(),@ContentType,@Data,@Designation_Code,@Designation_Short_Name)";
                        string query = "insert into file_info(ID,FileName,FileSubject,Div_Code,Update_dtm,ContentType,Designation_Code,Designation_Short_Name) values ('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@FileName, @FileSubject,@div_code, getdate(),@ContentType,@Designation_Code,@Designation_Short_Name)";

                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@FileName", filename);
                            cmd.Parameters.AddWithValue("@FileSubject", subject);
                            cmd.Parameters.AddWithValue("@div_code", div_code);

                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                           // cmd.Parameters.AddWithValue("@Data", bytes);
                            for (int i = 0; i < chkDesig.Items.Count; i++)
                            {
                                if (chkDesig.Items[i].Selected)
                                {
                                    sChkLocation = sChkLocation + chkDesig.Items[i].Value + ",";
                                    sChkLocationname = sChkLocationname + chkDesig.Items[i].Text + ",";
                                }
                            }
                            sChkLocation = sChkLocation.TrimEnd(',');
                            sChkLocationname = sChkLocationname.TrimEnd(',');
                            cmd.Parameters.AddWithValue("@Designation_Code", sChkLocation);
                            cmd.Parameters.AddWithValue("@Designation_Short_Name", sChkLocationname);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                            con.Close();
                        }
                    }
                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select a File');</script>");
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
    public void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    public void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {

                    int ID = Convert.ToInt32(e.CommandArgument);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "delete from file_info where ID=@ID";
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@ID", ID));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
                    //  BindGridviewData();
                    BindGridviewData();
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

      

    }
}